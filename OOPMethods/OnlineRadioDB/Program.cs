using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRadioDB
{
    public class Program
    {
        public static void Main()
        {
            int numberOfSongs = int.Parse(Console.ReadLine());
            var playlist = new List<Song>();

            for (int i = 0; i < numberOfSongs; i++)
            {
                string[] info = Console.ReadLine().Split(';');

                try
                {
                    var author = info[0];
                    var songName = info[1];

                    var time = info[2].Split(':');
                    int minutes;
                    int seconds;
                    if (int.TryParse(time[0], out minutes) && int.TryParse(time[1], out seconds))
                    {
                        playlist.Add(new Song(author, songName, minutes, seconds));
                        Console.WriteLine("Song added.");
                    }
                    else
                    {
                        throw new InvalidSongLengthException();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            var durationInSeconds = playlist.Sum(s => s.Seconds) + playlist.Sum(s => s.Minutes) * 60;

            var totalMinutes = durationInSeconds / 60;
            var totalSeconds = durationInSeconds % 60;
            var hours = totalMinutes / 60;
            totalMinutes %= 60;

            Console.WriteLine($"Songs added: {playlist.Count}");
            Console.WriteLine($"Playlist length: {hours}h {totalMinutes}m {totalSeconds}s");
        }
    }
}

