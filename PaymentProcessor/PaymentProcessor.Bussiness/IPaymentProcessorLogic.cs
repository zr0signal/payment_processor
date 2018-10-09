using PaymentProcessor.Bussiness.Entities;

namespace PaymentProcessor.Bussiness
{
    public interface IPaymentProcessorLogic
    {
        void ProcessPayment(Payment payment);
    }
}
