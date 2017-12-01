﻿namespace TeamBuilder.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Reflection.Metadata;

    public class Team
    {
        public Team()
        {
            this.Members=new List<UserTeam>();
            this.ParticipatedEvents=new List<TeamEvent>();
            this.Invitations=new List<Invitation>();
        }
        
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [StringLength(3,MinimumLength = 3)]
        public string Acronym { get; set; }

        public int CreatorId { get; set; }

        public User Creator { get; set; }

        public ICollection<UserTeam> Members { get; set; }

        public ICollection<TeamEvent> ParticipatedEvents { get; set; }

        public ICollection<Invitation> Invitations { get; set; }
    }
}