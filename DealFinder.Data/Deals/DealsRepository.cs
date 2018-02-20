using System;
using DealFinder.Core.Communication;
using DealFinder.Core.Distance;

namespace DealFinder.Data.Deals
{
    public interface IDealsRepository
    {
        GetByLocationResponse GetByLocation(double latitude, double longitude);
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
                        dealRecord.DistanceInMeters = Haversine.Calculate(latitude, longitude, dealRecord.Location.Latitude, dealRecord.Location.Longitude);
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
    }
}