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
        private readonly DealContext _context;

        public DealsRepository(DealContext context)
        {
            _context = context;
        }

        public GetByLocationResponse GetByLocation(double latitude, double longitude)
        {
            var response = new GetByLocationResponse();
            using (_context)
            {
                try
                {
                    var dealRecords = _context.Deals;

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

            using (_context)
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Add(new DealRecord
                    {
                        Title = request.Deal.Title,
                        Summary = request.Deal.Summary,
                        Latitude = request.Deal.Location.Latitude,
                        Longitude = request.Deal.Location.Longitude
                    });
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
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
    }
}