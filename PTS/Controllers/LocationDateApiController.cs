using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PTS.Controllers
{
    public class LocationDateApiController : ApiController
    {
        [HttpGet]
        public string Index()
        {
            return "LocationDatevalue";
        }
    }
}
