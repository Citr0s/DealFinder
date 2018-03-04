using DealFinder.Api.Controllers.Deal;
using DealFinder.Data.Deals.Service;
using Moq;
using NUnit.Framework;

namespace DealFinder.Api.Tests.Deal.GivenARequestToTheDealController
{
    [TestFixture]
    public class WhenGetByLocationMethodIsInvoked
    {
        private ActionResult _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var dealService = new Mock<IDealsService>();
            dealService.Setup(x => x.GetByLocation(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>()));

            var subject = new DealController(dealService);
            _result = subject.Get(-51, 2, "SOME_USER_ID");
        }

        [Test]
        public void ThenParametersAreMappedCorrectly()
        {
            
        }
    }
}