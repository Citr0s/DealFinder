using DealFinder.Core.Communication;
using DealFinder.Data.Deals.Repository;
using DealFinder.Data.Deals.Service;
using Moq;
using NUnit.Framework;

namespace DealFinder.Data.Tests.Deals.Service.GivenARequestToGetAllDeals
{
    [TestFixture]
    public class WhenAnErrorIsReturnedFromTheRepository
    {
        private GetDealsByLocationResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var dealsRepository = new Mock<IDealsRepository>();
            dealsRepository.Setup(x => x.GetByLocation(It.IsAny<double>(), It.IsAny<double>())).Returns(new GetByLocationResponse
            {
                HasError = true,
                Error = new Error
                {
                    Code = ErrorCodes.DatabaseError,
                    UserMessage = "Some User Message",
                    TechnicalMessage = "Some Technical Message"
                }
            });

            var subject = new DealsService(dealsRepository.Object, null);
            _result = subject.GetByLocation(1, 2, null);
        }

        [Test]
        public void ThenHasErrorFlagIsCorrectlyMappedOntoTheResponse()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenErrorCodeFlagIsCorrectlyMappedOntoTheResponse()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.DatabaseError));
        }

        [Test]
        public void ThenUserErrorMessageCodeFlagIsCorrectlyMappedOntoTheResponse()
        {
            Assert.That(_result.Error.UserMessage, Is.EqualTo("Some User Message"));
        }

        [Test]
        public void ThenTechnicalErrorMessageCodeFlagIsCorrectlyMappedOntoTheResponse()
        {
            Assert.That(_result.Error.TechnicalMessage, Is.EqualTo("Some Technical Message"));
        }
    }
}