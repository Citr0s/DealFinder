using DealFinder.Core.Communication;
using DealFinder.Core.Distance;
using DealFinder.Core.Security;
using DealFinder.Data.Deals.Repository;
using DealFinder.Data.Deals.Service;
using DealFinder.Data.Users.Service;
using Moq;
using NUnit.Framework;

namespace DealFinder.Data.Tests.Deals.Service.GivenARequestToSaveADeal
{
    [TestFixture]
    public class WhenRepositoryReturnsAnError
    {
        private SaveDealDetailsResponse _result;
        private Mock<IDealsRepository> _dealsRepository;

        [OneTimeSetUp]
        public void SetUp()
        {
            _dealsRepository = new Mock<IDealsRepository>();
            _dealsRepository.Setup(x => x.SaveDeal(It.IsAny<SaveDealRequest>())).Returns(new SaveDealResponse
            {
                HasError = true,
                Error = new Error
                {
                    Code = ErrorCodes.DatabaseError,
                    UserMessage = "Some User Message",
                    TechnicalMessage = "Some Technical Message"
                }
            });

            var keyReader = new Mock<IKeyReader>();
            keyReader.Setup(x => x.GetKey()).Returns("KEY");

            var subject = new DealsService(_dealsRepository.Object, new DealsMapper(new UserMapper(new AesEncryptor(keyReader.Object.GetKey()))), null);
            _result = subject.SaveDealDetails(new DealModel
            {
                Location = new Location
                {
                    Latitude = 51,
                    Longitude = 2
                },
                Summary = "Some Summary",
                Title = "Some Title",
                UserIdentifier = "Some User Identifier"
            });
        }

        [Test]
        public void ThenRepositoryIsCalledWithCorrectlyMappedLatitude()
        {
            _dealsRepository.Verify(x => x.SaveDeal(It.Is<SaveDealRequest>(y => y.Deal.Location.Latitude == 51)));
        }

        [Test]
        public void ThenRepositoryIsCalledWithCorrectlyMappedLongitude()
        {
            _dealsRepository.Verify(x => x.SaveDeal(It.Is<SaveDealRequest>(y => y.Deal.Location.Longitude == 2)));
        }

        [Test]
        public void ThenRepositoryIsCalledWithCorrectlyMappedSummary()
        {
            _dealsRepository.Verify(x => x.SaveDeal(It.Is<SaveDealRequest>(y => y.Deal.Summary == "Some Summary")));
        }

        [Test]
        public void ThenRepositoryIsCalledWithCorrectlyMappedTitle()
        {
            _dealsRepository.Verify(x => x.SaveDeal(It.Is<SaveDealRequest>(y => y.Deal.Title == "Some Title")));
        }

        [Test]
        public void ThenRepositoryIsCalledWithCorrectlyMappedUserIdentifier()
        {
            _dealsRepository.Verify(x => x.SaveDeal(It.Is<SaveDealRequest>(y => y.Deal.UserIdentifier == "Some User Identifier")));
        }

        [Test]
        public void ThenAnErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenAnErrorCodeIsReturned()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.DatabaseError));
        }

        [Test]
        public void ThenUserErrorMessageIsReturned()
        {
            Assert.That(_result.Error.UserMessage, Is.EqualTo("Some User Message"));
        }

        [Test]
        public void ThenTechnicalErrorMessageIsReturned()
        {
            Assert.That(_result.Error.TechnicalMessage, Is.EqualTo("Some Technical Message"));
        }
    }
}