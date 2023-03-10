using HW0903.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HW0903.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(ILogger<HomeController> logger)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> AddLot([FromBody]Lot lot)
        {
            await QueueController.AddLot(lot);
            return JsonSerializer.Serialize(new { isOK = true });
        }

        [HttpGet]
        public async Task<string> ReceiveLots(string curr)
        {
            var lots = new List<object>();
            var response = await QueueController.ReceiveLots();
            response = response.Where(r => r.Body.ToString().Contains(curr)).ToArray();
            foreach (var item in response)
            {
                lots.Add(new
                {
                    id = item.MessageId,
                    lot = JsonSerializer.Deserialize<Lot>(item.Body.ToString())
                });
            }
            return JsonSerializer.Serialize(lots);
        }

        [HttpGet]
        public async Task<string> DeleteLot(string id)
        {
            await QueueController.DeleteLot(id);
            return JsonSerializer.Serialize(new {isOk = true});
        }
    }
}