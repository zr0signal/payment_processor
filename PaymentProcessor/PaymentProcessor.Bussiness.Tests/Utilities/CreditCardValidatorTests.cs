using NUnit.Framework;
using PaymentProcessor.Bussiness.Utilities;
using PaymentProcessor.Bussiness.Entities;
using System;

namespace PaymentProcessor.Bussiness.Tests.Gateway
{
    [TestFixture]
    public class CreditCardValidatorTests
    {
        private ICreditCardValidator subject;

        [SetUp]
        public void SetUp()
        {
            subject = new CreditCardValidator();
        }

        [Test]
        public void ValidateCard_WithNullCard_ShouldReturnFalse()
        {
            var result = subject.ValidateCard(null);

            Assert.IsFalse(result);
        }

        [TestCase(null, null, -1, "1234")]
        [TestCase("Me", null, -1, "1234")]
        [TestCase("Me", "22", -1, "1234")]
        [TestCase("Me", "33334", -1, "1234")]
        [TestCase("Me", "2233", -1, "1234")]
        [TestCase("Me", "2233", 1, "1234")]
        public void ValidateCard_WithInvalidData_ShouldReturnFalse(string cardHolder, string cardNumber, int dateAdd, string securityCode)
        {
            var card = new CreditCard
            {
                CardHolder = cardHolder,
                CreditCardNumber = cardNumber,
                ExpirationDate = DateTime.UtcNow.AddDays(dateAdd),
                SecurityCode = securityCode
            };

            var result = subject.ValidateCard(card);

            Assert.IsFalse(result);
        }

        [TestCase("123")]
        [TestCase(null)]
        public void ValidateCard_WithValidData_ShouldReturnTrue(string securityCode)
        {
            var card = new CreditCard
            {
                CardHolder = "Me",
                CreditCardNumber = "1234",
                ExpirationDate = DateTime.UtcNow.AddYears(1),
                SecurityCode = securityCode
            };

            var result = subject.ValidateCard(card);

            Assert.IsTrue(result);
        }
    }
}
