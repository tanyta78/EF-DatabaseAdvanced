using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTG
{
    class Program
    {
        public static void Main()
        {
            List<Team> teams = new List<Team>();

            var input = string.Empty;

            while ((input = Console.ReadLine()) != "END")
            {
                var args = input.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                var command = args[0];
                try
                {
                    switch (command)
                    {
                        case "Team":
                            teams.Add(new Team(args[1]));
                            break;

                        case "Add":
                            if (!teams.Any(t => t.Name == args[1]))
                            {
                                throw new ArgumentException($"Team {args[1]} does not exist.");
                            }
                            else
                            {
                                var currentTeam = teams.First(t => t.Name == args[1]);
                                currentTeam.AddPlayer(new Player(args[2], int.Parse(args[3]), int.Parse(args[4]),
                                    int.Parse(args[5]), int.Parse(args[6]), int.Parse(args[7])));
                            }
                            break;

                        case "Remove":
                            var teamToRemove = teams.First(t => t.Name == args[1]);
                            teamToRemove.RemovePlayer(args[2]);
                            break;

                        case "Rating":
                            if (!teams.Any(t => t.Name == args[1]))
                            {
                                throw new ArgumentException($"Team {args[1]} does not exist.");
                            }
                            else
                            {
                                Console.WriteLine(teams.First(t => t.Name == args[1]).ToString());
                            }
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
