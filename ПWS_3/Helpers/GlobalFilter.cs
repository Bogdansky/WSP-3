using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ПWS_3.Helpers
{
    public class GlobalFilter
    {
        public StudentFilter Filter { get; set; }
        public PaginationFilter Pagination { get; set; }
    }
}