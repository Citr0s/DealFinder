using DealFinder.Core.Communication;
using NUnit.Framework;

namespace DealFinder.Core.Tests.Communication.GivenARequest
{
    public class WhenCommunicationResponseIsCreate
    {
        private CommunicationResponse _subject;

        [OneTimeSetUp]
        public void SetUp()
        {
            _subject = new CommunicationResponse();
            _subject.AddError(new Error
            {
                Code = 1,
                UserMessage = "Some user message",
                TechnicalMessage = "Some technical message"
            });
        }

        [Test]
        public void ThenHasErrorFlagIsSetCorrectly()
        {
            Assert.That(_subject.HasError, Is.True);
        }

        [Test]
        public void ThenErrorCodeIsSetCorrectly()
        {
            Assert.That(_subject.Error.Code, Is.EqualTo(1));
        }

        [Test]
        public void ThenUserErrorMessageCodeIsSetCorrectly()
        {
            Assert.That(_subject.Error.UserMessage, Is.EqualTo("Some user message"));
        }

        [Test]
        public void ThenTechnicalErrorMessageCodeIsSetCorrectly()
        {
            Assert.That(_subject.Error.TechnicalMessage, Is.EqualTo("Some technical message"));
        }
    }
}