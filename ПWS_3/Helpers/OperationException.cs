using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ПWS_3.Helpers
{
    public class OperationException : Exception
    {
        public readonly string StatusCode = "400";
        public ErrorEnum Status { get; set; }
        public OperationException(ErrorEnum status) : base()
        {
            Status = status;
        }
    }
}