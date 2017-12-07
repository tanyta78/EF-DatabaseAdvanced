namespace Photography.App
{
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using AutoMapper;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class Application
    {
        public static void Main(string[] args)
        {
            var context = new PhotographyDbContext();
            ResetDatabase(context,true);

            Console.WriteLine("Database Reset.");

            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());

            ImportEntities(context);
            ExportEntities(context);
        }

        private static void ImportEntities(PhotographyDbContext context, string baseDir = @"Datasets\")
        {
            const string exportDir = "./ImportResults/";

            var lenses = DataProcessor.Deserializer.ImportLenses(context, File.ReadAllText(baseDir + "lenses.json"));
            PrintAndExportEntityToFile(lenses, exportDir + "Lenses.txt");

            var cameras = DataProcessor.Deserializer.ImportCameras(context, File.ReadAllText(baseDir + "cameras.json"));
            PrintAndExportEntityToFile(cameras, exportDir + "Cameras.txt");

            var photographers = DataProcessor.Deserializer.ImportPhotographers(context, File.ReadAllText(baseDir + "photographers.json"));
            PrintAndExportEntityToFile(photographers, exportDir + "Photographers.txt");


            var accessories = DataProcessor.Deserializer.ImportAccessories(context, File.ReadAllText(baseDir + "accessories.xml"));
            PrintAndExportEntityToFile(accessories, exportDir + "Accessories.txt");

            var workshops = DataProcessor.Deserializer.ImportWorkshops(context, File.ReadAllText(baseDir + "workshops.xml"));
            PrintAndExportEntityToFile(workshops, exportDir + "Workshops.txt");

        }

        private static void ExportEntities(PhotographyDbContext context)
        {
            const string exportDir = "./ExportResults/";

            string jsonOutput = DataProcessor.Serializer.ExportOrderedPhotographers(context);
            Console.WriteLine(jsonOutput);
            File.WriteAllText(exportDir + "photographers-ordered.json", jsonOutput);

            string jsonOutput2 = DataProcessor.Serializer.ExportLandscapePhotographers(context);
            Console.WriteLine(jsonOutput2);
            File.WriteAllText(exportDir + "landscape-photographers.json", jsonOutput2);


            string xmlOutput = DataProcessor.Serializer.ExportSameCamerasPhotographers(context);
            Console.WriteLine(xmlOutput);
            File.WriteAllText(exportDir + "same-cameras-photographers.xml", xmlOutput);

            string xmlOutput2 = DataProcessor.Serializer.ExportWorkshopsByLocation(context);
            Console.WriteLine(xmlOutput2);
            File.WriteAllText(exportDir + "workshops-by-location.xml", xmlOutput2);
        }

        private static void PrintAndExportEntityToFile(string entityOutput, string outputPath)
        {
            Console.WriteLine(entityOutput);
            File.WriteAllText(outputPath, entityOutput.TrimEnd());
        }

        private static void ResetDatabase(PhotographyDbContext context, bool shouldDeleteDatabase = false)
        {
            if (shouldDeleteDatabase)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            context.Database.EnsureCreated();

            var disableIntegrityChecksQuery = "EXEC sp_MSforeachtable @command1='ALTER TABLE ? NOCHECK CONSTRAINT ALL'";
            context.Database.ExecuteSqlCommand(disableIntegrityChecksQuery);

            var deleteRowsQuery = "EXEC sp_MSforeachtable @command1='DELETE FROM ?'";
            context.Database.ExecuteSqlCommand(deleteRowsQuery);

            var enableIntegrityChecksQuery = "EXEC sp_MSforeachtable @command1='ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'";
            context.Database.ExecuteSqlCommand(enableIntegrityChecksQuery);

            var reseedQuery = "EXEC sp_MSforeachtable @command1='DBCC CHECKIDENT(''?'', RESEED, 0)'";
            try
            {
                context.Database.ExecuteSqlCommand(reseedQuery);
            }
            catch (SqlException) // OrderItems table has no identity column, which isn't a problem
            {
            }
        }
    }
}
