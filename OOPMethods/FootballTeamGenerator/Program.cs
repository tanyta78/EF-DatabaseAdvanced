namespace FootballTeamGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            var cmdLine = Console.ReadLine().Split(';').ToArray();
            var teams = new List<Team>();

            while (cmdLine[0] != "END")
            {
                var command = cmdLine[0];
                switch (command)
                {
                    case "Team":
                        CreateTeam(cmdLine, teams);
                        break;
                    case "Add":
                        AddPlayer(cmdLine, teams);
                        break;
                    case "Remove":
                        RemovePlayer(cmdLine, teams);
                        break;
                    case "Rating":
                        Rating(cmdLine, teams);
                        break;


                }

                cmdLine = Console.ReadLine().Split(';').ToArray();
            }


        }

        private static void Rating(string[] cmdLine, List<Team> teams)
        {
            var teamName = cmdLine[1];
            var team = teams.FirstOrDefault(t => t.Name == teamName);
            if (team == null)
            {
                Console.WriteLine($"Team {teamName} does not exist.");
            }

            Console.WriteLine($"{teamName} - {team.Rating}");
        }

        private static void RemovePlayer(string[] cmdLine, List<Team> teams)
        {
            var teamName = cmdLine[1];
            var playerName = cmdLine[2];

            var team = teams.FirstOrDefault(t => t.Name == teamName);
            if (team == null)
            {
                Console.WriteLine($"Team {teamName} does not exist.");
            }
            else
            {
                var player = team.Players.FirstOrDefault(p => p.Name == playerName);
                if (player == null)
                {
                    Console.WriteLine($"Player {playerName} is not in {teamName} team.");
                }
                else
                {
                    team.RemovePlayer(player);
                }
            }

        }

        private static void AddPlayer(string[] cmdLine, List<Team> teams)
        {
            var teamName = cmdLine[1];
            var playerName = cmdLine[2];
            if (!teams.Exists(t => t.Name == teamName))
            {
                Console.WriteLine($"Team {teamName} does not exist.");
            }
            try
            {
                var stats = new List<Stat>();
                Stat endurance = new Endurance(int.Parse(cmdLine[3]));
                stats.Add(endurance);
                Stat sprint = new Sprint(int.Parse(cmdLine[4]));
                stats.Add(sprint);
                Stat dribble = new Dribble(int.Parse(cmdLine[5]));
                stats.Add(dribble);
                Stat passing = new Passing(int.Parse(cmdLine[6]));
                stats.Add(passing);
                Stat shooting = new Shooting(int.Parse(cmdLine[7]));
                stats.Add(shooting);

                Player player = new Player(playerName);
                player.Stats = stats;

                teams.FirstOrDefault(t => t.Name == teamName).AddPlayer(player);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

        }

        private static void CreateTeam(string[] cmdLine, List<Team> teams)
        {
            var teamName = cmdLine[1];
            try
            {
                var team = new Team(teamName);
                teams.Add(team);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
        }
    }
}
