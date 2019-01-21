using Kinvey;
using Newtonsoft.Json;

namespace KinveyLiveServicesXamarinAutomaticReconnection
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Book : Entity
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("genre")]
        public string Genre { get; set; }

        [JsonProperty("publish_date")]
        public string PublishDate { get; set; }
    }
}
