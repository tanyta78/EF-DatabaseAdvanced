namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;
    using Utilities;

    public class ShowEventCommand:ICommand
    {
        private readonly IEventService eventService;

        public ShowEventCommand(IEventService eventService)
        {
            this.eventService = eventService;
        }

        //•	ShowEvent<eventName>
        //*There might be several events with the same name. Always pick the one with the latest start date!
        public string Execute(string cmd, params string[] args)
        {
            // Validate input length
            Check.CheckLength(1, args);

            var eventName = args[0];
            
            //check is event with this name exist
            if (!CommandHelper.IsEventExisting(eventName))
            {
                throw new ArgumentException(String.Format(Constants.ErrorMessages.EventNotFound,eventName));
            }
          
            
            return this.eventService.ShowEvent(eventName);
        }
    }
}
