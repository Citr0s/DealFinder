using System;
using DealFinder.Core.Communication;
using DealFinder.Core.Distance;

namespace DealFinder.Data.Deals.Repository
{
    public interface IDealsRepository
    {
        GetByLocationResponse GetByLocation(double latitude, double longitude);
        SaveDealResponse SaveDeal(SaveDealRequest request);
    }

    public class DealsRepository : IDealsRepository
    {
        public GetByLocationResponse GetByLocation(double latitude, double longitude)
        {
            var response = new GetByLocationResponse();
            using (var context = new DatabaseContext())
            {
                try
                {
                    var dealRecords = context.Deals;

                    foreach (var dealRecord in dealRecords)
                    {
                        dealRecord.DistanceInMeters = Haversine.Calculate(latitude, longitude, dealRecord.Latitude, dealRecord.Longitude);
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
                    context.Add(new DealRecord
                    {
                        Title = request.Deal.Title,
                        Summary = request.Deal.Summary,
                        Latitude = request.Deal.Location.Latitude,
                        Longitude = request.Deal.Location.Longitude
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