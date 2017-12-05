namespace Stations.DataProcessor
{
    using System.Collections.Generic;
    using System.Text;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using Stations.Data;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Xml.Serialization;
    using AutoMapper;
    using Dto.Import;
    using Microsoft.EntityFrameworkCore;
    using Remotion.Linq.Clauses.ResultOperators;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;


    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportStations(StationsDbContext context, string jsonString)
        {
            var stationDtos = JsonConvert.DeserializeObject<StationDto[]>(jsonString);

            var result = new StringBuilder();

            var validStations = new List<Station>();

            foreach (var stationDto in stationDtos)
            {

                if (!IsValid(stationDto))
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                if (string.IsNullOrWhiteSpace(stationDto.Town))
                {
                    stationDto.Town = stationDto.Name;

                }

                var stationAlreadyExist = validStations.Any(s => s.Name == stationDto.Name);

                if (stationAlreadyExist)
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                var station = Mapper.Map<Station>(stationDto);

                validStations.Add(station);

                result.AppendLine(String.Format(SuccessMessage, station.Name));


            }
            context.Stations.AddRange(validStations);
            context.SaveChanges();
            return result.ToString();
        }

        private static bool IsValid(object obj)
        {
            var validContext = new ValidationContext(obj);
            var valRes = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validContext, valRes, true);

            return isValid;
        }

        private static bool IsValidStationManual(Station station, StationsDbContext context)
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
            var seatClassesDto = JsonConvert.DeserializeObject<SeatingClassDto[]>(jsonString);

            var result = new StringBuilder();

            var validClasses = new List<SeatingClass>();

            foreach (var scDto in seatClassesDto)
            {
                if (!IsValid(scDto))
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                var seatingClassAlreadyExist = validClasses.Any(s => s.Name == scDto.Name || s.Abbreviation == scDto.Abbreviation);

                if (seatingClassAlreadyExist)
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                var seatingClass = Mapper.Map<SeatingClass>(scDto);

                validClasses.Add(seatingClass);
                result.AppendLine(String.Format(SuccessMessage, seatingClass.Name));
            }

            context.SeatingClasses.AddRange(validClasses);

            context.SaveChanges();

            return result.ToString();
        }

        private static bool IsValidClassManual(SeatingClass seatingClass, StationsDbContext context)
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
            var trainDtos = JsonConvert.DeserializeObject<TrainDto[]>(jsonString, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var result = new StringBuilder();

            var validTrains = new List<Train>();

            foreach (var trainDto in trainDtos)
            {
                if (!IsValid(trainDto))
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                var trainAlreadyExist = validTrains.Any(t => t.TrainNumber == trainDto.TrainNumber);

                if (trainAlreadyExist)
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                var validSeats = trainDto.Seats.All(IsValid);

                if (!validSeats)
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                var seatingClassesAreValid = trainDto.Seats
                    .All(s =>context.SeatingClasses
                        .Any(sc => sc.Name == s.Name && sc.Abbreviation == s.Abbreviation));

                if (!seatingClassesAreValid)
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                var type = Enum.Parse<TrainType>(trainDto.Type);

                var trainSeats = trainDto.Seats.Select(s => new TrainSeat()
                {
                    SeatingClass =
                        context.SeatingClasses.SingleOrDefault(sc =>sc.Name == s.Name && sc.Abbreviation == s.Abbreviation),
                    Quantity = s.Quantity.Value
                }).ToArray();


                var train = new Train()
                {
                    TrainNumber = trainDto.TrainNumber,
                    Type = type,
                    TrainSeats = trainSeats

                };

                validTrains.Add(train);
                result.AppendLine(String.Format(SuccessMessage, trainDto.TrainNumber));
            }

            context.Trains.AddRange(validTrains);
            context.SaveChanges();
            
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

        private static bool IsValidTrainManual(Train train, StationsDbContext context)
        {
            if (train.TrainNumber == null)
            {
                return false;
            }
            if (train.TrainNumber.Length > 10)
            {
                return false;
            }
            if (context.Trains.Any(t => t.TrainNumber == train.TrainNumber))
            {
                return false;
            }

            return true;
        }

        public static string ImportTrips(StationsDbContext context, string jsonString)
        {
            var tripDtos = JsonConvert.DeserializeObject<TripDto[]>(jsonString, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });

            var result = new StringBuilder();

            var validTrips = new List<Trip>();

            foreach (var tripDto in tripDtos)
            {
                if (!IsValid(tripDto))
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                var train = context.Trains.SingleOrDefault(s => s.TrainNumber == tripDto.Train);

                var originStation = context.Stations.SingleOrDefault(s => s.Name == tripDto.OriginStation);

                var destinationStation = context.Stations.SingleOrDefault(s => s.Name == tripDto.DestinationStation);

                if (train == null || originStation == null || destinationStation == null)
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                var departureTime = DateTime.ParseExact(tripDto.DepartureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                var arrivalTime = DateTime.ParseExact(tripDto.ArrivalTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                TimeSpan timeDiff;
                if (tripDto.TimeDifference != null)
                {
                    timeDiff = TimeSpan.ParseExact(tripDto.TimeDifference, @"hh\:mm", CultureInfo.InvariantCulture);
                }

                if (departureTime > arrivalTime)
                {
                    result.AppendLine(FailureMessage);
                    continue;
                }

                var status = Enum.Parse<TripStatus>(tripDto.Status);

                var trip = new Trip()
                {
                    Train = train,
                    OriginStation = originStation,
                    DestinationStation = destinationStation,
                    DepartureTime = departureTime,
                    ArrivalTime = arrivalTime,
                    Status = status,
                    TimeDifference = timeDiff

                };

                validTrips.Add(trip);

                result.AppendLine($"Trip from {tripDto.OriginStation} to {tripDto.DestinationStation} imported.");
            }

            context.Trips.AddRange(validTrips);
            context.SaveChanges();

            return result.ToString();
        }

        public static string ImportCards(StationsDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(CardDto[]), new XmlRootAttribute("Cards"));
            var deserializedCards = (CardDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));
            var sb = new StringBuilder();
            var validCards = new List<CustomerCard>();

            foreach (var dtoCard in deserializedCards)
            {
                if (!IsValid(dtoCard))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                //to check 
                var cardType = Enum.TryParse<CardType>(dtoCard.CardType, out var card) ? card : CardType.Normal;

                var customerCard = new CustomerCard()
                {
                    Name = dtoCard.Name,
                    Type = cardType,
                    Age = dtoCard.Age
                };

                validCards.Add(customerCard);
                sb.AppendLine(String.Format(SuccessMessage, customerCard.Name));

            }
            context.Cards.AddRange(validCards);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportTickets(StationsDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(TicketDto[]), new XmlRootAttribute("Tickets"));
            var deserializedTickets = (TicketDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var sb = new StringBuilder();
            var validTickets = new List<Ticket>();

            foreach (var dtoTicket in deserializedTickets)
            {
                if (!IsValid(dtoTicket))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                //check trip
                var departureTime = DateTime.ParseExact(dtoTicket.Trip.DepartureTime, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                var trip = context.Trips
                    .Include(t => t.OriginStation)
                    .Include(t => t.DestinationStation)
                    .Include(t => t.Train)
                    .ThenInclude(tr => tr.TrainSeats)
                    .SingleOrDefault(t =>
                    t.OriginStation.Name == dtoTicket.Trip.OriginStation &&
                    t.DestinationStation.Name == dtoTicket.Trip.DestinationStation && t.DepartureTime == departureTime);

                if (trip == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                //check customer card
                CustomerCard card = null;
                if (dtoTicket.Card != null)
                {
                    card = context.Cards.SingleOrDefault(c => c.Name == dtoTicket.Card.Name);

                    if (card == null)
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }
                }

                //check seat
                var classAbbreviation = dtoTicket.Seat.Substring(0, 2);
                var quantity = int.Parse(dtoTicket.Seat.Substring(2));

                var seatExists = trip.Train.TrainSeats
                    .SingleOrDefault(s =>
                    s.SeatingClass.Abbreviation == classAbbreviation && s.Quantity >= quantity);

                if (seatExists == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var seat = dtoTicket.Seat;

                //create ticket
                var ticket = new Ticket()
                {
                    Trip = trip,
                    CustomerCard = card,
                    Price = dtoTicket.Price,
                    SeatingPlace = seat
                };

                validTickets.Add(ticket);
                sb.AppendLine(String.Format("Ticket from {0} to {1} departing at {2} imported.",
                    trip.OriginStation.Name,
                    trip.DestinationStation.Name,
                    trip.DepartureTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)));

            }
            context.Tickets.AddRange(validTickets);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }
    }
}