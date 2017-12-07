namespace WeddinsPlanner.DataProcessor
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using ExportDtos;
    using ImportDtos;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Enums;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportOrderedAgencies(WeddingsPlannerDbContext context)
        {
            var agency = context.Agencies
                .Select(a => new
                {
                    name = a.Name,
                    count = a.EmployeesCount,
                    town = a.Town
                })
                .OrderByDescending(a => a.count)
                .ThenBy(a => a.name)
                .ToArray();

            var json = JsonConvert.SerializeObject(agency, Formatting.Indented);
            return json;
        }

        public static string ExportGuestsLists(WeddingsPlannerDbContext context)
        {
            var weddinginfo = context.Weddings
                .Include(w => w.Bride)
                .Include(w => w.Bridegroom)
                .Include(w => w.Agency)
                .Include(w => w.Invitations)
                .ThenInclude(i => i.Guest)
                .Select(w => new ExportWeddingDto
                {
                    Bride = w.Bride.FullName,
                    Bridegroom = w.Bridegroom.FullName,
                    Agency = new ExportAgencyDto()
                    {
                        Name = w.Agency.Name,
                        Town = w.Agency.Town
                    },
                    InvitedGuests = w.Invitations.Count,
                    BrideGuests = w.Invitations.Count(i => i.Family == Family.Bride),
                    BridegroomGuests = w.Invitations.Count(i => i.Family == Family.Bridegroom),
                    AttendingGuests = w.Invitations.Count(i => i.IsAttending),
                    Guests = w.Invitations.Where(inv => inv.IsAttending).Select(i => i.Guest.FullName).ToList()

                })
                .OrderByDescending(wedding => wedding.InvitedGuests)
                .ThenBy(wedding => wedding.AttendingGuests).ToArray();

            var json = JsonConvert.SerializeObject(weddinginfo, Formatting.Indented);
            return json;
        }

        public static string ExportVenuesInSofia(WeddingsPlannerDbContext context)
        {
            //Venues which are not in Sofia because there are no venues in Sofia with more than 3 weddings
            var venuesInSofia = context.Venues.Where(v => v.Town != "Sofia" && v.Weddings.Count >= 3)
                .Select(w=>new ExportVenueDto()
                {
                    Name = w.Name,
                    Capacity = w.Capacity,
                    WeddingsCount = w.Weddings.Count
                })
                .OrderBy(v => v.Capacity).ToArray();
            
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ExportVenueDto[]), new XmlRootAttribute("venues"));

            serializer.Serialize(new StringWriter(sb), venuesInSofia, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString();
            return result;
        }

        public static string ExportAgenciesByTown(WeddingsPlannerDbContext context)
        {
            var agenciesByTown = context.Agencies
                   .Where(agency => agency.Town.Length >= 6)
                   .GroupBy(agency => agency.Town, agency => agency, (town, agencies) => new
                   {
                       Town = town,
                       Agencies = agencies.Where(agency => agency.Weddings.Count >= 2)
                   })
                   .Select(gr => new TownDto()
                   {
                       Name = gr.Town,
                       AgenciesDtos = gr.Agencies.Select(agency => new AgencyInTownDto()
                       {
                           Name = agency.Name,
                           Profit = agency.Weddings
                                       .Sum(wedding => wedding.Invitations
                                       .Where(inv => (inv.Present as Cash) != null)
                                       .Sum(inv => (inv.Present as Cash).Amount)
                                       ) * 0.2m,
                           Weddings = agency.Weddings.Select(wedding => new ExportWeddingInTownDto()
                           {
                               Cash = (wedding.Invitations
                                       .Where(inv => (inv.Present as Cash) != null)
                                       .Sum(inv => (decimal?)(inv.Present as Cash).Amount) ?? 0.0m),
                               Present = wedding.Invitations.Count(inv => (inv.Present as Gift) != null),
                               Bride = wedding.Bride.FirstName + " " + wedding.Bride.MiddleNameInitial + " " + wedding.Bride.LastName,
                               Bridegroom = wedding.Bridegroom.FirstName + " " + wedding.Bridegroom.MiddleNameInitial + " " + wedding.Bridegroom.LastName,
                               Guests = wedding.Invitations.Where(inv => inv.IsAttending).Select(guest => new ExportGuestsDto()
                               {
                                   Family = guest.Family.ToString(),
                                   Name = guest.Guest.FirstName + " " + guest.Guest.MiddleNameInitial + " " + guest.Guest.LastName,
                               }).ToList()
                           }).ToList()
                       }).ToList()
                   }).ToArray();

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(TownDto[]), new XmlRootAttribute("towns"));

            serializer.Serialize(new StringWriter(sb), agenciesByTown, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString();
            return result;
        }
    }
}
