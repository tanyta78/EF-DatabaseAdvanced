namespace TeamBuilder.Services
{
    using System.Linq;
    using Contracts;
    using Data;
    using Models;

    public class InvitationService : IInvitationService
    {
        private readonly TeamBuilderContext db;

        public InvitationService(TeamBuilderContext db)
        {
            this.db = db;
        }

        //•	InviteToTeam <teamName> <username>
        //Sends an invite to the specified user to join given team.If the user is actually the creator of the team – add him/her directly!

        public string InviteToTeam(string teamName, string userName)
        {
            int invitedUserId = this.db.Users.FirstOrDefault(u => u.Username == userName).Id;
            int teamId = this.db.Teams.FirstOrDefault(t => t.Name == teamName).Id;

            var invitation = new Invitation
            {
                InvitedUserId = invitedUserId,
                TeamId = teamId
            };

            this.db.Invitations.Add(invitation);
            this.db.SaveChanges();

            return $"Team {teamName} invited {userName}!";
        }

        public string AcceptInvite(string teamName)
        {
            var user = Session.User;
            var team = this.db.Teams.SingleOrDefault(t => t.Name == teamName);

            var userTeam = new UserTeam
            {
                Team = team,
                User = user
            };

            //this.db.Users.Attach(user);
            //this.db.Teams.Attach(team);
            user.Teams.Add(userTeam);
            team.Members.Add(userTeam);
            this.db.UserTeams.Add(userTeam);
            var invitation =
                this.db.Invitations.SingleOrDefault(
                    i => i.InvitedUserId == user.Id && i.TeamId == team.Id && i.IsActive);
            invitation.IsActive = false;
            this.db.SaveChanges();
            return $"User {user.Username} joined team {teamName}!";
        }

        public string DeclineInvite(string teamName)
        {
            var user = Session.User;
            var team = this.db.Teams.SingleOrDefault(t => t.Name == teamName);
            var invitation =
                this.db.Invitations.SingleOrDefault(
                    i => i.InvitedUserId == user.Id && i.TeamId == team.Id && i.IsActive);
            invitation.IsActive = false;
            this.db.SaveChanges();
            return $"Invite from {teamName} declined.";
        }
    }
}
