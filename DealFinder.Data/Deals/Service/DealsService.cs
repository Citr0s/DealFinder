using System.Linq;
using DealFinder.Core.Communication;
using DealFinder.Data.Deals.Repository;

namespace DealFinder.Data.Deals.Service
{
    public interface IDealsService
    {
        GetDealsByLocationResponse GetByLocation(double latitude, double longitude, string userIdentifier);
        SaveDealDetailsResponse SaveDealDetails(DealModel deal);
    }

    public class DealsService : IDealsService
    {
        private readonly IDealsRepository _dealsRepository;
        private readonly IDealsMapper _dealsMapper;

        public DealsService(IDealsRepository dealsRepository, IDealsMapper dealsMapper)
        {
            _dealsRepository = dealsRepository;
            _dealsMapper = dealsMapper;
        }

        public GetDealsByLocationResponse GetByLocation(double latitude, double longitude, string userIdentifier)
        {
            var response = new GetDealsByLocationResponse();

            var getDealsByLocationResponse = _dealsRepository.GetByLocation(latitude, longitude);

            if (getDealsByLocationResponse.HasError)
            {
                response.AddError(getDealsByLocationResponse.Error);
                return response;
            }

            response.Deals = _dealsMapper.Map(getDealsByLocationResponse.Deals, userIdentifier).OrderBy(x => x.DistanceInMeters).ToList();
            return response;
        }

        public SaveDealDetailsResponse SaveDealDetails(DealModel deal)
        {
            var response = new SaveDealDetailsResponse();

            if (deal.Title == null || deal.Summary == null || deal.Location == null || deal.Tags.Count == 0)
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.ValidationError,
                    UserMessage = "Title, Summary, Tags and Location fields are required.",
                    TechnicalMessage = "User has not specified required fields."
                });
                return response;
            }

            var saveDealDetailsResponse = _dealsRepository.SaveDeal(new SaveDealRequest
            {
                Deal = deal
            });

            if (saveDealDetailsResponse.HasError)
                response.AddError(saveDealDetailsResponse.Error);

            return response;
        }
    }
}