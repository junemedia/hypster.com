using Newtonsoft.Json;

namespace hypster.Models
{
    public class Contact
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string email { get; set; }
    }
}