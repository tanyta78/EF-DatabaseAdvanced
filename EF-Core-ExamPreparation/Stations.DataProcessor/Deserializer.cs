using System;
using Stations.Data;

namespace Stations.DataProcessor
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using Models;
    using Newtonsoft.Json;

    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportStations(StationsDbContext context, string jsonString)
        {
            IEnumerable<Station> stations = JsonConvert.DeserializeObject<IEnumerable<Station>>(jsonString);

            var result = new StringBuilder();

            foreach (Station station in stations)
            {

                if (string.IsNullOrEmpty(station.Town) || string.IsNullOrWhiteSpace(station.Town))
                {
                    station.Town = station.Name;

                }

                if (!IsValidStation(station, context))
                {
                    result.AppendLine(FailureMessage);
                }
                else
                {
                    context.Stations.Add(station);
                    context.SaveChanges();

                    result.AppendLine(String.Format(SuccessMessage, station.Name));
                }

            }

            // context.SaveChanges();

            return result.ToString();
        }

        private static bool IsValidStation(Station station, StationsDbContext context)
        {
            if (station.Town == null)
            {
                return false;
            }
            if (station.Name == null)
            {
                return false;
            }
            if (station.Town.Length > 50)
            {
                return false;
            }
            if (station.Name.Length > 50)
            {
                return false;
            }
            if (context.Stations.Any(s => s.Name == station.Name))
            {
                return false;
            }
            return true;

        }


        public static string ImportClasses(StationsDbContext context, string jsonString)
        {
            IEnumerable<SeatingClass> seatClasses = JsonConvert.DeserializeObject<IEnumerable<SeatingClass>>(jsonString);

            var result = new StringBuilder();

            foreach (SeatingClass seatingClass in seatClasses)
            {
                if (!IsValidClass(seatingClass, context))
                {
                    result.AppendLine(FailureMessage);
                }
                else
                {
                    context.SeatingClasses.Add(seatingClass);
                    context.SaveChanges();
                    result.AppendLine(String.Format(SuccessMessage, seatingClass.Name));
                }

            }

            return result.ToString();
        }

        private static bool IsValidClass(SeatingClass seatingClass, StationsDbContext context)
        {
            if (seatingClass.Name == null)
            {
                return false;
            }
            if (seatingClass.Abbreviation == null)
            {
                return false;
            }
            if (seatingClass.Abbreviation.Length != 2)
            {
                return false;
            }
            if (seatingClass.Name.Length > 30)
            {
                return false;
            }
            if (context.SeatingClasses.Any(s => s.Name == seatingClass.Name))
            {
                return false;
            }
            return true;
        }

        public static string ImportTrains(StationsDbContext context, string jsonString)
        {
            IEnumerable<Train> trains = JsonConvert.DeserializeObject<IEnumerable<Train>>(jsonString);

            var result = new StringBuilder();

            foreach (Train train in trains)
            {
                if (!IsValidTrain(train, context))
                {
                    result.AppendLine(FailureMessage);
                }
                else
                {
                    context.Trains.Add(train);
                    context.SaveChanges();
                    ImportTrainSeats(train, context, result);
                    context.SaveChanges();
                    result.AppendLine(String.Format(SuccessMessage, train.TrainNumber));
                }

            }

            return result.ToString();
        }

        private static void ImportTrainSeats(Train train, StationsDbContext db, StringBuilder result)
        {
            var trainSeats = train.TrainSeats;
            var validatedSeats = new List<TrainSeat>();
            foreach (var trainSeat in trainSeats)
            {
                var seatClass = trainSeat.SeatingClass;
                if (db.SeatingClasses.Any(sc => (sc.Name == seatClass.Name && sc.Abbreviation == seatClass.Abbreviation)) && trainSeat.Quantity > 0)
                {
                    validatedSeats.Add(trainSeat);
                    train.TrainSeats.Add(trainSeat);
                }
            }
            db.TrainSeats.AddRange(validatedSeats);
            db.SaveChanges();

        }

        private static bool IsValidTrain(Train train, StationsDbContext db)
        {
            ////•	TrainNumber – text with max length 10 (required, unique) 
            //•	Type – TrainType enumeration with possible values: "HighSpeed", "LongDistance" or "Freight"(optional)
            //    •	TrainSeats – Collection of type TrainSeat
            //    •	Trips – Collection of type Trip

            if (train.TrainNumber == null)
            {
                return false;
            }
            if (train.TrainNumber.Length > 10)
            {
                return false;
            }
            if (db.Trains.Any(t => t.TrainNumber == train.TrainNumber))
            {
                return false;
            }

            return true;
        }

        public static string ImportTrips(StationsDbContext context, string jsonString)
        {
            IEnumerable<Trip> trips = JsonConvert.DeserializeObject<IEnumerable<Trip>>(jsonString);

            var result = new StringBuilder();

            foreach (Trip trip in trips)
            {
                if (!IsValidTrip(trip, context))
                {
                    result.AppendLine(FailureMessage);
                }
                else
                {
                    context.Trips.Add(trip);
                    context.SaveChanges();

                    result.AppendLine($"Trip from {trip.OriginStation.Name} to {trip.DestinationStation.Name} imported.");
                }

            }

            return result.ToString();
        }

        private static bool IsValidTrip(Trip trip, StationsDbContext db)
        {
            if (trip.ArrivalTime == null || trip.DepartureTime == null || trip.DepartureTime > trip.ArrivalTime)
            {
                return false;
            }

            if (!db.Trains.Any(t => t.TrainNumber == trip.Train.TrainNumber) ||
                !db.Stations.Any(st => st.Name == trip.OriginStation.Name) ||
                !db.Stations.Any(st => st.Name == trip.DestinationStation.Name))
            {
                return false;
            }

            if (trip.Status.ToString() == null)
            {
                trip.Status = TripStatus.OnTime;
            }

            TimeSpan timeDifference;

            if (trip.TimeDifference != null && !TimeSpan.TryParseExact(trip.TimeDifference.ToString(), @"hh\:mm", CultureInfo.InvariantCulture, out timeDifference))
            {
                return false;
            }
            //to do •	Arrival/Departure date may be null or in format “dd/MM/yyyy HH:mm”


            return true;
        }

        public static string ImportCards(StationsDbContext context, string xmlString)
        {
            XDocument xmlDocument = XDocument.Parse(xmlString);
            var result = new StringBuilder();
            //XElement root = xmlDocument.Element("Cards");
            IEnumerable<XElement> cards = xmlDocument.XPathSelectElements("Cards/Card");

            List<CustomerCard> validCards = new List<CustomerCard>();

            foreach (XElement cardXml in cards)
            {
                string name = cardXml.Element("Name").Value;
                int age = int.Parse(cardXml.Element("Age").Value);

                CardType cardType =
                    cardXml.Element("CardType") == null
                        ? CardType.Normal
                        : (CardType)Enum.Parse(typeof(CardType), cardXml.Element("CardType").Value);

                CustomerCard card = new CustomerCard
                {
                    Name = name,
                    Age = age,
                    Type = cardType
                };

                if (!IsValidCard(card, context))
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                validCards.Add(card);
                result.AppendLine($"Record {card.Name} successfully imported.");
            }

            context.Cards.AddRange(validCards);
            context.SaveChanges();

            return result.ToString();
        }

        private static bool IsValidCard(CustomerCard card, StationsDbContext context)
        {
            //•	Name – text with max length 128(required)
            //    •	Age – integer between 0 and 120
            //    •	Type – CardType enumeration with values: "Normal", "Pupil", "Student", "Elder", "Debilitated"(default: Normal)
            if (card.Name.Length > 128)
            {
                return false;
            }
            if (card.Age < 0 || card.Age > 120)
            {
                return false;
            }
            return true;
        }

        public static string ImportTickets(StationsDbContext context, string xmlString)
        {
            //•	Price – decimal value of the ticket(required, non - negative)
            //    •	SeatingPlace – text with max length of 8 which combines seating class abbreviation plus a positive integer(required)
            //    •	TripId – integer(required)
            //    •	Trip – the trip for which the ticket is for (required)
            //    •	CustomerCardId – integer(optional)

            XDocument xmlDocument = XDocument.Parse(xmlString);
            var result = new StringBuilder();
            //XElement root = xmlDocument.Element("Tickets");
            IEnumerable<XElement> ticketsNode = xmlDocument.XPathSelectElements("Tickets/Ticket");

            List<Ticket> validTickets = new List<Ticket>();

            foreach (XElement ticketNode in ticketsNode)
            {
                decimal price = decimal.Parse(ticketNode.Attribute("price").Value);

                if (price <= 0m)
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                Ticket ticket = new Ticket
                {
                    Price = price
                };

                string seat = ticketNode.Attribute("seat")?.Value;

                if (!IsSeatNumberValid(seat) ||
                    !context.SeatingClasses.Any(sc => sc.Abbreviation == seat.Substring(0, 2)))
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                XElement tripNode = ticketNode.Element("Trip");

                string originStationName = tripNode?.Element("OriginStation")?.Value;
                string destinationStationName = tripNode?.Element("DestinationStation")?.Value;

                DateTime departureTime = DateTime.ParseExact(tripNode.Element("DepartureTime").Value, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                Trip trip = context.Trips
                    .SingleOrDefault(t =>
                        t.OriginStation.Name == originStationName &&
                        t.DestinationStation.Name == destinationStationName &&
                        t.DepartureTime == departureTime);

                if (trip == null)
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                XElement cardXml = ticketNode.Element("Card");

                if (cardXml != null)
                {
                    string cardName = cardXml.Attribute("Name").Value;

                    CustomerCard card = context.Cards.SingleOrDefault(c => c.Name == cardName);

                    if (card == null)
                    {
                        result.AppendLine(FailureMessage);
                        continue;
                    }

                    ticket.CustomerCard = card;
                }

                if (!IsSeatPlaceValid(trip.Train, seat))
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                ticket.SeatingPlace = seat;
                ticket.Trip = trip;

                validTickets.Add(ticket);
                Console.WriteLine($"Ticket from {originStationName} to {destinationStationName} departing at {departureTime:dd/MM/yyyy HH:mm} imported.");
            }

           context.Tickets.AddRange(validTickets);

            return result.ToString();
        }

        private static bool IsSeatPlaceValid(Train train, string seat)
        {
            string abbreviation = seat.Substring(0, 2);
            int seatNumber = int.Parse(seat.Substring(2));

            return train.TrainSeats.Any(ts => ts.SeatingClass.Abbreviation == abbreviation && ts.Quantity >= seatNumber);
        }

        private static bool IsSeatNumberValid(string seat)
        {
            int parsedNumber;
            bool isNumber = int.TryParse(seat.Substring(2), out parsedNumber);

            if (!isNumber)
            {
                return false;
            }

            if (parsedNumber <= 0)
            {
                return false;
            }

            return true;
        }
    }
}