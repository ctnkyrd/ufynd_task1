using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using task1.app.Models;
using task1.app.Services;

namespace task1.app
{
    public class Hotel
    {
        public string hotelName
        {
            get;
            private set;
        }
        public string address { get; private set; }

        public double reviewPoints { get; private set;}
        public int numberOfReviews { get; private set;}
        public string description { get; private set;}
        public List<RoomCategory> roomCategories { get; private set;}
        public List<AlternativeHotel> alternativeHotels { get; private set;}

        // public static async Task<Hotel> Create(WebScrapper scrapper, HotelScrapperConfig config)
        // {
        //     var hotel = new Hotel();
        //     await hotel.Initialize(scrapper, config);
        //     return hotel;
        // }

        public Hotel(WebScrapper scrapper, HotelScrapperConfig config){
            hotelName = scrapper.GetTextValue(config.hotelName);
            address = scrapper.GetTextValue(config.address);
            if(Double.TryParse(scrapper.GetTextValue(config.reviewPoints), out double point)){
                reviewPoints = point;
            }
            else {
                reviewPoints = 0;
            }
            if(Int32.TryParse(scrapper.GetTextValue(config.numberOfReviews), out int number)){
                numberOfReviews = number;
            }
            else {
                numberOfReviews = 0;
            }
            description = scrapper.GetTextValue(config.description);

            var roomCategoryElements = scrapper.GetElements(config.roomCategoryConfig.categoryTable);
            //initialize an empty list if GetElements returns item list
            if (roomCategoryElements.Length > 0)
            {
                this.roomCategories = new List<RoomCategory>();
            }
            foreach (var roomCategoryElement in roomCategoryElements)
            {
                var capacity = scrapper.GetAttributeValue(config.roomCategoryConfig.capacity,"title",roomCategoryElement);
                var roomType = scrapper.GetTextValue(config.roomCategoryConfig.roomType, roomCategoryElement);
                RoomCategory roomCategory = new RoomCategory{
                    Capacity = capacity,
                    RoomType = roomType
                };
                this.roomCategories.Add(roomCategory);
            }

            var alternativeHotelElements = scrapper.GetElements(config.alternativeHotelConfig.container);

            if(alternativeHotelElements.Length > 0)
            {
                this.alternativeHotels = new List<AlternativeHotel>();
            }

            foreach (var alternativeHotelElement in alternativeHotelElements)
            {
                var name = scrapper.GetTextValue(config.alternativeHotelConfig.name, alternativeHotelElement);
                var link = scrapper.GetAttributeValue(config.alternativeHotelConfig.link,"href", alternativeHotelElement);
                var image = scrapper.GetAttributeValue(config.alternativeHotelConfig.image,"src", alternativeHotelElement);
                var description = scrapper.GetTextValue(config.alternativeHotelConfig.description, alternativeHotelElement);
                var message = scrapper.GetTextValue(config.alternativeHotelConfig.message, alternativeHotelElement);
                var reviewCount = scrapper.GetTextValue(config.alternativeHotelConfig.reviewCount, alternativeHotelElement);
                var reviewPoints = scrapper.GetTextValue(config.alternativeHotelConfig.reviewPoints, alternativeHotelElement);

                AlternativeHotel alternativeHotel = new AlternativeHotel { 
                    Name = name,
                    Link = link,
                    Image = image,
                    Description = description,
                    Message = message,
                    ReviewCount = Convert.ToInt32(reviewCount),
                    ReviewPoints = Convert.ToDouble(reviewPoints)
                };

                this.alternativeHotels.Add(alternativeHotel);
            }
        }

        // private Hotel()
        // {

        // }

        // private async Task Initialize(WebScrapper scrapper, HotelScrapperConfig config)
        // {
        //     hotelName = await scrapper.GetTextValue(config.hotelName);
        //     address = await scrapper.GetTextValue(config.address);
        //     if(Double.TryParse(await scrapper.GetTextValue(config.reviewPoints), out double point)){
        //         reviewPoints = point;
        //     }
        //     else {
        //         reviewPoints = 0;
        //     }
        //     if(Int32.TryParse(await scrapper.GetTextValue(config.numberOfReviews), out int number)){
        //         numberOfReviews = number;
        //     }
        //     else {
        //         numberOfReviews = 0;
        //     }
        //     description = await scrapper.GetTextValue(config.description);

        //     var roomCategoryElements = await scrapper.GetElements(config.roomCategoryConfig.categoryTable);
        //     //initialize an empty list if GetElements returns item list
        //     if (roomCategoryElements.Length > 0)
        //     {
        //         this.roomCategories = new List<RoomCategory>();
        //     }
        //     foreach (var roomCategoryElement in roomCategoryElements)
        //     {
        //         var capacity = scrapper.GetAttributeValue(config.roomCategoryConfig.capacity,"title",roomCategoryElement);
        //         var roomType = await scrapper.GetTextValue(config.roomCategoryConfig.roomType, roomCategoryElement);
        //         RoomCategory roomCategory = new RoomCategory{
        //             Capacity = capacity,
        //             RoomType = roomType
        //         };
        //         this.roomCategories.Add(roomCategory);
        //     }

        //     var alternativeHotelElements = await scrapper.GetElements(config.alternativeHotelConfig.container);

        //     if(alternativeHotelElements.Length > 0)
        //     {
        //         this.alternativeHotels = new List<AlternativeHotel>();
        //     }

        //     foreach (var alternativeHotelElement in alternativeHotelElements)
        //     {
        //         var name = await scrapper.GetTextValue(config.alternativeHotelConfig.name, alternativeHotelElement);
        //         var link = scrapper.GetAttributeValue(config.alternativeHotelConfig.link,"href", alternativeHotelElement);
        //         var image = scrapper.GetAttributeValue(config.alternativeHotelConfig.image,"src", alternativeHotelElement);
        //         var description = await scrapper.GetTextValue(config.alternativeHotelConfig.description, alternativeHotelElement);
        //         var message = await scrapper.GetTextValue(config.alternativeHotelConfig.message, alternativeHotelElement);
        //         var reviewCount = await scrapper.GetTextValue(config.alternativeHotelConfig.reviewCount, alternativeHotelElement);
        //         var reviewPoints = await scrapper.GetTextValue(config.alternativeHotelConfig.reviewPoints, alternativeHotelElement);

        //         AlternativeHotel alternativeHotel = new AlternativeHotel { 
        //             Name = name,
        //             Link = link,
        //             Image = image,
        //             Description = description,
        //             Message = message,
        //             ReviewCount = Convert.ToInt32(reviewCount),
        //             ReviewPoints = Convert.ToDouble(reviewPoints)
        //         };

        //         this.alternativeHotels.Add(alternativeHotel);
        //     }
        // }

        public void CreateFile(string path = "")
        {
            string jsondata = JsonConvert.SerializeObject(this, Formatting.Indented);
            // Write that JSON to txt file,  
            System.IO.File.WriteAllText(path + "hotel.json", jsondata);
        }






    }
}