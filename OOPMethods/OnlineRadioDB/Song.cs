namespace OnlineRadioDB
{
   public class Song
    {
        private string songName;
        private string artistName;
        private int seconds;
        private int minutes;

        public Song(string artistName, string songName, int minutes, int seconds)
        {
            this.ArtistName = artistName;
            this.SongName = songName;
            this.Minutes = minutes;
            this.Seconds = seconds;
        }

        public int Seconds
        {
            get { return this.seconds; }
            set
            {
                if (value > 59 || value < 0)
                {
                    throw new InvalidSongSecondsException();
                }

                this.seconds = value;
            }
        }

        public int Minutes
        {
            get { return this.minutes; }
            set
            {
                if (value > 14 || value < 0)
                {
                    throw new InvalidSongMinutesException();
                }

                this.minutes = value;
            }
        }

        public string SongName
        {
            get { return this.songName; }
            set
            {
                if (value.Length > 30 || value.Length < 3)
                {
                    throw new InvalidSongNameException();
                }

                this.songName = value;
            }
        }

        public string ArtistName
        {
            get { return artistName; }
            set
            {
                if (value.Length > 20 || value.Length < 3)
                {
                    throw new InvalidArtistNameException();
                }

                this.artistName = value;
            }
        }
    }
}
