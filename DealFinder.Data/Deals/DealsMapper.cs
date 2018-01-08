using System.Collections.Generic;

namespace DealFinder.Data.Deals
{
    public class DealsMapper
    {
        public static List<DealModel> Map(List<DealRecord> dealRecords)
        {
            var response = new List<DealModel>();

            foreach (var dealRecord in dealRecords)
            {
                response.Add(new DealModel
                {
                    Title = dealRecord.Title,
                    Summary = dealRecord.Summary,
                    DistanceInMeters = dealRecord.DistanceInMeters,
                    Location = dealRecord.Location
                });   
            }

            return response;
        }
    }
}