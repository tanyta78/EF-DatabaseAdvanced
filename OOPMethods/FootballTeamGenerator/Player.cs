namespace FootballTeamGenerator
{
    using System;
    using System.Collections.Generic;

    public class Player
    {
        private string name;
        private ICollection<Stat> stats;

        public Player(string name)
        {
            this.Name = name;
            this.Stats = new List<Stat>();
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrEmpty(value)||string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }

                this.name = value;
            }
        }

        public ICollection<Stat> Stats
        {
            get { return this.stats; }
            set { this.stats = value; }
        }
    }
}
