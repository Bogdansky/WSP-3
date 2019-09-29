using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ПWS_3.Helpers
{
    public class PaginationFilter
    {
        public int Limit { get; set; }
        public bool Sort { get; set; }
        public int Offset { get; set; }
    }
}