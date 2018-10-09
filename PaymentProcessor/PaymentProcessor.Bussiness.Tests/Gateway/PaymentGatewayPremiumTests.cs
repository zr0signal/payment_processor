using NUnit.Framework;
using Moq;
using PaymentProcessor.Bussiness.Gateway;
using PaymentProcessor.Bussiness.Utilities;
using PaymentProcessor.Bussiness.Entities;

namespace PaymentProcessor.Bussiness.Tests.Gateway
{
    [TestFixture]
    public class PaymentGatewayPremiumTests
    {
        private IPaymentGatewayPremium subject;
        private Mock<IPaymentValidator> paymentValidatorMock;

        [SetUp]
        public void SetUp()
        {
            paymentValidatorMock = new Mock<IPaymentValidator>();

            paymentValidatorMock.Setup(x => x.ValidatePayment(It.IsAny<Payment>())).Returns(true);

            subject = new PaymentGatewayPremium(paymentValidatorMock.Object);
        }

        [Test]
        public void ProcessPayment_WithValidData_ShouldProcessSuccessfully()
        {
            subject.Payment = new Payment
            {
                CreditCard = new CreditCard(),
                Amount = 1000
            };

            Assert.DoesNotThrow(() => subject.ProcessPayment(), "ProcessPayment_InvalidPayment");
        }

        [TestCase(1)]
        [TestCase(20)]
        [TestCase(500)]
        public void ValidatePaymentForGateway_WithInvalidData_ShouldReturnFalse(decimal amount)
        {
            subject.Payment = new Payment
            {
                CreditCard = new CreditCard(),
                Amount = amount
            };

            Assert.IsFalse(subject.ValidatePaymentForGateway());
        }

        [TestCase(501)]
        [TestCase(1000)]
        [TestCase(545555)]
        public void ValidatePaymentForGateway_WithValidData_ShouldReturnTrue(decimal amount)
        {
            subject.Payment = new Payment
            {
                CreditCard = new CreditCard(),
                Amount = amount
            };

            Assert.IsTrue(subject.ValidatePaymentForGateway());
        }
    }
}
