namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
    using Services.Contracts;
    using Utilities;

    public class ShowTeamCommand:ICommand
    {
        private readonly ITeamService teamService;

        public ShowTeamCommand(ITeamService teamService)
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

            return this.teamService.ShowTeam(teamName);
        }
    }
}
