using PaymentProcessor.Bussiness.Gateway;
using System;

namespace PaymentProcessor.Bussiness
{
    public static class GatewayType
    {
        public static Type GetGatewayTypeFromAmount(decimal amount)
        {
            if (amount <= 20)
            {
                return typeof(PaymentGatewayCheap);
            }
            else if (amount <= 500)
            {
                return typeof(PaymentGatewayExpensive);
            }
            else
            {
                return typeof(PaymentGatewayPremium);
            }
        }
    }
}
