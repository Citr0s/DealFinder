using System;
using System.Linq;
using DealFinder.Core.Communication;
using DealFinder.Data.Deals.Repository;
using DealFinder.Data.Users.Service;

namespace DealFinder.Data.Deals.Service
{
    public interface IDealsService
    {
        GetDealsByLocationResponse GetByLocation(double latitude, double longitude, string userIdentifier);
        SaveDealDetailsResponse SaveDealDetails(DealModel deal);
        MarkDealAsExpiredResponse MarkDealAsExpired(MarkDealAsExpiredRequest request);
    }

    public class DealsService : IDealsService
    {
        private readonly IDealsRepository _dealsRepository;
        private readonly IDealsMapper _dealsMapper;
        private readonly IUserService _userService;

        public DealsService(IDealsRepository dealsRepository, IDealsMapper dealsMapper, IUserService userService)
        {
            _dealsRepository = dealsRepository;
            _dealsMapper = dealsMapper;
            _userService = userService;
        }

        public GetDealsByLocationResponse GetByLocation(double latitude, double longitude, string userIdentifier)
        {
            var response = new GetDealsByLocationResponse();

            if (userIdentifier != null)
                _userService.SaveLastKnownLocation(latitude, longitude, userIdentifier);

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

        public MarkDealAsExpiredResponse MarkDealAsExpired(MarkDealAsExpiredRequest request)
        {
            var response = new MarkDealAsExpiredResponse();

            if (request.Id == null)
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.ValidationError,
                    UserMessage = "Something went wrong. Please try again later.",
                    TechnicalMessage = "Deal Identifier was not specified."
                });
                return response;
            }

            if (!Guid.TryParse(request.Id, out var dealId))
            {
                response.AddError(new Error
                {
                    Code = ErrorCodes.ValidationError,
                    UserMessage = "Something went wrong. Please try again later.",
                    TechnicalMessage = $"Deal Identifier was not in the correct format. Provided DealId: {request.Id}."
                });
                return response;
            }

            var saveDealDetailsResponse = _dealsRepository.MarkDealAsExpired(new UpdateDealRequest
            {
                Deal = new DealModel
                {
                    Id = dealId,
                    Expired = request.Expired
                }
            });

            if (saveDealDetailsResponse.HasError)
                response.AddError(saveDealDetailsResponse.Error);

            return response;
        }
    }
}