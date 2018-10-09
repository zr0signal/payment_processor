using PaymentProcessor.Bussiness.Entities;
using PaymentProcessor.Bussiness.Utilities;
using System;

namespace PaymentProcessor.Bussiness.Gateway
{
    public class PaymentGatewayCheap : IPaymentGatewayCheap
    {
        public Payment Payment { get; set; }

        private readonly IPaymentValidator _paymentValidator;
        private readonly IExternalProcessor _external;

        public PaymentGatewayCheap(IPaymentValidator paymentValidator, IExternalProcessor external)
        {
            _paymentValidator = paymentValidator;
            _external = external;
        }

        public void ProcessPayment(bool skipGatewayValidation = false)
        {
            if (!skipGatewayValidation && !ValidatePaymentForGateway())
            {
                throw new Exception("ProcessPayment_InvalidPayment");
            }

            _external.Process();
        }

        public bool ValidatePaymentForGateway()
        {
            return _paymentValidator.ValidatePayment(Payment) && Payment.Amount <= 20;
        }
    }
}
