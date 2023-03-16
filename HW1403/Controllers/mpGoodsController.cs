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
    public class mpGoodsController : Controller
    {
        private readonly MpContext _context;

        public mpGoodsController(MpContext context)
        {
            _context = context;
        }

        // GET: mpGoods
        public async Task<IActionResult> Index()
        {
              return _context.Goods != null ? 
                          View(await _context.Goods.ToListAsync()) :
                          Problem("Entity set 'MpContext.Goods'  is null.");
        }

        // GET: mpGoods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Goods == null)
            {
                return NotFound();
            }

            var mpGood = await _context.Goods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mpGood == null)
            {
                return NotFound();
            }
            ViewBag.MpCategory = (await _context.Categories.FindAsync(mpGood.MpCategoryId))!.Name;
            return View(mpGood);
        }

        // GET: mpGoods/Create
        public IActionResult Create()
        {
            ViewBag.MpCategoryId = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: mpGoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MpCategoryId")] mpGood mpGood)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mpGood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mpGood);
        }

        // GET: mpGoods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Goods == null)
            {
                return NotFound();
            }
            var mpGood = await _context.Goods.FindAsync(id);
            if (mpGood == null)
            {
                return NotFound();
            }
            ViewBag.MpCategoryId = new SelectList(_context.Categories, "Id", "Name",mpGood.MpCategoryId);
            return View(mpGood);
        }

        // POST: mpGoods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MpCategoryId")] mpGood mpGood)
        {
            if (id != mpGood.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mpGood);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mpGoodExists(mpGood.Id))
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
            return View(mpGood);
        }

        // GET: mpGoods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Goods == null)
            {
                return NotFound();
            }

            var mpGood = await _context.Goods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mpGood == null)
            {
                return NotFound();
            }

            return View(mpGood);
        }

        // POST: mpGoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Goods == null)
            {
                return Problem("Entity set 'MpContext.Goods'  is null.");
            }
            var mpGood = await _context.Goods.FindAsync(id);
            if (mpGood != null)
            {
                _context.Goods.Remove(mpGood);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool mpGoodExists(int id)
        {
          return (_context.Goods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
