namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
    using Services;
    using Services.Contracts;
    using Utilities;

    public class AddTeamToCommand:ICommand
    {
        private readonly ITeamService teamService;

        public AddTeamToCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }
       
        //•	AddTeamTo<eventName> <teamName>
        //Adds given team for event specified.If there are more than one events with same name pick the latest start date.

        public string Execute(string cmd, params string[] args)
        {
            // Validate input length
            Check.CheckLength(2, args);

            //check is event exist
            var eventName = args[0];
            if (!CommandHelper.IsEventExisting(eventName))
            {
                throw new ArgumentException(String.Format(Constants.ErrorMessages.EventNotFound,eventName));
            }
            
            //check is team exist
            var teamName = args[1];
            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(String.Format(Constants.ErrorMessages.TeamExists,teamName));
            }
            
            //check currentUser - is loggedin user
            var currentUser = Session.User;
            if (currentUser==null)
            {
             throw  new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            //check is user is creator of event
            if (!CommandHelper.IsUserCreatorOfEvent(eventName,currentUser))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }
            
            //check is team is already added to event
            if (CommandHelper.IsTeamEventExist(eventName,teamName))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.CannotAddSameTeamTwice);
            }

            return this.teamService.AddTeamTo(eventName, teamName);
        }
    }
}
