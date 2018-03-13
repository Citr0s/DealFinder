namespace DealFinder.Data.Deals.Service
{
    public class MarkDealAsExpiredRequest
    {
        public string Id { get; set; }
        public bool Expired { get; set; }
    }
}