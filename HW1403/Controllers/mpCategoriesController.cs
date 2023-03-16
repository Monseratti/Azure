using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HW1403.Models;

namespace HW1403.Controllers
{
    public class mpCategoriesController : Controller
    {
        private readonly MpContext _context;

        public mpCategoriesController(MpContext context)
        {
            _context = context;
        }

        // GET: mpCategories
        public async Task<IActionResult> Index()
        {
              return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'MpContext.Categories'  is null.");
        }

        // GET: mpCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var mpCategory = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mpCategory == null)
            {
                return NotFound();
            }

            return View(mpCategory);
        }

        // GET: mpCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: mpCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] mpCategory mpCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mpCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mpCategory);
        }

        // GET: mpCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var mpCategory = await _context.Categories.FindAsync(id);
            if (mpCategory == null)
            {
                return NotFound();
            }
            return View(mpCategory);
        }

        // POST: mpCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] mpCategory mpCategory)
        {
            if (id != mpCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mpCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mpCategoryExists(mpCategory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mpCategory);
        }

        // GET: mpCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var mpCategory = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mpCategory == null)
            {
                return NotFound();
            }

            return View(mpCategory);
        }

        // POST: mpCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'MpContext.Categories'  is null.");
            }
            if (_context.Goods.Where(o=>o.MpCategoryId==id) != null)
            {
                return Problem("Entity was used.");
            }

            var mpCategory = await _context.Categories.FindAsync(id);
            if (mpCategory != null)
            {
                _context.Categories.Remove(mpCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool mpCategoryExists(int id)
        {
          return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
