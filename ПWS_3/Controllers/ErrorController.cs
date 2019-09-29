using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ПWS_3.Helpers;

namespace ПWS_3.Controllers
{
    public class ErrorController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(ErrorEnum id)
        {
            return Ok(new { StatusCode = (int)id, Status = id.GetDescription() });
        }
    }
}
