namespace FootballTeamGenerator
{
    using System;

    public abstract class Stat
    {
        private int value;

        public Stat(int value)
        {
            this.Value = value;
        }

        public int Value
        {
            get { return this.value; }
            protected set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException($"{this.GetType().Name} should be between 0 and 100.");
                }

                this.value = value;
            }
        }
    }
}
