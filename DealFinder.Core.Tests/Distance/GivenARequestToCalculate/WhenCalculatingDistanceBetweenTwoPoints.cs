using DealFinder.Core.Distance;
using NUnit.Framework;

namespace DealFinder.Core.Tests.Distance.GivenARequestToCalculate
{
    [TestFixture]
    public class WhenCalculatingDistanceBetweenTwoPoints
    {
        private double _subject;

        [OneTimeSetUp]
        public void SetUp()
        {
            _subject = Haversine.Calculate(50, 25, 55, 30);
        }

        [Test]
        public void ThenDistanceIsCorrectlyCalculated()
        {
            Assert.That(_subject, Is.EqualTo(650516));
        }
    }
}