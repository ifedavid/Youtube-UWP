using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace App7.Models
{
    public class YoutubeModel
    {
        //
        // Summary:
        //     Serialized EventId of the request which produced this response.
        [JsonProperty("eventId")]
        public virtual string EventId { get; set; }
        //
        // Summary:
        //     A list of results that match the search criteria.
        [JsonProperty("items")]
        public virtual IList<SearchResults> Items { get; set; }
        //
        // Summary:
        //     Identifies what kind of resource this is. Value: the fixed string "youtube#searchListResponse".
        [JsonProperty("kind")]
        public virtual string Kind { get; set; }

        public string NextPageToken { get; set; }
        public string PrevPageToken { get; set; }
    }
}
