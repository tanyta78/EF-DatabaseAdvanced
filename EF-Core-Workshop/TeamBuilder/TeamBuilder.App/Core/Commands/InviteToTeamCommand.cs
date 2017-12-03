namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
    using Services;
    using Services.Contracts;
    using Utilities;

    public class InviteToTeamCommand:ICommand
    {
        private readonly IInvitationService invitationService;

        public InviteToTeamCommand(IInvitationService invitationService)
        {
            this.invitationService = invitationService;
        }
       
        //•	InviteToTeam<teamName> <username>
       // Sends an invite to the specified user to join given team.If the user is actually the creator of the team – add him/her directly!

        public string Execute(string cmd, params string[] args)
        {
            // Validate input length
            Check.CheckLength(2, args);

            //check for loggin user
            if (Session.User == null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }

            //check is userToInvite exist
            var teamname = args[0];
            var username = args[1];

            if (!CommandHelper.IsUserExisting(username))
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamOrUserNotExist);
            }

            if (!CommandHelper.IsTeamExisting(teamname))
            {
                throw new ArgumentException(Constants.ErrorMessages.TeamOrUserNotExist);
            }

            //check
           
            if (!CommandHelper.IsCreatorOrMemberOfTeam(teamname, Session.User))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }
            if (CommandHelper.IsMemberOfTeam(teamname, username))
            {
                throw new InvalidOperationException(Constants.ErrorMessages.NotAllowed);
            }
            


            //check for active invite
            if (CommandHelper.IsInviteExisting(teamname, username))
            {
              throw new InvalidOperationException(Constants.ErrorMessages.InviteIsAlreadySent);
            }

            return this.invitationService.InviteToTeam(teamname, username);
        }
    }
}
