using KTRS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KTRS.Controllers
{
    public class OgrenciController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OgrenciController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /Ogrenci/Index?katNo=1
        // Bu action, ilgili kattaki TÜM ekli koltukları listeler (Durum true/false fark etmez).
        public async Task<IActionResult> Index(int katNo = 1)
        {
            // Koltuk tablosunda KatNo = verilen katNo olanları çekiyoruz.
            // "Sadece eklenmiş koltuklar" = zaten Koltuk tablosunda kaydı olanlar.
            //var koltuklar = await _context.Koltuklar
            //                              .Where(k => k.KatNo == katNo)
            //                              .OrderBy(k => k.RowIndex)
            //                              .ThenBy(k => k.ColumnIndex)
            //                              .ToListAsync();

            var koltuklar = _context.Koltuklar
                        .Where(k => k.KatNo == katNo)
                        .ToList();
            var kat = _context.Katlar.Find(katNo);
            if (kat == null) return NotFound();

            int maxRow = kat.MaxRow;
            int maxCol = kat.MaxCol;

            ViewBag.KatNo = katNo; // Görünümde kullanmak üzere kat bilgisini tutuyoruz.
            ViewBag.MaxRow = maxRow; // Görünümde kullanmak üzere kat bilgisini tutuyoruz.
            ViewBag.MaxCol = maxCol; // Görünümde kullanmak üzere kat bilgisini tutuyoruz.

            return View(koltuklar);
        }

        // POST /Ogrenci/RezervasyonYap
        // Öğrenci, KoltukNo veya KoltukId üzerinden rezervasyon isteğinde bulunur
        [HttpPost]
        public async Task<IActionResult> RezervasyonYap(string ogrenciID, string koltukNo)
        {
            // Koltuğu bul → Durum = false (yani boş/müsait) ise rezerve et
            var koltuk = await _context.Koltuklar
                                       .FirstOrDefaultAsync(k => k.koltukNo == koltukNo && !k.Durum);
            if (koltuk != null)
            {
                // Koltuk rezerve olsun
                koltuk.Durum = true; // Artık dolu

                // Rezervasyon tablosuna da ek kayıt atıyoruz (opsiyonel)
                var rezervasyon = new Rezervasyon
                {
                    RezervasyonID = System.Guid.NewGuid().ToString(),
                    Durum = true,
                    ogrenciID = ogrenciID,
                    KoltukNo = koltukNo
                };
                _context.Rezervasyonlar.Add(rezervasyon);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { katNo = koltuk.KatNo });
            }
            // Hata durumu
            return View("Error");
        }
    }
}
