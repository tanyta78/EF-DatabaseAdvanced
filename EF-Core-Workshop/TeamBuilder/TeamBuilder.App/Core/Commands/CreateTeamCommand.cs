namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
    using Services;
    using Services.Contracts;
    using Utilities;

    public class CreateTeamCommand:ICommand
    {
        private readonly ITeamService teamService;

        public CreateTeamCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        //•	CreateTeam <name> <acronym> <description>
        public string Execute(string cmd, params string[] args)
        {
            // Validate input length
            if (args.Length != 2 && args.Length != 3)
            {
                throw new ArgumentOutOfRangeException(nameof(args));
            }

            //check is team exist
            var teamName = args[0];
            if (CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(String.Format(Constants.ErrorMessages.TeamExists,teamName));
            }
            
            //validate acronim
            var acronym = args[1];

            if (acronym.Length != 3)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.InvalidAcronym, acronym));
            }

            //check for logged user
            if (Session.User==null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
            
            //check for description optional
            var description = args.Length == 3 ? args[2] : null;

            return this.teamService.CreateTeam(teamName,acronym,description);
        }
    }
}
