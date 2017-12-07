namespace WeddingsPlanner.App
{
    using System;
    using System.Data.SqlClient;
    using System.IO;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using WeddinsPlanner.Data;
    using WeddinsPlanner.DataProcessor;

    public class Application
    {
        public static void Main(string[] args)
        {
            var context = new WeddingsPlannerDbContext();
            ResetDatabase(context,true);
            //context.Database.EnsureCreated();
            Console.WriteLine("Database Reset.");

            Mapper.Initialize(cfg => cfg.AddProfile<MappingProfile>());

            ImportEntities(context);
            ExportEntities(context);
        }

        private static void ImportEntities(WeddingsPlannerDbContext context, string baseDir = @"Datasets\")
        {
            const string exportDir = "./ImportResults/";

            var agencies = Deserializer.ImportAgencies(context, File.ReadAllText(baseDir + "agencies.json"));
            PrintAndExportEntityToFile(agencies, exportDir + "agencies.txt");

            var people = Deserializer.ImportPeople(context, File.ReadAllText(baseDir + "people.json"));
            PrintAndExportEntityToFile(people, exportDir + "people.txt");

            var weddings = Deserializer.ImportWeddingsAndInvitations(context, File.ReadAllText(baseDir + "weddings.json"));
            PrintAndExportEntityToFile(weddings, exportDir + "weddings.txt");


            var venues = Deserializer.ImportVenues(context, File.ReadAllText(baseDir + "venues.xml"));
            PrintAndExportEntityToFile(venues, exportDir + "venues.txt");

            var presents = Deserializer.ImportPresents(context, File.ReadAllText(baseDir + "presents.xml"));
            PrintAndExportEntityToFile(presents, exportDir + "presents.txt");

        }

        private static void ExportEntities(WeddingsPlannerDbContext context)
        {
            const string exportDir = "./ExportResults/";

            string jsonOutput = Serializer.ExportOrderedAgencies(context);
            Console.WriteLine(jsonOutput);
            File.WriteAllText(exportDir + "Agencies-ordered.json", jsonOutput);

            string jsonOutput2 = Serializer.ExportGuestsLists(context);
            Console.WriteLine(jsonOutput2);
            File.WriteAllText(exportDir + "guests.json", jsonOutput2);
            
            string xmlOutput = Serializer.ExportVenuesInSofia(context);
            Console.WriteLine(xmlOutput);
            File.WriteAllText(exportDir + "sofia-venues.xml", xmlOutput);
          
            string xmlOutput2 = Serializer.ExportAgenciesByTown(context);
            Console.WriteLine(xmlOutput2);
            File.WriteAllText(exportDir + "agencies-by-town.xml", xmlOutput2);
        }

        private static void PrintAndExportEntityToFile(string entityOutput, string outputPath)
        {
            Console.WriteLine(entityOutput);
            File.WriteAllText(outputPath, entityOutput.TrimEnd());
        }

        private static void ResetDatabase(WeddingsPlannerDbContext context, bool shouldDeleteDatabase = false)
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
