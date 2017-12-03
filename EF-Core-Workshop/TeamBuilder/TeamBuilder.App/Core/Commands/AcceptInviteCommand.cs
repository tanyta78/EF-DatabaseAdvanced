namespace TeamBuilder.App.Core.Commands
{
    using System;
    using Contracts;
    using Services;
    using Services.Contracts;
    using Utilities;

    public class AcceptInviteCommand:ICommand
    {
        private readonly IInvitationService invitationService;

        public AcceptInviteCommand(IInvitationService invitationService)
        {
            this.invitationService = invitationService;
        }
        

        //•	AcceptInvite<teamName>
        //Checks current user’s active invites and accepts the one from the team specified.

        public string Execute(string cmd, params string[] args)
        {
            // Validate input length
            Check.CheckLength(1, args);

            string teamName=args[0];
            //is team exist
            if (!CommandHelper.IsTeamExisting(teamName))
            {
                throw new ArgumentException(String.Format(Constants.ErrorMessages.TeamNotFound,teamName));
            }

            //is there logged user
            if (Session.User==null)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
            
            //is there a invite from team
            if (!CommandHelper.IsInviteExisting(teamName,Session.User.Username))
            {
               throw new ArgumentException(String.Format(Constants.ErrorMessages.InviteNotFound,teamName));
            }
            
            return this.invitationService.AcceptInvite(teamName);
        }
    }
}
