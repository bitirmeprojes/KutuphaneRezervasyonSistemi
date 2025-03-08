using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KTRS.Models;
using Microsoft.AspNetCore.Authorization;

namespace KTRS.Controllers
{
    // [Authorize(Roles="Admin")]
    public class KoltukController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KoltukController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Kat bazlı koltukları listelemek
        public async Task<IActionResult> Index(int katId)
        {
            var koltuklar = await _context.Koltuklar
                                          .Include(k => k.Kat)
                                          .Where(k => k.KatId == katId)
                                          .OrderBy(k => k.RowIndex)
                                          .ThenBy(k => k.ColumnIndex)
                                          .ToListAsync();

            ViewBag.Kat = await _context.Katlar
                                        .Include(x => x.Block)
                                        .FirstOrDefaultAsync(x => x.Id == katId);

            return View(koltuklar);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int katId, int row = 0, int col = 0)
        {
            var kat = await _context.Katlar.FindAsync(katId);
            if (kat == null) return NotFound();

            var model = new Koltuk
            {
                KatId = katId,
                RowIndex = row,
                ColumnIndex = col,
                Durum = false // varsayılan boş
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create([FromForm] Koltuk model)
        {
            if (ModelState.IsValid)
            {
                // model.KatId, model.XCoord, model.YCoord, model.KoltukNo, model.Durum vs. doldurulmuş olacak
                _context.Koltuklar.Add(model);
                _context.SaveChanges();
                return Content("OK"); // Veya Json(new { success=true })
            }
            return BadRequest("Invalid model");
        }





        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var koltuk = await _context.Koltuklar.FindAsync(id);
            if (koltuk == null) return NotFound();

            return View(koltuk);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Koltuk model)
        {
            if (ModelState.IsValid)
            {
                var koltuk = await _context.Koltuklar.FindAsync(model.Id);
                if (koltuk == null) return NotFound();

                koltuk.RowIndex = model.RowIndex;
                koltuk.ColumnIndex = model.ColumnIndex;
                koltuk.Durum = model.Durum;
                koltuk.KoltukNo = model.KoltukNo;
                koltuk.Aciklama = model.Aciklama;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { katId = koltuk.KatId });
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult EditPosition(int id, int XCoord, int YCoord)
        {
            var koltuk = _context.Koltuklar.Find(id);
            if (koltuk == null)
                return NotFound();

            koltuk.XCoord = XCoord;
            koltuk.YCoord = YCoord;
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var koltuk = await _context.Koltuklar
                                       .Include(k => k.Kat)
                                       .FirstOrDefaultAsync(k => k.Id == id);
            if (koltuk == null) return NotFound();

            return View(koltuk);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var koltuk = await _context.Koltuklar.FindAsync(id);
            if (koltuk == null) return NotFound();

            var katId = koltuk.KatId;
            _context.Koltuklar.Remove(koltuk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { katId });
        }
    }
}
