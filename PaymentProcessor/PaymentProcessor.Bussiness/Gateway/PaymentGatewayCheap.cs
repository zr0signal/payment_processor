using PaymentProcessor.Bussiness.Entities;
using PaymentProcessor.Bussiness.Utilities;
using System;
using System.Threading;

namespace PaymentProcessor.Bussiness.Gateway
{
    public class PaymentGatewayCheap : IPaymentGatewayCheap
    {
        public Payment Payment { get; set; }

        private readonly IPaymentValidator _paymentValidator;

        public PaymentGatewayCheap(IPaymentValidator paymentValidator)
        {
            _paymentValidator = paymentValidator;
        }

        public void ProcessPayment(bool skipGatewayValidation = false)
        {
            if (!skipGatewayValidation && !ValidatePaymentForGateway())
            {
                throw new Exception("ProcessPayment_InvalidPayment");
            }
            
            Thread.Sleep(10);
        }

        public bool ValidatePaymentForGateway()
        {
            return _paymentValidator.ValidatePayment(Payment) && Payment.Amount <= 20;
        }
    }
}
