using System.Collections.Generic;
using DealFinder.Core.Communication;

namespace DealFinder.Data.Deals
{
    public class GetDealsByLocationResponse : CommunicationResponse
    {
        public GetDealsByLocationResponse()
        {
            Deals = new List<DealModel>();
        }
        
        public List<DealModel> Deals { get; set; }
    }
}