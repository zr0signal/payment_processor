using PaymentProcessor.Bussiness.Entities;

namespace PaymentProcessor.Bussiness.Gateway
{
    public interface IPaymentGateway
    {
        Payment Payment { get; set; }

        void ProcessPayment(bool skipGatewayValidation = false);
        bool ValidatePaymentForGateway();
    }
}
