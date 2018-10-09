using NUnit.Framework;
using Moq;
using PaymentProcessor.Bussiness.Gateway;
using PaymentProcessor.Bussiness.Utilities;
using PaymentProcessor.Bussiness.Entities;

namespace PaymentProcessor.Bussiness.Tests.Gateway
{
    [TestFixture]
    public class PaymentGatewayExpensiveTests
    {
        private IPaymentGatewayExpensive subject;
        private Mock<IPaymentValidator> paymentValidatorMock;
        private Mock<IPaymentGatewayCheap> paymentGatewayCheapMock;
        private Mock<IExternalProcessor> externalProcessorMock;

        [SetUp]
        public void SetUp()
        {
            paymentValidatorMock = new Mock<IPaymentValidator>();
            paymentGatewayCheapMock = new Mock<IPaymentGatewayCheap>();
            externalProcessorMock = new Mock<IExternalProcessor>();

            paymentValidatorMock.Setup(x => x.ValidatePayment(It.IsAny<Payment>())).Returns(true);
            paymentGatewayCheapMock.Setup(x => x.ProcessPayment(true)).Verifiable();

            subject = new PaymentGatewayExpensive(paymentGatewayCheapMock.Object, paymentValidatorMock.Object, externalProcessorMock.Object);
        }

        [Test]
        public void ProcessPayment_WithValidData_ShouldProcessSuccessfully()
        {
            subject.Payment = new Payment
            {
                CreditCard = new CreditCard(),
                Amount = 100
            };

            Assert.DoesNotThrow(() => subject.ProcessPayment(), "ProcessPayment_InvalidPayment");
            paymentGatewayCheapMock.Verify(x => x.ProcessPayment(true), Times.Once);
        }

        [TestCase(1)]
        [TestCase(20)]
        [TestCase(501)]
        [TestCase(545555)]
        public void ValidatePaymentForGateway_WithInvalidData_ShouldReturnFalse(decimal amount)
        {
            subject.Payment = new Payment
            {
                CreditCard = new CreditCard(),
                Amount = amount
            };

            Assert.IsFalse(subject.ValidatePaymentForGateway());
            Assert.IsFalse(GatewayType.GetGatewayTypeFromAmount(amount) == typeof(PaymentGatewayExpensive));
        }

        [TestCase(21)]
        [TestCase(100)]
        [TestCase(500)]
        public void ValidatePaymentForGateway_WithValidData_ShouldReturnTrue(decimal amount)
        {
            subject.Payment = new Payment
            {
                CreditCard = new CreditCard(),
                Amount = amount
            };

            Assert.IsTrue(subject.ValidatePaymentForGateway());
            Assert.IsTrue(GatewayType.GetGatewayTypeFromAmount(amount) == typeof(PaymentGatewayExpensive));
        }
    }
}
