using System;

namespace PaymentProcessor.Bussiness.Entities
{
    public class CreditCard
    {
        public string CreditCardNumber { get; set; }

        public string CardHolder { get; set; }

        public DateTime ExpirationDate { get; set; } = new DateTime(1970, 1, 1);

        public string SecurityCode { get; set; }
    }
}
