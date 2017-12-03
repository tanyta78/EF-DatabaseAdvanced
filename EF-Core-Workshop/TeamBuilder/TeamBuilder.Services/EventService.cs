namespace TeamBuilder.Services
{
    using System;
    using System.Linq;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class EventService:IEventService
    {
        private readonly TeamBuilderContext db;

        public EventService(TeamBuilderContext db)
        {
            this.db = db;
        }

        //•	CreateEvent <name> <description> <startDate> <endDate>
        //Creates an event (currently logged user is it’s creator). Keep in mind when parsing dates that there should be  additional spaces between them.
       // *There might be several events with the same name.Always pick the one with the latest start date!

        public string CreateEvent(string eventName, string description, DateTime startDate, DateTime endDate)
        {
            var creator = Session.User;
            var currentEvent = new Event()
            {
                CreatorId = creator.Id,
                Description = description,
                Name = eventName,
                StartDate = startDate,
                EndDate = endDate
            };

            this.db.Events.Add(currentEvent);
            this.db.SaveChanges();
           
            return $"Event {eventName} was created successfully!";
        }
        
        //•	ShowEvent <eventName>
        //There might be several events with the same name.Always pick the one with the latest start date!
        
        public string ShowEvent(string eventName)
        {
            var currentEvent = this.db.Events
                .Where(e=>e.Name==eventName)
                .OrderByDescending(e=>e.StartDate)
                .First();

            var participatingTeams = this.db.TeamEvents
                .Where(te => te.EventId == currentEvent.Id)
                .Include(te => te.Team)
                .ToList()
                .Select(te=>$"-{te.Team.Name}");

            var result = $"{currentEvent.Name} {currentEvent.StartDate} {currentEvent.EndDate}"+ Environment.NewLine +
                         $"{currentEvent.Description}" + Environment.NewLine + "" +
                         "Teams:" + Environment.NewLine +
                         $"{string.Join(Environment.NewLine,participatingTeams)}"; 

            return result;
        }
    }
}
