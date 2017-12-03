namespace TeamBuilder.Services
{
    using System;
    using Contracts;
    using Data;
    using Models;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class TeamService : ITeamService
    {
        private readonly TeamBuilderContext db;

        public TeamService(TeamBuilderContext db)
        {
            this.db = db;
        }

        //•	CreateTeam <name> <acronym> <description>
        // Creates a team(currently logged user is it’s creator). Description is optional.

        public string CreateTeam(string teamName, string acronim, string description)
        {
            User creator = Session.User;

            var team = new Team()
            {
                CreatorId = creator.Id,
                Name = teamName,
                Acronym = acronim,
                Description = description
            };

            this.db.Users.Attach(Session.User);
            creator.CreatedTeams.Add(team);
            this.db.Teams.Add(team);

            this.db.SaveChanges();
            return $"Team {teamName} successfully created!";
        }


        //•	AddTeamTo <eventName> <teamName>
        //Adds given team for event specified.If there are more than one events with same name pick the latest start date.

        public string AddTeamTo(string eventName, string teamName)
        {
            var teamToAdd = db.Teams.SingleOrDefault(team => team.Name == teamName);
            var createdEvent = db.Events
                .OrderByDescending(ev => ev.StartDate)
                .FirstOrDefault(ev => ev.Name == eventName);

            var teamEvent = new TeamEvent()
            {
                TeamId = teamToAdd.Id,
                EventId = createdEvent.Id

            };

            this.db.TeamEvents.Add(teamEvent);

            teamToAdd.ParticipatedEvents.Add(teamEvent);
            createdEvent.ParticipatingTeams.Add(teamEvent);
            db.SaveChanges();

            return $"Team {teamName} added for {eventName}!";
        }

       public string KickMember(string teamName, string userName)
       {
           var userTeam =
               this.db.UserTeams.FirstOrDefault(ut => ut.Team.Name == teamName && ut.User.Username == userName);
           this.db.UserTeams.Remove(userTeam);
           this.db.SaveChanges();
           return $"User {userName} was kicked from {teamName}!";

       }

        public string Disband(string teamName)
        {
            var team = db.Teams.SingleOrDefault(t => t.Name == teamName);

            var teamMembers = team.Members;
            var participatedEvents = team.ParticipatedEvents;
            var invitations = team.Invitations;
            
            this.db.UserTeams.RemoveRange(teamMembers);
            this.db.TeamEvents.RemoveRange(participatedEvents);
            this.db.Invitations.RemoveRange(invitations);
            
            this.db.SaveChanges();

            this.db.Teams.Remove(team);
            this.db.SaveChanges();

            return $"{teamName} has disbanded!";
        }

        public string ShowTeam(string teamName)
        {
            var currentTeam = this.db.Teams
               .First(t => t.Name == teamName);

            var teamMembers = this.db.UserTeams
                .Where(ut => ut.TeamId == currentTeam.Id)
                .Include(ut=>ut.User)
                .ToList()
                .Select(ut => $"--{ut.User.Username}");

            var result = $"{currentTeam.Name} {currentTeam.Acronym}" + Environment.NewLine +
                         "Members:" + Environment.NewLine +
                         $"{string.Join(Environment.NewLine, teamMembers)}";

            return result;
        }
    }
}
