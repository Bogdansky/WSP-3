using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ПWS_3.Helpers
{
    public class StudentFilter
    {
        public int MinId { get; set; }
        public int MaxId { get; set; }
        public string Like { get; set; }
        public string GlobalLike { get; set; }
    }
}