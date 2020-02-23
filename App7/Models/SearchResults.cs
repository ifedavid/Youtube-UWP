using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App7.Models
{
    public class SearchResults
    {
        public string Kind { get; set; }
        public Resource Id { get; set; }

        public SearchResultSnippet Snippet { get; set; }
    }
}
