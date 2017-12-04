namespace Stations.DataProcessor.Dto
{
    using System;

    public class TripDto
    {
        public string TrainNumber { get; set; }

        public TimeSpan? TimeDifference { get; set; }
    }
}
