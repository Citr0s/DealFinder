using System.Collections.Generic;
using DealFinder.Core.Communication;

namespace DealFinder.Data.Deals
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