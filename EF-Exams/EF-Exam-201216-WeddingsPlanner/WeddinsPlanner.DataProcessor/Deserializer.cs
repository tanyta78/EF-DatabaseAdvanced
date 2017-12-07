namespace WeddinsPlanner.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Data;
    using ImportDtos;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Enums;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string FailureMessage = "Error. Invalid data provided";
        private const string SuccessMessage = "Successfully imported {0}";

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }

        public static string ImportAgencies(WeddingsPlannerDbContext context, string jsonString)
        {
            var agenciesDtos = JsonConvert.DeserializeObject<AgencyDto[]>(jsonString);

            var sb = new StringBuilder();

            var validAgencies = new List<Agency>();

            foreach (var agencyDto in agenciesDtos)
            {
                if (!IsValid(agencyDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var agency = new Agency()
                {
                    Name = agencyDto.Name,
                    EmployeesCount = agencyDto.EmployeesCount,
                    Town = agencyDto.Town
                };

                validAgencies.Add(agency);
                sb.AppendLine(String.Format(SuccessMessage, agencyDto.Name));
            }

            context.Agencies.AddRange(validAgencies);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportPeople(WeddingsPlannerDbContext context, string jsonString)
        {
            var peopleDtos = JsonConvert.DeserializeObject<PersonDto[]>(jsonString);

            var sb = new StringBuilder();

            var validPeople = new List<Person>();

            foreach (var personDto in peopleDtos)
            {
                if (!IsValid(personDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                bool isGenderValid = Enum.TryParse(personDto.Gender.ToString(), out Gender gender);

                if (!isGenderValid)
                {
                    gender = Gender.NotSpecified;
                }


                Person person = new Person()
                {
                    FirstName = personDto.FirstName,
                    MiddleNameInitial = personDto.MiddleInitial,
                    LastName = personDto.LastName,
                    Gender = gender,
                    Birthdate = personDto.Birthdate,
                    Phone = personDto.Phone,
                    Email = personDto.Email
                };

                validPeople.Add(person);
                sb.AppendLine(String.Format(SuccessMessage, person.FullName));
            }

            context.Persons.AddRange(validPeople);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportWeddingsAndInvitations(WeddingsPlannerDbContext context, string jsonString)
        {
            var weddingDtos = JsonConvert.DeserializeObject<WeddingDto[]>(jsonString);

            var sb = new StringBuilder();

            var validWeddings = new List<Wedding>();

            foreach (var weddingDto in weddingDtos)
            {
                if (!IsValid(weddingDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                Person bride = context.Persons.FirstOrDefault(person =>
                    person.FullName == weddingDto.Bride);
                Person bridesgroom = context.Persons.FirstOrDefault(person =>
                   person.FullName == weddingDto.Bridegroom);
                Agency agency = context.Agencies.FirstOrDefault(ag => ag.Name == weddingDto.Agency);
                DateTime date = DateTime.Parse(weddingDto.Date);

                if (bride == null || bridesgroom == null || agency == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var wedding = new Wedding()
                {
                    Bride = bride,
                    Bridegroom = bridesgroom,
                    Date = date,
                    Agency = agency
                };

                var guests = weddingDto.Guests;
                var validGuests = new List<Invitation>();

                if (guests?.Count > 0)
                {
                    foreach (var guestDto in guests)
                    {
                        if (!IsValid(guestDto))
                        {
                            continue;
                        }

                        var guest = context.Persons.FirstOrDefault(p => p.FullName == guestDto.Name);

                        if (guest == null)
                        {
                            continue;
                        }

                        var invitation = new Invitation()
                        {
                            Guest = guest,
                            IsAttending = guestDto.RSVP,
                            Family = guestDto.Family
                        };

                        validGuests.Add(invitation);
                    }

                    wedding.Invitations.AddRange(validGuests);
                }

                validWeddings.Add(wedding);
                sb.AppendLine(String.Format(SuccessMessage, $"wedding of {bride.FirstName} and {bridesgroom.FirstName}"));
            }

            context.Weddings.AddRange(validWeddings);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportVenues(WeddingsPlannerDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(VenueDto[]), new XmlRootAttribute("venues"));
            var venueDtos = (VenueDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var sb = new StringBuilder();

            var validVenues = new List<Venue>();

            foreach (var venueDto in venueDtos)
            {
                if (!IsValid(venueDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var venue = new Venue()
                {
                    Name = venueDto.Name,
                    Capacity = venueDto.Capacity,
                    Town = venueDto.Town
                };

                validVenues.Add(venue);
                sb.AppendLine(String.Format(SuccessMessage, venueDto.Name));

            }
            context.Venues.AddRange(validVenues);
            context.SaveChanges();

            Random random = new Random();
            var weddingsVenues = new List<WeddingsVenue>();
            var weddings = context.Weddings.ToList();
            foreach (Wedding wedding in weddings)
            {
                int randomId = random.Next(2, context.Venues.Count() + 1);
                Venue venue = context.Venues.Find(randomId);
                
                var weddingVenue = new WeddingsVenue()
                {
                    Venue = venue,
                    Wedding = wedding
                };
                
                venue = context.Venues.Find(randomId-1);

                var weddingVenue2 = new WeddingsVenue()
                {
                    Venue = venue,
                    Wedding = wedding
                };
                
               weddingsVenues.Add(weddingVenue);
               weddingsVenues.Add(weddingVenue2);

            }

            context.WeddingsVenues.AddRange(weddingsVenues);
            context.SaveChanges();
            
            var result = sb.ToString();
            return result;
        }

        public static string ImportPresents(WeddingsPlannerDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(PresentDto[]), new XmlRootAttribute("presents"));
            var presentDtos = (PresentDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));

            var sb = new StringBuilder();

            var validCashPresents = new List<Cash>();
            var validGiftPresents = new List<Gift>();

            foreach (var presentDto in presentDtos)
            {
                if (!IsValid(presentDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var invitation = context.Invitations.FirstOrDefault(i => i.Id == presentDto.InvitationId);

                if (invitation==null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                Type presentType = Assembly
                    .GetAssembly(typeof(Present))
                    .GetTypes()
                    .FirstOrDefault(type => type.Name.ToLower() == (presentDto.Type).ToLower());

                if (presentDto.Type.ToLower() == "cash")
                {
                    if (presentDto.Amount==null)
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }
                    
                    var present = (Cash)Mapper.Map(presentDto, presentDto.GetType(), presentType);
                    validCashPresents.Add(present);
                    invitation.Present = present;
                }

                if (presentDto.Type.ToLower() == "gift")
                {
                    if (presentDto.Name == null)
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }

                    var size = presentDto.Size;

                    Size presentSize = Size.NotSpecified;

                    if (size != null)
                    {
                        bool isPresentSizeValid = Enum.TryParse(size, out presentSize);
                        if (!isPresentSizeValid)
                        {
                            sb.AppendLine(FailureMessage);
                            continue;
                        }
                    }
                    var present = (Gift)Mapper.Map(presentDto, presentDto.GetType(), presentType);
                    validGiftPresents.Add(present);
                    invitation.Present = present;
                }

                var guestName = context.Invitations
                    .Include(i => i.Guest)
                    .FirstOrDefault(i => i.Id == presentDto.InvitationId)
                    .Guest.FullName;


                sb.AppendLine(String.Format(SuccessMessage, $" {presentDto.Type} from {guestName}"));



            }
            context.CashPresents.AddRange(validCashPresents);
            context.GiftPresents.AddRange(validGiftPresents);

            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }
    }

}
