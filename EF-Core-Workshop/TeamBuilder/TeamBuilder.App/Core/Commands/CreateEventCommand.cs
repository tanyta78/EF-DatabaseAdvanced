namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Globalization;
    using Contracts;
    using Services;
    using Services.Contracts;
    using Utilities;

    public class CreateEventCommand:ICommand
    {
        private readonly IEventService eventService;

        public CreateEventCommand(IEventService eventService)
        {
            this.eventService = eventService;
        }

        //	CreateEvent<name> <description> <startDate> <endDate>
        //CreateEvent TEDexSofia Inovation 01/01/2012 12:00 02/01/2012 22:00
        public string Execute(string cmd, params string[] args)
        {
            // Validate input length
            Check.CheckLength(6, args);
            
            //get creator
            var creator = Session.User;

            if (creator==null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
            
            //get input data
            var eventName = args[0];
            var description = args[1];
            DateTime startDate;
            DateTime endDate;

            bool isStartDateTime = DateTime.TryParseExact(args[2] + " " + args[3],
                Constants.DateTimeFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out startDate);

            

            bool isEndDateTime = DateTime.TryParseExact(args[4] + " " + args[5],
                Constants.DateTimeFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out endDate);

            if (!isStartDateTime || !isEndDateTime)
            {
                throw new ArgumentException(Constants.ErrorMessages.InvalidDateFormat);
            }

            if (startDate > endDate)
            {
                throw new ArgumentException(Constants.ErrorMessages.StartDateAfterEndDate);
            }

            return this.eventService.CreateEvent(eventName,description,startDate,endDate);
        }
    }
}
