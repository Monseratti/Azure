using HW0703.Models;
using HW3003.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HW3003.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> DetectLang([FromBody] RequestBody trText)
        {
            return await TranslateController.TranslateAsync(trText.From, trText.To, trText.Text);
        }

        [HttpPost]
        public async Task<string> AnalyzePicture([FromBody]string uri)
        {
            return JsonSerializer.Serialize(new {text=await AIAnalyze.ReadTextFromPicture(uri)});
        }
        
    }
    
    public class RequestBody
        {
            public string From { get; set; }
            public string To { get; set; }
            public string Text { get; set; }
        }
}