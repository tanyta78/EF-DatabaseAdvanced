namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
    using Services;
    using Services.Contracts;
    using Utilities;

    public class KickMemberCommand : ICommand
    {
        private readonly ITeamService teamService;

        public KickMemberCommand(ITeamService teamService)
        {
            this.teamService = teamService;
        }

        // KickMember<teamName> <username>
        // Removes specified user member from given team.Only the creator of the team can kick other members.
        public string Execute(string cmd, params string[] args)
        {
            // Validate input length
            Check.CheckLength(2, args);

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

            string userName = args[1];
            //is user exist
            if (!CommandHelper.IsUserExisting(userName))
            {
               throw new ArgumentException(String.Format(Constants.ErrorMessages.UserNotFound,userName));
            }
            
            //is user a member
            if (!CommandHelper.IsMemberOfTeam(teamName,userName))
            {
                throw new ArgumentException(String.Format(Constants.ErrorMessages.NotPartOfTeam,userName,teamName)); 
            }
            
            //is current user is creator of team
            if (!CommandHelper.IsUserCreatorOfTeam(teamName,Session.User))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }
            
            //the user to be kicked is the creator
            if (CommandHelper.IsUserCreatorOfTeam(teamName,userName))
            {
                throw new InvalidOperationException(String.Format(Constants.ErrorMessages.CommandNotAllowed, "DisbandTeam"));
            }

            return this.teamService.KickMember(teamName,userName);
        }
    }
}
