using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace PaymentProcessor.Api.Controllers
{
    public class TransactionController : ApiController
    {
        [AcceptVerbs("GET")]
        [ResponseType(typeof(string))]
        public IHttpActionResult ProcessPayment(string ccn, string name)
        {
            return Ok("Hello World");
        }
    }
}
