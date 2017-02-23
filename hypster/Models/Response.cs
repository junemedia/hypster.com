using Newtonsoft.Json;

namespace hypster.Models
{
    public class Response
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string status_code { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string status_description { get; set; }
    }
}