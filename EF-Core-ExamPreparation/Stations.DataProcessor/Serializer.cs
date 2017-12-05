using System;
using Stations.Data;

namespace Stations.DataProcessor
{
    using System.Globalization;
    using System.Linq;
    using AutoMapper.QueryableExtensions;
    using Dto;
    using Models;
    using Newtonsoft.Json;

    public class Serializer
	{
		public static string ExportDelayedTrains(StationsDbContext db, string dateAsString)
		{
		    DateTime date = DateTime.ParseExact(dateAsString, "dd/MM/yyyy", CultureInfo.InvariantCulture); ;
		    var delayedTrips = db.Trips.Where(trip => trip.Status == TripStatus.Delayed && trip.DepartureTime <= date)
		        .AsQueryable()
		        .Select(t => new TripDto
		        {
		            TrainNumber = t.Train.TrainNumber,
		            TimeDifference = t.TimeDifference
		        })
		        .GroupBy(t => t.TrainNumber)
		        .ToList().AsQueryable()
		        .ProjectTo<TrainDto>()
		        .OrderByDescending(t => t.DelayedTimes)
		        .ThenByDescending(t => t.MaxDelayedTime)
		        .ThenBy(t => t.TrainNumber)
		        .ToList();
		    
		    return JsonConvert.SerializeObject(delayedTrips, Formatting.Indented);

		}

		public static string ExportCardsTicket(StationsDbContext context, string cardType)
		{
			throw new NotImplementedException();
		}
	}
}