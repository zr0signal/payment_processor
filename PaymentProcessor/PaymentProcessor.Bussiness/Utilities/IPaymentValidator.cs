using PaymentProcessor.Bussiness.Entities;

namespace PaymentProcessor.Bussiness.Utilities
{
    public interface IPaymentValidator
    {
        bool ValidatePayment(Payment payment);
    }
}
