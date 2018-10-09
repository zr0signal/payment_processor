using PaymentProcessor.Bussiness.Entities;
using PaymentProcessor.Bussiness.Utilities;
using System;
using System.Threading;

namespace PaymentProcessor.Bussiness.Gateway
{
    public class PaymentGatewayExpensive : IPaymentGatewayExpensive
    {
        public Payment Payment { get; set; }

        private readonly IPaymentGatewayCheap _paymentGatewayCheap;
        private readonly IPaymentValidator _paymentValidator;

        public PaymentGatewayExpensive(IPaymentGatewayCheap paymentGatewayCheap, IPaymentValidator paymentValidator)
        {
            _paymentGatewayCheap = paymentGatewayCheap;
            _paymentValidator = paymentValidator;
        }

        public void ProcessPayment(bool skipGatewayValidation = false)
        {
            if (!skipGatewayValidation && !ValidatePaymentForGateway())
            {
                throw new Exception("ProcessPayment_InvalidPayment");
            }

            Thread.Sleep(10);

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
