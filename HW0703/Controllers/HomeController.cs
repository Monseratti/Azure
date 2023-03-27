using HW0703.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HW0703.Controllers
{
    public class HomeController : Controller
    {
        ImageContext _context;

        private readonly string downloadPath = $@"/tmp/";

        public HomeController(ImageContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["FileList"] = await BlobConnect.ContainerInfo();
            ViewData["FileTags"] = (await _context.Images.ToListAsync()).Join(await _context.Tags.ToListAsync(),o=>o.Id, t=>t.ImageId,
                                        (o,t)=>new {o.URL, t.Value}).ToList();
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
                var image = await BlobConnect.UploadFile(tmpFilePath);
                Directory.Delete(tmpPath, true);
                if (image != null)
                {

                    image.Id = (await _context.Images.ToListAsync()).Count == 0 ? 0 : (await _context.Images.ToListAsync()).Last().Id + 1;
                    _context.Images.Add(image);
                    await _context.SaveChangesAsync();
                    var tag = new Tag()
                    {
                        Id = (await _context.Tags.ToListAsync()).Count == 0 ? 0 : (await _context.Tags.ToListAsync()).Last().Id + 1,
                        ImageId = image.Id,
                        Value = await AIAnalyze.MyAnalyseImageAsync(image.URL),
                    };
                    _context.Tags.Add(tag);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id != null)
            {
                var image = await BlobConnect.DeleteFile(id);
                if(image != null)
                {
                    image = await _context.Images.Where(o=>o.URL.Equals(image.URL)).FirstAsync();
                    var tags = await _context.Tags.Where(t=>t.ImageId.Equals(image.Id)).ToListAsync();
                    _context.Tags.RemoveRange(tags);
                    _context.Images.Remove(image);
                    await _context.SaveChangesAsync();
                }
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