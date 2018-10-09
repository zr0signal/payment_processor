using NUnit.Framework;
using Moq;
using PaymentProcessor.Bussiness.Gateway;
using PaymentProcessor.Bussiness.Utilities;
using PaymentProcessor.Bussiness.Entities;

namespace PaymentProcessor.Bussiness.Tests.Gateway
{
    [TestFixture]
    public class PaymentGatewayCheapTests
    {
        private IPaymentGatewayCheap subject;
        private Mock<IPaymentValidator> paymentValidatorMock;

        [SetUp]
        public void SetUp()
        {
            paymentValidatorMock = new Mock<IPaymentValidator>();

            paymentValidatorMock.Setup(x => x.ValidatePayment(It.IsAny<Payment>())).Returns(true);

            subject = new PaymentGatewayCheap(paymentValidatorMock.Object);
        }

        [Test]
        public void ProcessPayment_WithValidData_ShouldProcessSuccessfully()
        {
            subject.Payment = new Payment
            {
                CreditCard = new CreditCard(),
                Amount = 10
            };

            Assert.DoesNotThrow(() => subject.ProcessPayment(), "ProcessPayment_InvalidPayment");
        }

        [TestCase(21)]
        [TestCase(133)]
        [TestCase(545555)]
        public void ValidatePaymentForGateway_WithInvalidData_ShouldReturnFalse(decimal amount)
        {
            subject.Payment = new Payment
            {
                CreditCard = new CreditCard(),
                Amount = amount
            };

            Assert.IsFalse(subject.ValidatePaymentForGateway());
            Assert.IsFalse(GatewayType.GetGatewayTypeFromAmount(amount) == typeof(PaymentGatewayCheap));
        }

        [TestCase(1)]
        [TestCase(10)]
        [TestCase(20)]
        public void ValidatePaymentForGateway_WithValidData_ShouldReturnTrue(decimal amount)
        {
            subject.Payment = new Payment
            {
                CreditCard = new CreditCard(),
                Amount = amount
            };

            Assert.IsTrue(subject.ValidatePaymentForGateway());
            Assert.IsTrue(GatewayType.GetGatewayTypeFromAmount(amount) == typeof(PaymentGatewayCheap));
        }
    }
}
