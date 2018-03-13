using System;
using System.Linq;
using DealFinder.Core.Communication;
using DealFinder.Core.Distance;
using DealFinder.Data.Tags.Repository;
using Microsoft.EntityFrameworkCore;

namespace DealFinder.Data.Deals.Repository
{
    public interface IDealsRepository
    {
        GetByLocationResponse GetByLocation(double latitude, double longitude);
        SaveDealResponse SaveDeal(SaveDealRequest request);
    }

    public class DealsRepository : IDealsRepository
    {
        private const int COOLDOWN_AMOUNT_IN_MINUTES = 15;

        public GetByLocationResponse GetByLocation(double latitude, double longitude)
        {
            var response = new GetByLocationResponse();
            using (var context = new DatabaseContext())
            {
                try
                {
                    var dealRecords = context.Deals.Include(x => x.User).ToList();

                    foreach (var dealRecord in dealRecords)
                    {
                        dealRecord.DistanceInMeters = Haversine.Calculate(latitude, longitude, dealRecord.Latitude, dealRecord.Longitude);
                        dealRecord.Votes = context.Votes.Where(x => x.Deal.Identifier == dealRecord.Identifier).ToList();
                        dealRecord.DealTags = context.DealTags.Where(x => x.Deal.Identifier == dealRecord.Identifier).ToList();

                        foreach (var dealTag in dealRecord.DealTags)
                            dealTag.Tag = context.Tags.First(x => x.Identifier == dealTag.TagIdentifier);

                        response.Deals.Add(dealRecord);
                    }
                }
                catch (Exception exception)
                {
                    response.AddError(new Error
                    {
                        Code = ErrorCodes.DatabaseError,
                        UserMessage = "Something went wrong when getting latest deals. Please try again later.",
                        TechnicalMessage = $"The following exception was thrown: {exception.Message}"
                    });
                }
            }
            return response;
        }

        public SaveDealResponse SaveDeal(SaveDealRequest request)
        {
            var response = new SaveDealResponse();

            using (var context = new DatabaseContext())
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var previousUserDeals = context.Deals.Where(x => x.User.Identifier.ToString() == request.Deal.UserIdentifier).OrderByDescending(x => x.CreatedAt).FirstOrDefault();

                    if (previousUserDeals != null && previousUserDeals.CreatedAt.AddMinutes(COOLDOWN_AMOUNT_IN_MINUTES) > DateTime.Now)
                    {
                        response.AddError(new Error
                        {
                            Code = ErrorCodes.DealCreationCooldownActive,
                            UserMessage = $"You can only add one deal every {COOLDOWN_AMOUNT_IN_MINUTES} minutes. Please try again at {previousUserDeals.CreatedAt.AddMinutes(COOLDOWN_AMOUNT_IN_MINUTES).ToShortTimeString()}.",
                            TechnicalMessage = $"User attempted to create a deal within their cooldown period. UserId: {request.Deal.UserIdentifier}"
                        });
                        return response;
                    }

                    context.Add(new DealRecord
                    {
                        Title = request.Deal.Title,
                        Summary = request.Deal.Summary,
                        Latitude = request.Deal.Location.Latitude,
                        Longitude = request.Deal.Location.Longitude,
                        User = context.Users.First(x => x.Identifier.ToString() == request.Deal.UserIdentifier),
                        DealTags = request.Deal.Tags.ConvertAll(x => new DealTagRecord { Tag = context.Tags.FirstOrDefault(y => y.Name.ToLower() == x.ToLower()) ?? new TagRecord { Name = x } }),
                        CreatedAt = DateTime.Now
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
                        UserMessage = "Something went wrong when saving your deal. Please try again later.",
                        TechnicalMessage = $"The following exception was thrown: {exception.Message}"
                    });
                }
            }

            return response;
        }
    }
}