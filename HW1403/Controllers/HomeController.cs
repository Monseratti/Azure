using HW1403.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HW1403.Controllers
{
    public class HomeController : Controller
    {
        private readonly MpContext _context;

        public HomeController(MpContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Goods = (await _context.Goods.ToListAsync()).Join(
                (await _context.Categories.ToListAsync()), g=>g.MpCategoryId, c=>c.Id,
                (g, c) => new {id = g.Id, GoodName = g.Name, CategoryName = c.Name}
                ).ToList();
            return View();
        }

        public async Task<IActionResult> IndexByCategory(int id)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Goods = (await _context.Goods.ToListAsync()).Join(
                (await _context.Categories.ToListAsync()).Where(c=>c.Id==id).ToList(), g => g.MpCategoryId, c => c.Id,
                (g, c) => new { id = g.Id, GoodName = g.Name, CategoryName = c.Name }
                ).ToList();
            return View();
        }
    }
}