using KTRS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace KTRS.Controllers
{
    public class YetkiliController : Controller
    {
        private readonly ApplicationDbContext _context;

        public YetkiliController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var koltuklar = await _context.Set<Koltuk>().ToListAsync();
            return View(koltuklar);
        }

        [HttpPost]
        public async Task<IActionResult> KoltukGuncelle(int id, string yeniKoltukNo)
        {
            var koltuk = await _context.Set<Koltuk>().FindAsync(id);
            if (koltuk != null)
            {
                koltuk.koltukNo = yeniKoltukNo;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Error");
        }
    }
}


