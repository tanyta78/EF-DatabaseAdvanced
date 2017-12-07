namespace WeddinsPlanner.DataProcessor.ExportDtos
{
    using Newtonsoft.Json;

    public class ExportAgencyDto 
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "town")]
        public string Town { get; set; }
    }
}