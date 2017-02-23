using Newtonsoft.Json;

namespace hypster.Models
{
    public class Tags
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string username { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string password { get; set; }
    }
}