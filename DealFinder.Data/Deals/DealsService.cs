using System.Collections.Generic;
using System.Linq;
using DealFinder.Core.Distance;
using DealFinder.Core.Location;

namespace DealFinder.Data.Deals
{
    public interface IDealsService
    {
        List<DealModel> GetByLocation(double latitude, double longitude);
    }
        
    public class DealsService : IDealsService
    {
        private readonly IDealsRepository _dealsRepository;

        public DealsService(IDealsRepository dealsRepository)
        {
            _dealsRepository = dealsRepository;
        }

        public List<DealModel> GetByLocation(double latitude, double longitude)
        {
            var getDealsByLocationResponse = _dealsRepository.GetByLocation(latitude, longitude);

            if(getDealsByLocationResponse.HasError)
                return new List<DealModel>();

            return DealsMapper.Map(getDealsByLocationResponse.Deals).OrderBy(x => x.DistanceInMiles).ToList();
        }
    }
}
