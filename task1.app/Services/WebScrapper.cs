using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace task1.app.Services
{
    public class WebScrapper
    {
        private IConfiguration _config;
        private IBrowsingContext _context;
        private IDocument _document;

        public static async Task<WebScrapper> CreateWebScrapper(string url)
        {
            var webScrapper = new WebScrapper();
            await webScrapper.Initialize(url);
            return webScrapper;
        }

        private WebScrapper(){}

        private async Task Initialize(string url){
            _config = Configuration.Default.WithDefaultLoader();
            _context = BrowsingContext.New(_config);

         
            _document = await _context.OpenAsync(url);
            if(_document == null) 
            {
                throw new Exception("Html Document couldn't created! please check the Url you provided");
            }
        }

        public string GetTextValue(string selector, IElement element = null)
        {
            if (element != null)
            {
                var res = element.QuerySelector(selector);
                if (res == null)
                {
                    return string.Empty;
                }
                return res.TextContent.Trim();
            }
            var cells = _document.QuerySelectorAll(selector);
            var titles = cells.Select(m => m.TextContent);
            if (titles.Count() == 0)
                return string.Empty;
            return titles.FirstOrDefault().Trim();
        }

        public IHtmlCollection<IElement> GetElements(string selector)
        {
            var cells = _document.QuerySelectorAll(selector);
            return cells;
        }

        public string GetAttributeValue(string selector, string attribute, IElement element)
        {
            var selectedElement = element.QuerySelector(selector);
            var result = selectedElement.GetAttribute(attribute);
            if(result == null)
            {
                return string.Empty;
            }
            return result.Trim();
        }

    }
}