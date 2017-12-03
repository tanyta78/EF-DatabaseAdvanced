namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
    using Services;
    using Services.Contracts;
    using Utilities;

    public class DisbandCommand:ICommand
    {
        private readonly ITeamService teamService;

        public DisbandCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }
        
        public string Execute(string cmd, params string[] args)
        {
            // Validate input length
            Check.CheckLength(1, args);

            string teamName = args[0];
            //is team exist
            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(String.Format(Constants.ErrorMessages.TeamNotFound, teamName));
            }

            //is there logged user
            if (Session.User == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            //is current user is creator of team
            if (!CommandHelper.IsUserCreatorOfTeam(teamName, Session.User))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }

            return this.teamService.Disband(teamName);
        }
    }
}
