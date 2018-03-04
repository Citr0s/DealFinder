using System;
using DealFinder.Data.Votes.Repository;
using DealFinder.Data.Votes.Service;
using Moq;
using NUnit.Framework;

namespace DealFinder.Data.Tests.Votes.Service.GivenARequestToCastAVote
{
    [TestFixture]
    public class WhenValidRequestIsProvided
    {
        private Guid _userId;
        private Guid _dealId;
        private Mock<IVoteRepository> _voteRepository;
        private CastVoteDetailsResponse _result;

        [OneTimeSetUp]
        public void SetUp()
        {
            _voteRepository = new Mock<IVoteRepository>();
            _voteRepository.Setup(x => x.CastVote(It.IsAny<CastVoteRequest>())).Returns(new CastVoteResponse());

            _userId = Guid.NewGuid();
            _dealId = Guid.NewGuid();

            var subject = new VoteService(_voteRepository.Object);
            _result = subject.CastVote(new CastVoteDetailsRequest
            {
                Positive = true,
                Vote = new VoteModel
                {
                    UserId = _userId.ToString(),
                    DealId = _dealId.ToString()
                }
            });
        }

        [Test]
        public void ThenPositiveIsMappedCorrectlyOntoRepositoryRequest()
        {
            _voteRepository.Verify(x => x.CastVote(It.Is<CastVoteRequest>(y => y.Positive == true)));
        }

        [Test]
        public void ThenUserIdIsMappedCorrectlyOntoRepositoryRequest()
        {
            _voteRepository.Verify(x => x.CastVote(It.Is<CastVoteRequest>(y => y.UserId == _userId)));
        }

        [Test]
        public void ThenDealIdIsMappedCorrectlyOntoRepositoryRequest()
        {
            _voteRepository.Verify(x => x.CastVote(It.Is<CastVoteRequest>(y => y.DealId == _dealId)));
        }

        [Test]
        public void ThenNoErrorsAreReturned()
        {
            Assert.That(_result.HasError, Is.False);
        }
    }
}