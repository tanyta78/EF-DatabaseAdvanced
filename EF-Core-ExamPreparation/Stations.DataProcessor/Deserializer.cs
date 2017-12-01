using System;
using Stations.Data;

namespace Stations.DataProcessor
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Models;
    using Newtonsoft.Json;

    public static class Deserializer
    {
        private const string FailureMessage = "Invalid data format.";
        private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportStations(StationsDbContext context, string jsonString)
        {
            string stations = File.ReadAllText(jsonString);
            List<Station> validStations = new List<Station>();
            var result = new StringBuilder();

            //not finished - 
            //•	If any other validation error occurs (such as long station or town name) proceed as described above. - where???
            //what to return?
            // to create dto !!! and helpersmethods for checking validation
            
            foreach (Station station in stations)
            {
                if (string.IsNullOrEmpty(station.Town))
                {
                    station.Town = station.Name;
                }

                try
                {
                    context.Stations.Add(station);
                    result.AppendLine($"Record {station.Name} successfully imported.");
                }
                catch (Exception e)
                {
                    result.AppendLine(FailureMessage);
                }

            }

            context.SaveChanges();
            
            return result;
        }


        public static string ImportClasses(StationsDbContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportTrains(StationsDbContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportTrips(StationsDbContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static string ImportCards(StationsDbContext context, string xmlString)
        {
            throw new NotImplementedException();
        }

        public static string ImportTickets(StationsDbContext context, string xmlString)
        {
            throw new NotImplementedException();
        }
    }
}