using System;
using System.Collections.Generic;
using System.Linq;

namespace FTG
{
    public class Team
    {
        private string name;
        private List<Player> players;

        public Team(string name)
        {
            this.Name = name;
            this.Players = new List<Player>();
        }

        public string Name
        {
            get { return this.name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("A name should not be empty.");
                }

                this.name = value;
            }
        }

        public int Rating
        {
            get { return CalculateTeamRating(); }
        }

        private int CalculateTeamRating()
        {
            return this.players.Any() ? (int) Math.Round(this.players.Average(p => p.Stats)) : 0;
        }

        public List<Player> Players
        {
            get { return this.players; }
            set { this.players = value; }
        }

        public void AddPlayer(Player player)
        {
            this.players.Add(player);
        }

        public void RemovePlayer(string player)
        {
            if (this.players.All(p => p.Name != player))
            {
                throw new ArgumentException($"Player {player} is not in {this.Name} team. ");
            }

            Player currentPlayer = this.players.FirstOrDefault(p => p.Name == player);
            this.players.Remove(currentPlayer);
        }

        public override string ToString()
        {
            return $"{this.name} - {this.Rating}";
        }
    }
}
