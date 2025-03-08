using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KTRS.Models;

namespace KTRS.Controllers
{
    // [Authorize(Roles = "Admin")] // İsterseniz sadece Admin erişsin
    public class BlockController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlockController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var blocks = await _context.Block
                                       .Include(b => b.Kats) // İsteğe bağlı
                                       .ToListAsync();
            return View(blocks);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Block model)
        {
            if (ModelState.IsValid)
            {
                _context.Block.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var block = await _context.Block.FindAsync(id);
            if (block == null) return NotFound();

            return View(block);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Block model)
        {
            if (ModelState.IsValid)
            {
                var block = await _context.Block.FindAsync(model.Id);
                if (block == null) return NotFound();

                block.Ad = model.Ad;
                // Diğer alanlar da varsa güncellenir

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var block = await _context.Block.FindAsync(id);
            if (block == null) return NotFound();

            return View(block);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var block = await _context.Block.FindAsync(id);
            if (block == null) return NotFound();

            // Not: Eğer block'un kats vs. varsa silmeden önce kontrol 
            _context.Block.Remove(block);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
