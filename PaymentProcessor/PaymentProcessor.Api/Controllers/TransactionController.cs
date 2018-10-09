using PaymentProcessor.Api.Models;
using System.Web.Http;
using System.Web.Http.Description;

namespace PaymentProcessor.Api.Controllers
{
    public class TransactionController : ApiController
    {
        [AcceptVerbs("POST", "PUT")]
        [Route("payment/process")]
        [ResponseType(typeof(TransactionResult))]
        public IHttpActionResult ProcessPayment([FromBody] TransactionDetails transactionDetails)
        {
            if (transactionDetails == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            // TODO: call back-end

            return Ok(new TransactionResult { Message = "OK" });
        }
    }
}
