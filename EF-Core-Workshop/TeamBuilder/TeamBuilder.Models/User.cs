namespace TeamBuilder.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class User
    {
        public User()
        {
            this.CreatedEvents=new List<Event>();
            this.ReceivedInvitations=new List<Invitation>();
            this.Teams=new List<UserTeam>();
            this.CreatedTeams=new List<Team>();
        }
        
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength (25)]
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [StringLength(30,MinimumLength = 6)]
        [RegularExpression("(?=.*[A-Z])(?=.*[0-9]).*")]
        public string Password { get; set; }

        public Gender Gender { get; set; }

        [Range(0,Int32.MaxValue)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Invitation> ReceivedInvitations { get; set; }

        public ICollection<Event> CreatedEvents { get; set; }

        public ICollection<UserTeam> Teams { get; set; }

        public ICollection<Team> CreatedTeams { get; set; }
    }
}
