using DealFinder.Core.Security;
using NUnit.Framework;

namespace DealFinder.Core.Tests.Security.GivenARequestToEncrypt
{
    [TestFixture]
    public class WhenEncryptingAString
    {
        private string _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            var subject = new AesEncryptor("SOME_KEY");
            _result = subject.Encrypt("PASSWORD");
        }

        [Test]
        public void ThenStringIsEncryptedCorrectly()
        {
            Assert.That(_result, Is.EqualTo("ABZnmFTW8EcZLGqcDk2aQQ=="));
        }
    }
}