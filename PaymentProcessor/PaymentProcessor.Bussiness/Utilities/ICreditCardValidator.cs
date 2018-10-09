using PaymentProcessor.Bussiness.Entities;

namespace PaymentProcessor.Bussiness.Utilities
{
    public interface ICreditCardValidator
    {
        bool ValidateCard(CreditCard card);
    }
}
