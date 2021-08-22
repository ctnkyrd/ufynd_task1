using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using task1.app.Services;

namespace task1.tests
{
    public class WebScrapperTests
    {
        private WebScrapper webScrapper;
        private string _url;
        [SetUp]
        public async Task SetupAsync()
        {
            _url = "http://localhost:8000/task%201%20-%20Kempinski%20Hotel%20Bristol%20Berlin%2C%20Germany%20-%20Booking.com.html";
            webScrapper = await WebScrapper.CreateWebScrapper(_url);
        }

        [Test]
        public void Getting_Text_Value_Will_Return_Correct_Value()
        {
            //arrange
            string hotelName = "Kempinski Hotel Bristol Berlin";
            string hotelNameSelector = "#hp_hotel_name";

            //act
            string retrievedHotelName = webScrapper.GetTextValue(hotelNameSelector);

            //assert
            Assert.AreEqual(hotelName, retrievedHotelName);
        }

        [Test]
        public void Getting_Attribute_Value_Will_Return_Correct_Value()
        {
            //arrange
            string elementSelector = "#location_score_tooltip";
            string attributeName = "class";
            string expectedAttributeClass = "location_score_tooltip";
            var element = webScrapper.GetElements("body").FirstOrDefault();

            //act
            string retrievedAttributeValue = webScrapper.GetAttributeValue(elementSelector,attributeName,element);

            //assert
            Assert.AreEqual(expectedAttributeClass, retrievedAttributeValue);
        }

        [Test]
        public void Getting_Elements_Should_Return_Collection_Of_Elements()
        {
            //arrange
            int expectedElementCount = 7;
            string elementContainerSelector = "#maxotel_rooms tbody tr";

            //act
            var retrievedElements = webScrapper.GetElements(elementContainerSelector);

            //assert
            Assert.AreEqual(expectedElementCount,retrievedElements.Length);
        }

        [Test]
        public void Wrong_Url_Should_Throw()
        {
            //arrange with invalid url
            string url = "localhost";
            var webScrapper = WebScrapper.CreateWebScrapper(url);
            
            //act & assert
            Assert.ThrowsAsync<Exception>(() => WebScrapper.CreateWebScrapper(url));
        }
    }
}