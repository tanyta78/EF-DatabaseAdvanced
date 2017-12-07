namespace Photography.DataProcessor
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using ExportDtos;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Extensions.Internal;
    using Models;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportOrderedPhotographers(PhotographyDbContext context)
        {
            var photographers = context.Photographers
                .OrderBy(p => p.FirstName)
                .ThenByDescending(p => p.LastName)
                .Select(p => new
                {
                    p.FirstName,
                    p.LastName,
                    p.Phone
                });
            var json = JsonConvert.SerializeObject(photographers, Formatting.Indented);

            return json;
        }

        public static string ExportLandscapePhotographers(PhotographyDbContext context)
        {
            //var type = context.Photographers
            //    .Include(p=>p.PrimaryCamera).Count(p => p.PrimaryCamera.GetType()==typeof(MirrorlessCamera));
            
            var photographers = context.Photographers
                .Where(p => p.PrimaryCamera is MirrorlessCamera 
                                && p.Lens.Count > 0 
                && p.Lens.All(lens => lens.FocalLength <= 30))
                .OrderBy(p => p.FirstName)
                .Select(p => new
                {
                    p.FirstName,
                    p.LastName,
                    CameraMake = p.PrimaryCamera.Make,
                    LensesCount = p.Lens.Count
                }).ToArray();
            
            
            var json = JsonConvert.SerializeObject(photographers, Formatting.Indented);

            return json;
        }

        public static string ExportSameCamerasPhotographers(PhotographyDbContext context)
        {
            var photographers = context.Photographers
                .Include(p => p.Lens)
                .Where(p => p.PrimaryCamera.Make == p.SecondaryCamera.Make)
                .Select(p => new SameCameraMakePhotographerDto
                {
                    Name = p.FirstName + " " + p.LastName,
                    PrimaryCamera = p.PrimaryCamera.Make + " " +
                                    p.PrimaryCamera.Model,
                    Lenses = p.Lens.Select(l => l.Make + " " + l.FocalLength + "mm f" + l.MaxAperture).ToList()

                }).ToArray();

            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(SameCameraMakePhotographerDto[]), new XmlRootAttribute("photographers"));

            serializer.Serialize(new StringWriter(sb), photographers, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString();
            return result;
        }

        public static string ExportWorkshopsByLocation(PhotographyDbContext context)
        {
            var workshops = context.Workshops
                .Include(w=>w.Participants)
                .Where(w => w.Participants.Count >= 0)
                .GroupBy(w => w.Location,w=>w, (l, w) => new
                {
                    Location = l,
                    Workshops = w
                })
                .Where(l => l.Workshops.Any())
                .Select(e => new LocationDto()
                {
                    Name = e.Location,
                    WorkshopsDtos = e.Workshops.Select(w => new WorkshopExportDto()
                    {
                        Name = w.Name,
                        TotalProfit = (w.Participants.Count * w.PricePerParticipant) -
                                      ((w.Participants.Count * w.PricePerParticipant) * 0.2m),
                        ParticipantDto = new ParticipantsDto()
                        {
                            ParticipantCount = w.Participants.Count,
                            Names = w.Participants.Select(p => p.Photographer.FirstName + " " + p.Photographer.LastName)
                                .ToList()
                        }
                    }).ToList()
                }).ToArray();

            
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(LocationDto[]), new XmlRootAttribute("locations"));

            serializer.Serialize(new StringWriter(sb), workshops, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));

            var result = sb.ToString();
            return result;
        }
    }
}
