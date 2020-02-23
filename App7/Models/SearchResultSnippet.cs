using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App7.Models
{
   public class SearchResultSnippet
    {
        public DateTime PublishedAt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string ChannelTitle { get; set; }

        public ThumbnailDetail Thumbnails { get; set; }
    }
}
