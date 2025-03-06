using KTRS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace KTRS.Controllers
{
    [Authorize(Roles = "Admin")]
    public class KatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KatController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Kat/Index
        public async Task<IActionResult> Index()
        {
            var katlar = await _context.Katlar
                                       .OrderBy(k => k.KatNo)
                                       .ToListAsync();
            return View(katlar);
        }

        // GET: /Kat/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Kat/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kat model)
        {
            if (ModelState.IsValid)
            {
                _context.Katlar.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: /Kat/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var kat = await _context.Katlar.FindAsync(id);
            if (kat == null)
            {
                return NotFound();
            }
            return View(kat);
        }

        // POST: /Kat/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Kat model)
        {
            if (ModelState.IsValid)
            {
                var kat = await _context.Katlar.FindAsync(model.Id);
                if (kat == null)
                {
                    return NotFound();
                }

                kat.KatNo = model.KatNo;
                kat.Ad = model.Ad;

                _context.Katlar.Update(kat);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: /Kat/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var kat = await _context.Katlar.FindAsync(id);
            if (kat == null)
            {
                return NotFound();
            }
            return View(kat);
        }

        // POST: /Kat/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kat = await _context.Katlar.FindAsync(id);
            if (kat == null)
            {
                return NotFound();
            }

            _context.Katlar.Remove(kat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
