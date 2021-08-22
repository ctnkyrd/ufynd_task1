using System;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using task1.app.Models;
using task1.app.Services;

namespace task1.app
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //adding configuration file for hotel property id mappings from web site
            var builder = new ConfigurationBuilder()
                // .SetBasePath(Directory.GetCurrentDirectory())
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("./appsettings.json");

            //build key value pair from config file
            var configuration = builder.Build();

            //bind config object to class
            var hotelScrapperConfig = new HotelScrapperConfig();
            configuration.GetSection("HotelConfiguration").Bind(hotelScrapperConfig);
        
            //create web scrapper for given url
            try
            {
                var webScrapper = await WebScrapper.CreateWebScrapper(hotelScrapperConfig.url);
                //create hotel object instace for given config and scrapper
                var hotel = new Hotel(webScrapper, hotelScrapperConfig);
                hotel.CreateFile();
                // Console.WriteLine(JsonConvert.SerializeObject(hotel));
            }
            catch (Exception ex)
            {
                 Console.WriteLine(ex.Message);
            }

        }
    }
}
