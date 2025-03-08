using KTRS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace KTRS.Controllers
{
    // [Authorize(Roles="Admin")]
    public class KatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KatController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int blockId)
        {
            // Belirli bir blockId'ye ait katları listeler
            var kats = await _context.Katlar
                                     .Include(k => k.Block)
                                     .Where(k => k.BlockId == blockId)
                                     .OrderBy(k => k.KatNo)
                                     .ToListAsync();

            ViewBag.Block = await _context.Block.FindAsync(blockId);
            return View(kats);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int blockId)
        {
            // blockId parametresiyle hangi blok'a ekleyeceğimizi belirtiyoruz
            var block = await _context.Block.FindAsync(blockId);
            if (block == null) return NotFound();

            var model = new Kat
            {
                BlockId = blockId,
                // KatNo vb. default değerler
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kat model)
        {
            if (ModelState.IsValid)
            {
                _context.Katlar.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { blockId = model.BlockId });
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult PlanEditor(int katId = 1)
        {
            // Bu katin bilgilerini ve varsa mevcut koltuklarini cekelim
            var kat = _context.Katlar
                              .Include(k => k.Koltuklar)
                              .FirstOrDefault(k => k.Id == katId);
            if (kat == null)
                return NotFound();

            ViewBag.Kat = kat;
            // Kat.PlanImage gibi bir alana kaydediyorsanız, oradan da resmi alabilirsiniz
            // Sadece sabit /images/kutuphanePlan.png de olabilir
            ViewBag.PlanImage = Url.Content("~/A_Kat_Arastirma Salonu.jpg");
            return View(kat.Koltuklar.ToList());
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var kat = await _context.Katlar.FindAsync(id);
            if (kat == null) return NotFound();

            return View(kat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Kat model)
        {
            if (ModelState.IsValid)
            {
                var kat = await _context.Katlar.FindAsync(model.Id);
                if (kat == null) return NotFound();

                kat.KatNo = model.KatNo;
                kat.MaxRow = model.MaxRow;
                kat.MaxCol = model.MaxCol;
                // block değişikliğine izin vermek isterseniz blockId de güncellenebilir

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { blockId = kat.BlockId });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var kat = await _context.Katlar
                                    .Include(k => k.Block)
                                    .FirstOrDefaultAsync(k => k.Id == id);
            if (kat == null) return NotFound();

            return View(kat);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kat = await _context.Katlar.FindAsync(id);
            if (kat == null) return NotFound();

            var blockId = kat.BlockId;
            _context.Katlar.Remove(kat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { blockId });
        }
    }
}
