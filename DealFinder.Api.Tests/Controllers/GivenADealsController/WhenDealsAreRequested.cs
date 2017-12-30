using DealFinder.Api.Controllers;
using NUnit.Framework;

namespace DealFinder.Api.Tests.Controllers.GivenADealsController
{
    [TestFixture]
    public class WhenDealsAreRequested
    {
//        private ActionResult _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new DealsController();
    //        _result = subject.Get(-54.321, 12.345);
        }

        [Test]
        public void ThenLocationInformationIsReturned()
        {
   //         Assert.That(_result, Is.TypeOf<ActionResult>());
        }
    }
}