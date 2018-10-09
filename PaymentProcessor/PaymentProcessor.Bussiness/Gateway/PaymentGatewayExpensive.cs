using PaymentProcessor.Bussiness.Entities;
using PaymentProcessor.Bussiness.Utilities;
using System;

namespace PaymentProcessor.Bussiness.Gateway
{
    public class PaymentGatewayExpensive : IPaymentGatewayExpensive
    {
        public Payment Payment { get; set; }

        private readonly IPaymentGatewayCheap _paymentGatewayCheap;
        private readonly IPaymentValidator _paymentValidator;
        private readonly IExternalProcessor _external;

        public PaymentGatewayExpensive(IPaymentGatewayCheap paymentGatewayCheap, IPaymentValidator paymentValidator, IExternalProcessor external)
        {
            _paymentGatewayCheap = paymentGatewayCheap;
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

            RetryProcessPayment();
        }

        public void RetryProcessPayment()
        {
            _paymentGatewayCheap.ProcessPayment(true);
        }

        public bool ValidatePaymentForGateway()
        {
            return _paymentValidator.ValidatePayment(Payment) && Payment.Amount > 20 && Payment.Amount <= 500;
        }
    }
}
