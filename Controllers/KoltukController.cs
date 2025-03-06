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

    public class KoltukController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KoltukController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Koltuk/Index?katNo=1
        // maxRow, maxCol artık veritabanından gelecek
        public IActionResult Index(int katNo = 1)
        {
            // 1) Bu katın MaxRow ve MaxCol bilgilerini KoltukIndexViewModel tablosundan çek
            var duzen = _context.Katlar
                                .FirstOrDefault(k => k.KatNo == katNo);

            // Eğer o kat için henüz bir düzen yoksa varsayılan değerler belirleyebilirsiniz
            int maxRow = duzen?.MaxRow ?? 4;
            int maxCol = duzen?.MaxCol ?? 4;

            // 2) İlgili kattaki koltukları çek
            var koltuklar = _context.Koltuklar
                                    .Where(k => k.KatNo == katNo)
                                    .ToList();

            // 3) Görünümde kullanmak üzere bir model (ya da ViewBag vb.) hazırlayabilirsiniz
            ViewBag.KatNo = katNo;
            ViewBag.MaxRow = maxRow;
            ViewBag.MaxCol = maxCol;

            return View(koltuklar);
        }

        // GET: /Koltuk/Create?row=0&col=0&katNo=1
        [HttpGet]
        public IActionResult Create(int row, int col, int katNo)
        {
            var koltuk = new Koltuk
            {
                RowIndex = row,
                ColumnIndex = col,
                KatNo = katNo
            };
            return View(koltuk);
        }

        // POST: /Koltuk/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Koltuk model)
        {
            if (ModelState.IsValid)
            {
                _context.Koltuklar.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index", new { katNo = model.KatNo });
            }
            return View(model);
        }

        // GET: /Koltuk/Edit/5
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var koltuk = _context.Koltuklar.Find(id);
            if (koltuk == null)
            {
                return NotFound();
            }
            return View(koltuk);
        }

        // POST: /Koltuk/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Koltuk model)
        {
            if (ModelState.IsValid)
            {
                var koltuk = _context.Koltuklar.Find(model.Id);
                if (koltuk == null)
                {
                    return NotFound();
                }

                koltuk.KatNo = model.KatNo;
                koltuk.RowIndex = model.RowIndex;
                koltuk.ColumnIndex = model.ColumnIndex;
                koltuk.Durum = model.Durum;
                koltuk.Aciklama = model.Aciklama;
                koltuk.koltukNo = model.koltukNo;

                _context.SaveChanges();
                return RedirectToAction("Index", new { katNo = model.KatNo });
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]

        // GET: /Koltuk/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var koltuk = _context.Koltuklar.Find(id);
            if (koltuk == null)
            {
                return NotFound();
            }
            return View(koltuk);
        }

        // POST: /Koltuk/DeleteConfirmed/5
        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var koltuk = _context.Koltuklar.Find(id);
            if (koltuk == null)
            {
                return NotFound();
            }

            var katNo = koltuk.KatNo;
            _context.Koltuklar.Remove(koltuk);
            _context.SaveChanges();
            return RedirectToAction("Index", new { katNo });
        }

        //=============================================
        // ========== KAT DÜZENİ (MaxRow, MaxCol) =====
        //=============================================

        // GET: /Koltuk/EditDuzeni?katNo=1
        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult EditDuzeni(int katNo)
        {
            // Bu katın düzeni var mı?
            var duzen = _context.Katlar
                                .FirstOrDefault(k => k.KatNo == katNo);

            // Henüz kaydı yoksa yeni oluştur.
            if (duzen == null)
            {
                duzen = new Kat
                {
                    KatNo = katNo,
                    MaxRow = 4,
                    MaxCol = 4
                };
            }
            return View(duzen);
        }

        // POST: /Koltuk/EditDuzeni
        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditDuzeni(Kat model)
        {
            if (ModelState.IsValid)
            {
                var existing = _context.Katlar
                                       .FirstOrDefault(k => k.KatNo == model.KatNo);
                // Kayıt varsa güncelle
                if (existing != null)
                {
                    existing.MaxRow = model.MaxRow;
                    existing.MaxCol = model.MaxCol;
                }
                // Kayıt yoksa ekle
                else
                {
                    _context.Katlar.Add(new Kat
                    {
                        KatNo = model.KatNo,
                        MaxRow = model.MaxRow,
                        MaxCol = model.MaxCol
                    });
                }

                _context.SaveChanges();
                return RedirectToAction("Index", new { katNo = model.KatNo });
            }
            return View(model);
        }
    }
}
