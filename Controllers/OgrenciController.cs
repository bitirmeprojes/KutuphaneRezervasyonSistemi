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

        public async Task<IActionResult> Index()
        {
            var koltuklar = await _context.Set<Koltuk>().Where(k => !k.Durum).ToListAsync();
            return View(koltuklar);
        }

        [HttpPost]
        public async Task<IActionResult> RezervasyonYap(string ogrenciID, string koltukNo)
        {
            var koltuk = await _context.Set<Koltuk>().FirstOrDefaultAsync(k => k.koltukNo == koltukNo && !k.Durum);
            if (koltuk != null)
            {
                koltuk.Durum = true;
                var rezervasyon = new Rezervasyon
                {
                    RezervasyonID = System.Guid.NewGuid().ToString(),
                    Durum = true,
                    ogrenciID = ogrenciID
                };
                _context.Set<Rezervasyon>().Add(rezervasyon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Error");
        }
    }
}


