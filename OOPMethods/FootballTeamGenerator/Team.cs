namespace FootballTeamGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Team
   {
       private string name;
       private ICollection<Player> players;
       private int rating;

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
               if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
               {
                   throw new ArgumentException("A name should not be empty.");
               }
               this.name = value;
           }
       }

       public int Rating
       {
           get { return (int) Math.Round(this.Players.Sum(p => p.Stats.Average(s => s.Value))); }
          
       }

       public ICollection<Player> Players
       {
           get { return this.players; }
           set { this.players = value; }
       }

       internal void AddPlayer(Player player)
       {
           this.Players.Add(player);
       }

       internal void RemovePlayer(Player player)
       {
           if (!this.Players.Contains(player))
           {
               throw new ArgumentException($"Player {player.Name} is not in {this.Name} team.");
           }

           this.Players.Remove(player);
       }


    }
}
