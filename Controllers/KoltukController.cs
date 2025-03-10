using KTRS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KTRS.Controllers
{
    // [Authorize(Roles="Admin")] // İster eklersiniz
    public class KoltukController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KoltukController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Koltuk/Index?katId=...
        // Seçili katın koltuklarını listeler. (Siz isterseniz grid ya da tablo gösterebilirsiniz.)
        public async Task<IActionResult> Index(int katId)
        {
            var koltuklar = await _context.Koltuklar
                                          .Include(k => k.Kat)
                                          .Where(k => k.KatId == katId)
                                          .OrderBy(k => k.KoltukNo) // veya Id, vs.
                                          .ToListAsync();

            ViewBag.Kat = await _context.Katlar
                                        .Include(x => x.Block)
                                        .FirstOrDefaultAsync(x => x.Id == katId);

            return View(koltuklar);
        }

        // GET: /Koltuk/Create?katId=...
        // Basit form sayfası (row=0, col=0 opsiyonel parametre)
        [HttpGet]
        public async Task<IActionResult> Create(int katId)
        {
            var kat = await _context.Katlar.FindAsync(katId);
            if (kat == null) return NotFound();

            // Yeni koltuk modeli varsayılan değerlerle
            var model = new Koltuk
            {
                KatId = katId,
                XCoord = 0,
                YCoord = 0,
                Durum = false // varsayılan "boş"
            };
            return View(model);
        }

        // POST: /Koltuk/Create
        // Eğer admin panelinden normal form submit yapıyorsanız, "return Redirect(...)" diyebilirsiniz.
        // Eğer sürükle-bırak + Ajax mantığı kullanıyorsanız, "return Content(...)" veya JSON dönebilirsiniz.
        [HttpPost]
        public IActionResult Create([FromForm] Koltuk model)
        {
            if (ModelState.IsValid)
            {
                _context.Koltuklar.Add(model);
                _context.SaveChanges();
                return Content(model.Id.ToString()); // Yeni ID
            }
            // ModelState hatalarını toplayalım
            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage);
            return BadRequest("Invalid model: " + string.Join("; ", errors));
        }

        // GET: /Koltuk/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var koltuk = await _context.Koltuklar.FindAsync(id);
            if (koltuk == null) return NotFound();

            return View(koltuk);
        }

        // POST: /Koltuk/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Koltuk model)
        {
            if (ModelState.IsValid)
            {
                var koltuk = await _context.Koltuklar.FindAsync(model.Id);
                if (koltuk == null) return NotFound();

                // X/Y konumu da güncellenebilir
                koltuk.XCoord = model.XCoord;
                koltuk.YCoord = model.YCoord;
                koltuk.Durum = model.Durum;
                koltuk.KoltukNo = model.KoltukNo;
                koltuk.Aciklama = model.Aciklama;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { katId = koltuk.KatId });
            }
            return View(model);
        }

        // POST: /Koltuk/EditPosition
        // Sürükle-bırak senaryosunda sadece XCoord/YCoord güncelleyen basit bir endpoint
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

        // GET: /Koltuk/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var koltuk = await _context.Koltuklar
                                       .Include(k => k.Kat)
                                       .FirstOrDefaultAsync(k => k.Id == id);
            if (koltuk == null)
                return NotFound();

            return View(koltuk);
        }

        // POST: /Koltuk/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var koltuk = await _context.Koltuklar.FindAsync(id);
            if (koltuk == null)
                return NotFound();

            var katId = koltuk.KatId;
            _context.Koltuklar.Remove(koltuk);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { katId });
        }
    }
}
