using task1.app.Services;

namespace task1.app.Models
{
    public class HotelScrapperConfig
    {
        public string url { get; set; }
        public string hotelName { get; set; }
        public string address { get; set; }
        public string reviewPoints { get; set; }
        public string numberOfReviews { get; set; }
        public string description { get; set; }
        public RoomCategoryConfig roomCategoryConfig { get; set; }
        public AlternativeHotelConfig alternativeHotelConfig { get; set; }
    }

    public class RoomCategoryConfig
    {
        public string categoryTable { get; set; }
        public string capacity { get; set; }
        public string roomType { get; set; }
    }

    public class AlternativeHotelConfig
    {
        public string container { get; set; }
        public string name { get; set; }
        public string link { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string message { get; set; }
        public string reviewCount { get; set; }
        public string reviewPoints { get; set; }
    }
}