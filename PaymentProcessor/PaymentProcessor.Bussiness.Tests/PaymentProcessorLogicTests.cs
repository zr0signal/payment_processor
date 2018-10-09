using NUnit.Framework;
using Moq;
using PaymentProcessor.Bussiness.Gateway;
using PaymentProcessor.Bussiness.Entities;

namespace PaymentProcessor.Bussiness.Tests
{
    [TestFixture]
    public class PaymentProcessorLogicTests
    {
        private IPaymentProcessorLogic subject;
        private Mock<IPaymentGateway> paymentGatewayMock0;
        private Mock<IPaymentGatewayCheap> paymentGatewayMock1;
        private Mock<IPaymentGatewayExpensive> paymentGatewayMock2;
        private Mock<IPaymentGatewayPremium> paymentGatewayMock3;
        private Payment payment;

        [SetUp]
        public void SetUp()
        {
            paymentGatewayMock0 = new Mock<IPaymentGateway>();
            paymentGatewayMock1 = new Mock<IPaymentGatewayCheap>();
            paymentGatewayMock2 = new Mock<IPaymentGatewayExpensive>();
            paymentGatewayMock3 = new Mock<IPaymentGatewayPremium>();

            paymentGatewayMock0.Setup(x => x.ProcessPayment(false)).Verifiable();
            paymentGatewayMock1.Setup(x => x.ProcessPayment(false)).Verifiable();
            paymentGatewayMock2.Setup(x => x.ProcessPayment(false)).Verifiable();
            paymentGatewayMock3.Setup(x => x.ProcessPayment(false)).Verifiable();

            payment = new Payment
            {
                Amount = 1
            };

            paymentGatewayMock0.Setup(x => x.Payment).Returns(payment);
            paymentGatewayMock1.Setup(x => x.Payment).Returns(payment);
            paymentGatewayMock2.Setup(x => x.Payment).Returns(payment);
            paymentGatewayMock3.Setup(x => x.Payment).Returns(payment);
        }

        [Test]
        public void ProcessPayment_WithGenericGateway_ShouldProcessPayment()
        {
            subject = new PaymentProcessorLogic(paymentGatewayMock0.Object);

            subject.ProcessPayment(payment);

            Assert.AreSame(payment, paymentGatewayMock0.Object.Payment);
            paymentGatewayMock0.Verify(x => x.ProcessPayment(false), Times.Once);
        }

        [Test]
        public void ProcessPayment_WithCheapGateway_ShouldProcessPayment()
        {
            subject = new PaymentProcessorLogic(paymentGatewayMock1.Object);

            subject.ProcessPayment(payment);

            Assert.AreSame(payment, paymentGatewayMock1.Object.Payment);
            paymentGatewayMock1.Verify(x => x.ProcessPayment(false), Times.Once);
        }

        [Test]
        public void ProcessPayment_WithExpensiveGateway_ShouldProcessPayment()
        {
            subject = new PaymentProcessorLogic(paymentGatewayMock2.Object);

            subject.ProcessPayment(payment);

            Assert.AreSame(payment, paymentGatewayMock2.Object.Payment);
            paymentGatewayMock2.Verify(x => x.ProcessPayment(false), Times.Once);
        }

        [Test]
        public void ProcessPayment_WithPremiumGateway_ShouldProcessPayment()
        {
            subject = new PaymentProcessorLogic(paymentGatewayMock3.Object);

            subject.ProcessPayment(payment);

            Assert.AreSame(payment, paymentGatewayMock3.Object.Payment);
            paymentGatewayMock3.Verify(x => x.ProcessPayment(false), Times.Once);
        }
    }
}
