using System.Collections.Generic;
using DealFinder.Core.Communication;

namespace DealFinder.Data.Deals.Repository
{
    public class GetByLocationResponse : CommunicationResponse
    {
        public GetByLocationResponse()
        {
            Deals = new List<DealRecord>();
        }

        public List<DealRecord> Deals;
    }
}