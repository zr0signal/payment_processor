using PaymentProcessor.Bussiness.Entities;
using PaymentProcessor.Bussiness.Utilities;
using System;
using System.Threading;

namespace PaymentProcessor.Bussiness.Gateway
{
    public class PaymentGatewayPremium : IPaymentGatewayPremium
    {
        public Payment Payment { get; set; }

        private readonly IPaymentValidator _paymentValidator;
        private readonly IExternalProcessor _external;

        public PaymentGatewayPremium(IPaymentValidator paymentValidator, IExternalProcessor external)
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

            PrivateProcessing();
            RetryProcessPayment();
        }
        
        public void RetryProcessPayment()
        {
            PrivateProcessing();
            PrivateProcessing();
            PrivateProcessing();
        }

        public bool ValidatePaymentForGateway()
        {
            return _paymentValidator.ValidatePayment(Payment) && Payment.Amount > 500;
        }

        private void PrivateProcessing()
        {
            _external.Process();
        }
    }
}
