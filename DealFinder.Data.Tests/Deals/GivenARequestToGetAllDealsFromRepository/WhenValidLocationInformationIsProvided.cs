using DealFinder.Data.Deals.Repository;
using NUnit.Framework;

namespace DealFinder.Data.Tests.Deals.GivenARequestToGetAllDealsFromRepository
{
    [TestFixture]
    public class WhenCorrectLocationInformationIsProvided
    {
        private GetByLocationResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new DealsRepository();
            _result = subject.GetByLocation(50, 5);
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);   
        }

        [Test]
        public void ThenDealsAreReturned()
        {
            Assert.That(_result.Deals.Count, Is.GreaterThan(0));
        }
    }
}