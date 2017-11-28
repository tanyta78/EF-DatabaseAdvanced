namespace BusTicketSystem.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class BusStationService:IBusStationService
    {
        private readonly BusTicketSystemContext db;

        public BusStationService(BusTicketSystemContext db)
        {
            this.db = db;
        }
        
        
        public string PrintInfo(int busStationId)
        {
            var station = this.db.BusStations
                .Include(bs => bs.ArrivalTrips)
                .ThenInclude(at=>at.OriginBusStation)
                .Include(bs => bs.DepartureTrips)
                .ThenInclude(dt=>dt.DestinationBusStation)
                .Include(bs=>bs.Town)
                .FirstOrDefault(bs => bs.Id == busStationId);

            if (station==null)
            {
                throw new ArgumentException("No such bus station in system db.");
            }

            var arrivals = station.ArrivalTrips.Select(a =>
                $"From {a.OriginBusStation.Name} | Arrive at: {a.ArrivalTime} | Status: {a.Status}").ToList();
            
            var departures = station.DepartureTrips.Select(t => $"To {t.DestinationBusStation.Name} | Depart at: {t.DepartureTime} | Status {t.Status}");

            
            var result = $"{station.Name},{station.Town.Name}"+Environment.NewLine+"Arrivals:" + Environment.NewLine + string.Join(Environment.NewLine, arrivals)+Environment.NewLine+"Departures:"+  Environment.NewLine + string.Join(Environment.NewLine, departures);

            return result;

        }
    }
}
