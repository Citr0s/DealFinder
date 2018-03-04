using System;
using DealFinder.Core.Communication;
using DealFinder.Data.Votes.Repository;

namespace DealFinder.Data.Votes.Service
{
    public interface IVoteService
    {
        CastVoteDetailsResponse CastVote(CastVoteDetailsRequest voteModel);
    }

    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;

        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public CastVoteDetailsResponse CastVote(CastVoteDetailsRequest voteModel)
        {
            var response = new CastVoteDetailsResponse();

            Guid userId;
            if (Guid.TryParse(voteModel.Vote.UserId, out userId))
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.InvalidGuidProvided,
                    UserMessage = "Something went wrong. Please try again later.",
                    TechnicalMessage = "Provided user identifier is not a valid guid."
                });
                return response;
            }

            Guid dealId;
            if (Guid.TryParse(voteModel.Vote.DealId, out userId))
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.InvalidGuidProvided,
                    UserMessage = "Something went wrong. Please try again later.",
                    TechnicalMessage = "Provided deal identifier is not a valid guid."
                });
                return response;
            }

            var castVoteResponse = _voteRepository.CastVote(new CastVoteRequest
            {
                UserId = userId,
                DealId = dealId
            });

            if (castVoteResponse.HasError)
                response.AddError(castVoteResponse.Error);

            return response;
        }
    }
}