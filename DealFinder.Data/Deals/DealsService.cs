using System.Collections.Generic;
using System.Linq;
using DealFinder.Core.Distance;

namespace DealFinder.Data.Deals
{
    public interface IDealsService
    {
        GetDealsByLocationResponse GetByLocation(double latitude, double longitude);
    }

    public class DealsService : IDealsService
    {
        private readonly IDealsRepository _dealsRepository;

        public DealsService(IDealsRepository dealsRepository)
        {
            _dealsRepository = dealsRepository;
        }

        public GetDealsByLocationResponse GetByLocation(double latitude, double longitude)
        {
            var response = new GetDealsByLocationResponse();

            var getDealsByLocationResponse = _dealsRepository.GetByLocation(latitude, longitude);

            if (getDealsByLocationResponse.HasError)
            {
                response.AddError(getDealsByLocationResponse.Error);
                return response;
            }

            response.Deals = DealsMapper.Map(getDealsByLocationResponse.Deals).OrderBy(x => x.DistanceInMeters).ToList();
            return response;
        }
    }
}