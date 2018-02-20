using System.Collections.Generic;
using DealFinder.Data.Deals;
using Moq;
using NUnit.Framework;

namespace DealFinder.Data.Tests.Deals.GivenARequestToGetAllDealsFromService
{
    [TestFixture]
    public class WhenCorrectLocationInformationIsProvided
    {
        private GetDealsByLocationResponse _result;

        [SetUp]
        public void SetUp()
        {
            var dealsRepository = new Mock<IDealsRepository>();
            dealsRepository
                .Setup(x => x.GetByLocation(It.IsAny<double>(), It.IsAny<double>()))
                .Returns(() => new GetByLocationResponse
                {
                    Deals = new List<DealRecord>
                    {
                        new DealRecord
                        {
                            Title = "Awesome Deal Title",
                            Summary = "Awesome Deal Summary",
                            DistanceInMeters = 1200,
                            Latitude = 3,
                            Longitude = 5
                        }
                    }
                });

            var subject = new DealsService(dealsRepository.Object);
            _result = subject.GetByLocation(1, 2);
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }

        [Test]
        public void ThenAListOfDealsIsReturned()
        {
            Assert.That(_result.Deals.Count, Is.EqualTo(1));
        }

        [Test]
        public void ThenTheDealTitleIsMappedCorrectly()
        {
            Assert.That(_result.Deals[0].Title, Is.EqualTo("Awesome Deal Title"));
        }

        [Test]
        public void ThenTheDealSummaryIsMappedCorrectly()
        {
            Assert.That(_result.Deals[0].Summary, Is.EqualTo("Awesome Deal Summary"));
        }

        [Test]
        public void ThenTheDealDistanceInMetersIsMappedCorrectly()
        {
            Assert.That(_result.Deals[0].DistanceInMeters, Is.EqualTo(1200));
        }

        [Test]
        public void ThenTheLocationLatitudeIsMappedCorrectly()
        {
            Assert.That(_result.Deals[0].Location.Latitude, Is.EqualTo(3));
        }

        [Test]
        public void ThenTheLocationLongitudeIsMappedCorrectly()
        {
            Assert.That(_result.Deals[0].Location.Longitude, Is.EqualTo(5));
        }
    }
}