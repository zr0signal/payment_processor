namespace PaymentProcessor.Bussiness.Entities
{
    public class Payment
    {
        public CreditCard CreditCard { get; set; }
        
        public decimal Amount { get; set; }
    }
}
