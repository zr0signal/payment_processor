using Autofac;
using PaymentProcessor.Api.Models;
using PaymentProcessor.Bussiness;
using PaymentProcessor.Bussiness.Entities;
using PaymentProcessor.Bussiness.Gateway;
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

            var payment = GetPaymentFromDetails(transactionDetails);
            var gatewayType = GatewayType.GetGatewayTypeFromAmount(payment.Amount);

            using (var scope = ContainerConfig.Container.BeginLifetimeScope(x => 
            {
                x.RegisterType(gatewayType).As<IPaymentGateway>();
            }))
            {
                var logic = scope.Resolve<IPaymentProcessorLogic>();
                logic.ProcessPayment(payment);
            }

            return Ok(new TransactionResult { Message = "OK" });
        }

        private Payment GetPaymentFromDetails(TransactionDetails transactionDetails)
        {
            var card = new CreditCard
            {
                CardHolder = transactionDetails.CardHolder,
                CreditCardNumber = transactionDetails.CreditCardNumber,
                SecurityCode = transactionDetails.SecurityCode,
                ExpirationDate = transactionDetails.ExpirationDate
            };

            return new Payment
            {
                CreditCard = card,
                Amount = transactionDetails.Amount
            };
        }
    }
}
