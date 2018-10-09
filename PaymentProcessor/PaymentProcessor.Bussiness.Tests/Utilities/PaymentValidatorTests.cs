using NUnit.Framework;
using PaymentProcessor.Bussiness.Utilities;
using PaymentProcessor.Bussiness.Entities;
using Moq;

namespace PaymentProcessor.Bussiness.Tests.Gateway
{
    [TestFixture]
    public class PaymentValidatorTests
    {
        private IPaymentValidator subject;
        private Mock<ICreditCardValidator> cardValidatorMock;

        [SetUp]
        public void SetUp()
        {
            cardValidatorMock = new Mock<ICreditCardValidator>();

            cardValidatorMock.Setup(x => x.ValidateCard(It.IsAny<CreditCard>())).Returns(true);

            subject = new PaymentValidator(cardValidatorMock.Object);
        }

        [Test]
        public void ValidatePayment_WithNullPayment_ShouldReturnFalse()
        {
            var result = subject.ValidatePayment(null);

            Assert.IsFalse(result);
        }
        
        [TestCase(0)]
        [TestCase(-0.0000001)]
        [TestCase(-1)]
        [TestCase(-55555.663)]
        public void ValidatePayment_WithInvalidAmount_ShouldReturnFalse(decimal amount)
        {
            var payment = new Payment
            {
                Amount = amount
            };

            var result = subject.ValidatePayment(payment);

            Assert.IsFalse(result);
        }

        [TestCase(0.00000001)]
        [TestCase(0.99999999)]
        [TestCase(1)]
        [TestCase(1.00000000)]
        [TestCase(1.00000001)]
        [TestCase(244.56764)]
        public void ValidatePayment_WithValidAmount_ShouldReturnTrue(decimal amount)
        {
            var payment = new Payment
            {
                Amount = amount
            };

            var result = subject.ValidatePayment(payment);

            Assert.IsTrue(result);
        }
    }
}
