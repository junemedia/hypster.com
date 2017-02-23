using Newtonsoft.Json;

namespace hypster.Models
{
    public class SendEMail
    {
        public SendEMail() { }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string campaign_id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string content_id { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Contact contact { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Tags tags { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Response response { get; set; }
    }
}