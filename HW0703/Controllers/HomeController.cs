using HW0703.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace HW0703.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		private readonly string downloadPath = $@"~/tmp/";

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public async  Task<IActionResult> Index()
		{
			ViewData["FileList"] = await BlobConnect.ContainerInfo();
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Index(string id)
		{
			var files = Request.Form.Files;
			if (files.Count != 0)
			{
				var tmpPath = $@"{Directory.GetCurrentDirectory()}\wwwroot\tmpFiles";
				Directory.CreateDirectory(tmpPath);
				var tmpFilePath = Path.Combine(tmpPath, files[0].FileName);
				using (FileStream fs = new FileStream(tmpFilePath, FileMode.Create))
				{
					await files[0].CopyToAsync(fs);
				}
				await BlobConnect.UploadFile(tmpFilePath);
				Directory.Delete(tmpPath,true);
			}
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string? id)
		{
			if (id != null)
			{
				await BlobConnect.DeleteFile(id);
			}
			return RedirectToAction("Index");
		}


		[HttpGet]
		public async Task<IActionResult> Details(string? id)
		{
			if (id != null)
			{
				await BlobConnect.DownloadFile(id);
				ViewData["SrcImg"] = $"{downloadPath}{id}";
				return View();
			}
			return RedirectToAction("Index");
		}



		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}