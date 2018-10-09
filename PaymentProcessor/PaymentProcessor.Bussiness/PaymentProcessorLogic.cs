using PaymentProcessor.Bussiness.Entities;
using PaymentProcessor.Bussiness.Gateway;

namespace PaymentProcessor.Bussiness
{
    public class PaymentProcessorLogic : IPaymentProcessorLogic
    {
        private readonly IPaymentGateway _gateway;

        public PaymentProcessorLogic(IPaymentGateway gateway)
        {
            _gateway = gateway;
        }

        public void ProcessPayment(Payment payment)
        {
            _gateway.Payment = payment;
            _gateway.ProcessPayment();
        }
    }
}
