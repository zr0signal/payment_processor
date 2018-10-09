using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentProcessor.Bussiness.Gateway
{
    public interface IRetryPayment
    {
        void RetryProcessPayment();
    }
}
