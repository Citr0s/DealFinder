using System;
using System.Linq;
using DealFinder.Core.Communication;
using DealFinder.Data.Deals.Repository;

namespace DealFinder.Data.Votes.Repository
{
    public interface IVoteRepository
    {
        CastVoteResponse CastVote(CastVoteRequest request);
    }

    public class VoteRepository : IVoteRepository
    {
        public CastVoteResponse CastVote(CastVoteRequest request)
        {
            var response = new CastVoteResponse();

            using (var context = new DatabaseContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    context.Add(new VoteRecord
                    {
                        User = context.Users.First(x => x.Identifier == request.UserId),
                        Deal = context.Deals.First(x => x.Identifier == request.DealId),
                        Positive = request.Positive
                    });
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    response.AddError(new Error
                    {
                        Code = ErrorCodes.DatabaseError,
                        UserMessage = "Something went wrong when casting a vote. Please try again later.",
                        TechnicalMessage = $"The following exception was thrown: {exception.Message}"
                    });
                }
            }

            return response;
        }
    }
}