namespace Stations.DataProcessor.Dto.Import
{
    using System.Xml.Serialization;

    [XmlType("Trip")]
    public class TicketTripDto
    {
        public string OriginStation { get; set; }
        public string DestinationStation { get; set; }
        public string DepartureTime { get; set; }

    }
}