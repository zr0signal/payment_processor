using PaymentProcessor.Bussiness.Entities;

namespace PaymentProcessor.Bussiness.Utilities
{
    public class PaymentValidator : IPaymentValidator
    {
        private readonly ICreditCardValidator _creditCardValidator;

        public PaymentValidator(ICreditCardValidator creditCardValidator)
        {
            _creditCardValidator = creditCardValidator;
        }

        public bool ValidatePayment(Payment payment)
        {
            return payment != null && payment.Amount > 0 && _creditCardValidator.ValidateCard(payment.CreditCard);
        }
    }
}
