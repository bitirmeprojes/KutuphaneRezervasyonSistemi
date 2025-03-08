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

        // 1) BLOK SEÇME
        // GET: /Ogrenci/SelectBlock
        // Tüm blokları listeler, öğrenci hangi bloğu seçeceğini buradan tıklar
        public async Task<IActionResult> SelectBlock()
        {
            var blocks = await _context.Block
                                       .OrderBy(b => b.Ad)
                                       .ToListAsync();
            return View(blocks);
        }

        // 2) KAT SEÇME
        // GET: /Ogrenci/SelectKat?blockId=...
        // Seçilen bloğun katlarını listeler, öğrenci buradan bir katı tıklar
        public async Task<IActionResult> SelectKat(int blockId)
        {
            // Seçilen bloğu çek (içindeki Katlar'ı da almak isterseniz Include kullanabilirsiniz)
            var block = await _context.Block
                                      .Include(b => b.Kats)
                                      .FirstOrDefaultAsync(b => b.Id == blockId);
            if (block == null)
                return NotFound();

            return View(block);
        }

        // 3) KOLTUKLARI GÖRÜNTÜLE
        // GET: /Ogrenci/Index?katId=...
        // Belirli bir katın koltuklarını grid/liste şeklinde gösterir
        [HttpGet]
        public async Task<IActionResult> Index(int katId = 1)
        {
            var kat = await _context.Katlar
                                    .Include(x => x.Block)
                                    .FirstOrDefaultAsync(x => x.Id == katId);
            if (kat == null) return NotFound();

            // O kattaki koltukları çek
            var koltuklar = await _context.Koltuklar
                                          .Where(k => k.KatId == katId)
                                          .ToListAsync();

            // Gerekirse blueprint resminin adını Kat modelinde tutabilirsiniz (kat.PlanImagePath vs.)
            // Şimdilik sabit resim kullanacak varsayıyoruz.

            ViewBag.PlanImage = "/images/A_Kat_Arastirma Salonu.jpg";
            ViewBag.Kat = kat; // Kat bilgisini View'de kullanabilirsiniz
            return View(koltuklar); // Koltuk listesi model
        }


        // 4) REZERVASYON YAP
        // POST: /Ogrenci/RezervasyonYap
        // Kullanıcı koltuğa tıklayınca rezerve et
        [HttpPost]
        public async Task<IActionResult> RezervasyonYap(string ogrenciID, string koltukNo)
        {
            // Koltuğu bul → Durum=false => rezerve et
            var koltuk = await _context.Koltuklar
                                       .Include(k => k.Kat)
                                       .FirstOrDefaultAsync(k => k.KoltukNo == koltukNo && k.Durum == false);
            if (koltuk != null)
            {
                // Koltuğu dolu yap
                koltuk.Durum = true;

                // Rezervasyon tablosuna ek
                var rezervasyon = new Rezervasyon
                {
                    RezervasyonID = Guid.NewGuid().ToString(),
                    Durum = true,
                    ogrenciID = ogrenciID,
                    KoltukNo = koltukNo
                };
                _context.Rezervasyonlar.Add(rezervasyon);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { katId = koltuk.KatId });
            }
            return View("Error");
        }

    }
}
