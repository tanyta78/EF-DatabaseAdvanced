namespace TeamBuilder.App.Core.Utilities
{
    using System;
    using System.Linq;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Services;

    public class CommandHelper
    {
        public static bool IsTeamExisting(string teamName)
        {
            using (TeamBuilderContext db = new TeamBuilderContext())
            {
                var result= db.Teams.Any(t => t.Name == teamName);
                return result;
            }
        }

        public static bool IsUserExisting(string username)
        {
            using (TeamBuilderContext db = new TeamBuilderContext())
            {
                var result=db.Users.Any(t => t.Username == username && t.IsDeleted==false);
                return result;
            }
        }

        public static bool IsInviteExisting(string teamName, string username)
        {
            using (TeamBuilderContext db = new TeamBuilderContext())
            {
                return db.Invitations.Any(i => i.Team.Name == teamName && i.InvitedUser.Username == username && i.IsActive);
            }
        }

        public static bool IsEventExisting(string eventName)
        {
            using (TeamBuilderContext db = new TeamBuilderContext())
            {
                return db.Events.Any(e => e.Name == eventName);
            }
        }


        public static bool IsMemberOfTeam(string teamName, string username)
        {
            using (TeamBuilderContext db = new TeamBuilderContext())
            {
                return db.Teams
                    .Include(t=>t.Members)
                    .ThenInclude(m=>m.User)
                    .Single(t => t.Name == teamName)
                    .Members.Any(tm => tm.User.Username == username);

            }
        }

        public static bool IsUserCreatorOfTeam(string teamName, User user)
        {
            using (TeamBuilderContext db = new TeamBuilderContext())
            {
                return db.Teams
                    .Any(t => t.Name == teamName && t.Creator.Id == user.Id);
            }
        }

        public static bool IsUserCreatorOfEvent(string eventName, User user)
        {
            using (TeamBuilderContext db = new TeamBuilderContext())
            {
                return db.Events.Any(createdEvent =>
                    createdEvent.Name == eventName &&
                    createdEvent.CreatorId == user.Id);
            }
        }

        public static bool IsTeamEventExist(string eventName, string teamName)
        {
            using (TeamBuilderContext db = new TeamBuilderContext())
            {
                var currentEvent = db.Events
                    .Include(e => e.ParticipatingTeams)
                    .ThenInclude(pt => pt.Team)
                    .OrderByDescending(ev => ev.StartDate)
                    .FirstOrDefault(ev => ev.Name == eventName);
                return  currentEvent.ParticipatingTeams.Any(pt => pt.Team.Name == teamName);

            }
        }


        public static bool IsCreatorOrMemberOfTeam(string teamName, User loggedUser)
        {
            using (TeamBuilderContext db = new TeamBuilderContext())
            {
                return db.Teams
                    .Include(team => team.Members)
                    .ThenInclude(ut=>ut.User)
                    .Any(team => team.Name == teamName &&
                                 (team.CreatorId == loggedUser.Id || team.Members.Any(member => member.User.Username == loggedUser.Username)));
                
            }
        }

        public static bool IsUserCreatorOfTeam(string teamName, string userName)
        {
            using (TeamBuilderContext db = new TeamBuilderContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == userName);
                return db.Teams
                    .Any(t => t.Name == teamName && t.Creator.Id == user.Id);
            }
        }
    }
}
