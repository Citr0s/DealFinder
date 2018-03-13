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
    public class WhenValidRequestIsProvided
    {
        private SaveDealDetailsResponse _result;
        private Mock<IDealsRepository> _dealsRepository;

        [OneTimeSetUp]
        public void SetUp()
        {
            _dealsRepository = new Mock<IDealsRepository>();
            _dealsRepository.Setup(x => x.SaveDeal(It.IsAny<SaveDealRequest>())).Returns(new SaveDealResponse());

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
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }
    }
}