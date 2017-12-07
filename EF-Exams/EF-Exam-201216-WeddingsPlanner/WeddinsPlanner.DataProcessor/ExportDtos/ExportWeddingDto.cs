namespace WeddinsPlanner.DataProcessor.ExportDtos
{
    using System.Collections.Generic;
    using ImportDtos;
    using Newtonsoft.Json;

    public class ExportWeddingDto
    {
        [JsonProperty(PropertyName = "bride")]
        public string Bride { get; set; }


        [JsonProperty(PropertyName = "bridegroom")]
        public string Bridegroom { get; set; }

        [JsonProperty(PropertyName = "agency")]
        public ExportAgencyDto Agency { get; set; }

        [JsonProperty(PropertyName = "invitedGuests")]
        public int InvitedGuests { get; set; }

        [JsonProperty(PropertyName = "brideGuests")]
        public int BrideGuests { get; set; }

        [JsonProperty(PropertyName = "bridegroomGuests")]
        public int BridegroomGuests { get; set; }

        [JsonProperty(PropertyName = "attendingGuests")]
        public int AttendingGuests { get; set; }

        [JsonProperty(PropertyName = "guests")]
        public List<string> Guests { get; set; }
    }
}