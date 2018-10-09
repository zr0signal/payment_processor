using System;
using PaymentProcessor.Bussiness.Entities;

namespace PaymentProcessor.Bussiness.Utilities
{
    public class CreditCardValidator : ICreditCardValidator
    {
        public bool ValidateCard(CreditCard card)
        {
            return !string.IsNullOrWhiteSpace(card?.CardHolder) && 
                ValidateCcn(card.CreditCardNumber) && 
                ValidateExpirationDate(card.ExpirationDate) && 
                ValidateSecurityCode(card.SecurityCode);
        }

        protected virtual bool ValidateCcn(string ccn)
        {
            return !string.IsNullOrWhiteSpace(ccn) && ccn.Length == 4;
        }

        protected virtual bool ValidateExpirationDate(DateTime date)
        {
            return date > DateTime.UtcNow;
        }

        protected virtual bool ValidateSecurityCode(string code)
        {
            return string.IsNullOrWhiteSpace(code) || code.Length == 3;
        }
    }
}
