using System;
using DealFinder.Core.Communication;
using DealFinder.Data.Votes.Repository;
using DealFinder.Data.Votes.Service;
using Moq;
using NUnit.Framework;

namespace DealFinder.Data.Tests.Votes.Service.GivenARequestToCastAVote
{
    [TestFixture]
    public class WhenDealIdIsNotAValidGuid
    {
        private Mock<IVoteRepository> _voteRepository;
        private CastVoteDetailsResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _voteRepository = new Mock<IVoteRepository>();
            _voteRepository.Setup(x => x.CastVote(It.IsAny<CastVoteRequest>())).Returns(new CastVoteResponse());

            var subject = new VoteService(_voteRepository.Object);
            _result = subject.CastVote(new CastVoteDetailsRequest
            {
                Positive = true,
                Vote = new VoteModel
                {
                    UserId = Guid.NewGuid().ToString(),
                    DealId = "NOT_A_GUID"
                }
            });
        }

        [Test]
        public void ThenUserRepositoryIsNeverCalled()
        {
            _voteRepository.Verify(x => x.CastVote(It.IsAny<CastVoteRequest>()), Times.Never);
        }

        [Test]
        public void ThenAnErrorIsReturned()
        {
            Assert.That(_result.HasError, Is.True);
        }

        [Test]
        public void ThenErrorCodeIsReturned()
        {
            Assert.That(_result.Error.Code, Is.EqualTo(ErrorCodes.InvalidGuidProvided));
        }
    }
}