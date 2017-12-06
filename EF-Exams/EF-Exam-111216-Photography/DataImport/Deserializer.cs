namespace Photography.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using System.Text;
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
            throw new System.NotImplementedException();
        }

        public static string ImportAccessories(PhotographyDbContext context, string xmlString)
        {
            throw new System.NotImplementedException();
        }

        public static string ImportWorkshops(PhotographyDbContext context, string xmlString)
        {
            throw new System.NotImplementedException();
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
