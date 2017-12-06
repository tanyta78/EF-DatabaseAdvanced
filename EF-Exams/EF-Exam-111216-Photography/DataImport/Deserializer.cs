namespace Photography.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using AutoMapper;
    using Data;
    using ImportDtos;
    using Models;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private static string FailureMessage = "Error. Invalid data provided";
        private static string SuccessMessage = "Successfully imported {0}.";

        private static bool IsValid(object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }

        public static string ImportLenses(PhotographyDbContext context, string jsonString)
        {
            var lenDtos = JsonConvert.DeserializeObject<LenDto[]>(jsonString);
            
            var validLens = new List<Len>();
            var sb = new StringBuilder();

            foreach (var lenDto in lenDtos)
            {
                if (!IsValid(lenDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var len = new Len()
                {
                    Make = lenDto.Make,
                    FocalLength = lenDto.FocalLength,
                    MaxAperture = lenDto.MaxAperture,
                    CompatibleWith = lenDto.CompatibleWith
                };
                
                validLens.Add(len);

                string lenDtoinfo = $"{lenDto.Make} {lenDto.FocalLength}mm f{lenDto.MaxAperture:f1}";
                
                sb.AppendLine(string.Format(SuccessMessage, lenDtoinfo));
            }
            
            context.Lens.AddRange(validLens);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportCameras(PhotographyDbContext context, string jsonString)
        {
            var camerasDtos = JsonConvert.DeserializeObject<CameraDto[]>(jsonString);

            var validDslrCameras = new List<DSLRCamera>();
            var validMirrorlessCameras = new List<MirrorlessCamera>();
            var sb = new StringBuilder();

            foreach (var cameraDto in camerasDtos)
            {
                if (!IsValid(cameraDto) || cameraDto.Type == null || cameraDto.Make == null || cameraDto.Model == null )
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                Type cameraType = Assembly
                    .GetAssembly(typeof(Camera))
                    .GetTypes()
                    .FirstOrDefault(type => type.Name.ToLower() == (cameraDto.Type + "Camera").ToLower());
                
                if (cameraDto.Type.ToLower()== "mirrorless")
                {
                    var camera = (MirrorlessCamera) Mapper.Map(cameraDto, cameraDto.GetType(), cameraType);
                    validMirrorlessCameras.Add(camera);
                }

                if (cameraDto.Type.ToLower() == "dslr")
                {
                    var camera = (DSLRCamera)Mapper.Map(cameraDto, cameraDto.GetType(), cameraType);
                    validDslrCameras.Add(camera);
                }

                string cameraDtoinfo = $"{cameraDto.Type} {cameraDto.Make} {cameraDto.Model}";

                sb.AppendLine(string.Format(SuccessMessage, cameraDtoinfo));
            }

            context.MirrorlessCameras.AddRange(validMirrorlessCameras);
            context.DslrCameras.AddRange(validDslrCameras);

            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportPhotographers(PhotographyDbContext context, string jsonString)
        {
            var photographerDtos = JsonConvert.DeserializeObject<PhotographerDto[]>(jsonString);

            var validPhotographers = new List<Photographer>();
            var sb = new StringBuilder();

            foreach (var photographerDto in photographerDtos)
            {
                //check input valid
                if (!IsValid(photographerDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                //add random cameras
                var dslrCameras = context.DslrCameras.ToArray();
                int maxDslrCameraIds = dslrCameras.Length;
                
                var mirrorlessCameras = context.MirrorlessCameras.ToArray();
                int maxMirrorlessCameraIds = mirrorlessCameras.Length;
                
                Random rnd = new Random();
                Camera primaryCamera = dslrCameras[rnd.Next(maxDslrCameraIds-1)];
                Camera secondaryCamera = mirrorlessCameras[rnd.Next(maxMirrorlessCameraIds-1)];
                
                var primaryCameraMake = primaryCamera.Make;
                var secondaryCameraMake = secondaryCamera.Make;
                
                //check lenses and create list of valid ones
                var lensIds = photographerDto.Lenses;
                var validLens = new List<Len>();

                var dbLens = context.Lens;

                foreach (var lensId in lensIds)
                {
                    //check is id exist
                    var len = dbLens.SingleOrDefault(l => l.Id == lensId);

                    if (len==null )
                    {
                        continue;
                    }
                    
                    //check compatible with cameras
                    var compatibleWith = len.CompatibleWith;
                    

                    if (compatibleWith!=primaryCameraMake && compatibleWith!=secondaryCameraMake)
                    {
                        continue;
                    }

                  validLens.Add(len);
                }
                
                //create photographer

                var photographer = new Photographer()
                {
                    FirstName = photographerDto.FirstName,
                    LastName = photographerDto.LastName,
                    Lens = validLens,
                    Phone = photographerDto.Phone
                };


                validPhotographers.Add(photographer);
                
                sb.AppendLine(string.Format(SuccessMessage, $"{photographerDto.FirstName} {photographerDto.LastName} | Lenses:{validLens.Count}"));
            }

            context.Photographers.AddRange(validPhotographers);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportAccessories(PhotographyDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(AccessoryDto[]), new XmlRootAttribute("accessories"));
            var accessoryDtos = (AccessoryDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));
            
            var validAccessories = new List<Accessory>();
            var sb = new StringBuilder();

            foreach (var accessoryDto in accessoryDtos)
            {
                if (!IsValid(accessoryDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                //add random photographer
                var photographersDb = context.Photographers.ToArray();
                
                Random rnd = new Random();
                var photographer = photographersDb[rnd.Next(photographersDb.Count() - 1)];
                
                var accessory = new Accessory()
                {
                    Name = accessoryDto.Name,
                    Owner = photographer
                };
                
                validAccessories.Add(accessory);
                sb.AppendLine(String.Format(SuccessMessage, accessoryDto.Name));
            }

            context.Accessories.AddRange(validAccessories);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportWorkshops(PhotographyDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(WorkshopDto[]), new XmlRootAttribute("workshops"));
            var workshopDtos = (WorkshopDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));
            
           
           var validWorkshops = new List<Workshop>();
            var photographersWorkshops = new List<PhotographersWorkshop>();

            var sb = new StringBuilder();
            
            foreach (var workshopDto in workshopDtos)
            {
                if (!IsValid(workshopDto))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var name = workshopDto.Name;
                var location = workshopDto.Location;
                var price = workshopDto.PricePerParticipant;
                var trainerNames = workshopDto.Trainer.Split();
                var trainer = context.Photographers.FirstOrDefault(p => p.FirstName == trainerNames[0] && p.LastName == trainerNames[1]);
                
                //create workshop
                var workshop = new Workshop()
                {
                    Name = name,
                    Location = location,
                    PricePerParticipant = price,
                    Trainer = trainer
                };
                
                //check dates
                var startDateAsString = workshopDto.StartDate;
                if (startDateAsString!=null)
                {

                    //   DateTime startDate = DateTime.ParseExact(startDateAsString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                    DateTime startDate = DateTime.Parse(startDateAsString);
                    workshop.StartDate = startDate;
                }

                var endDateAsString = workshopDto.EndDate;
                if (endDateAsString != null)
                {
                    //DateTime endDate = DateTime.ParseExact(endDateAsString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                    DateTime endDate = DateTime.Parse(endDateAsString);
                    workshop.EndDate = endDate;
                }

                /*26-10-2017
                EFCore 2 has unclear error message when performing insert involving many - to - many join table. The property 'prop' on entity type 'someType' has a temporary value.. #10165
                    Closed
                Exception after upgrading the EF Core package to 2.0.1 #10360
                */
                /*
                //add participants if exists


                if (workshopDto.Participants.Length>0)
                {
                    var participantsDtos = workshopDto.Participants;
                    
                    var participants = new List<Photographer>();

                    foreach (var participantsDto in participantsDtos)
                    {
                        var photographer = context.Photographers.FirstOrDefault(p => p.FirstName == participantsDto.FirstName && p.LastName == participantsDto.LastName);
                        participants.Add(photographer);
                    }
                    
                    //create photographersWorkshops 
                   
                    foreach (var photographer in participants)
                    {
                        var pw = new PhotographersWorkshop()
                        {
                            Photographer = photographer,
                            Workshop = workshop
                        };
                        
                        photographersWorkshops.Add(pw);
                        //add pwlist to workshop
                        workshop.Participants.Add(pw);

                    }*/
                    
                
                validWorkshops.Add(workshop);
                sb.AppendLine(string.Format(SuccessMessage, workshopDto.Name));
            }
           
            context.Workshops.AddRange(validWorkshops);
           // context.PhotorgaphersWorkshops.AddRange(photographersWorkshops);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        public static string ImportWorkshopsManual(PhotographyDbContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(WorkshopDto[]), new XmlRootAttribute("workshops"));
            var workshopDtos = (WorkshopDto[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));


            var validWorkshops = new List<Workshop>();
            var sb = new StringBuilder();

            foreach (var workshopDto in workshopDtos)
            {


                //var participantsDtos =
                //    (ParticipantDto[]) serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));
            }

            context.Workshops.AddRange(validWorkshops);
            context.SaveChanges();

            var result = sb.ToString();
            return result;
        }

        private static Camera GetCamera(CameraDto cameraDto)
        {
            // Determine the camera type with reflection:

            Type cameraType = Assembly
                .GetAssembly(typeof(Camera))
                .GetTypes()
                .FirstOrDefault(type => type.Name.ToLower() == (cameraDto.Type + "Camera").ToLower());

            // All the needed code to create 'CameraDslr' or 'CameraMirrorless' if we use AutoMapper:

           // Object cameraObject = Mapper.Map(cameraDto, cameraDto.GetType(), cameraType);
            
          //  return cameraObject as Camera;

            Camera cameraEntity = new Camera();

            if (cameraType?.Name.ToLower() == "dslrcamera")
            {
                cameraEntity = new DSLRCamera()
                {
                    Make = cameraDto.Make,
                    Model = cameraDto.Model,
                    IsFullFrame = cameraDto.IsFullFrame,
                    MinISO = cameraDto.MinISO,
                    MaxISO = cameraDto.MaxISO,
                    MaxShutterSpeed = cameraDto.MaxShutterSpeed
                };
            }
            else if (cameraType?.Name.ToLower() == "mirrorlesscamera")
            {
                cameraEntity = new MirrorlessCamera()
                {
                    Make = cameraDto.Make,
                    Model = cameraDto.Model,
                    IsFullFrame = cameraDto.IsFullFrame,
                    MinISO = cameraDto.MinISO,
                    MaxISO = cameraDto.MaxISO,
                    MaxVideoResolution = cameraDto.MaxVideoResolution,
                    MaxFrameRate = cameraDto.MaxFrameRate
                };
            }

            return cameraEntity;
        }
    }
}
