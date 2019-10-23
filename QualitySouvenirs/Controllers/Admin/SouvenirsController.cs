using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QualitySouvenirs.Data;
using QualitySouvenirs.Models;
using QualitySouvenirs.Models.Extended;

namespace QualitySouvenirs.Controllers.Admin
{
    public class SouvenirsController : Controller
    {
        private readonly SouvenirContext _context;

        public SouvenirsController(SouvenirContext context)
        {
            _context = context;
        }

        // GET: Souvenirs
        public async Task<IActionResult> Index()
        {
            var souvenirContext = _context.Souvenirs.Include(s => s.Category).Include(s => s.Supplier);
            return View(await souvenirContext.ToListAsync());
        }

        // GET: Souvenirs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs
                .Include(s => s.Category)
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.SouvenirID == id);
            if (souvenir == null)
            {
                return NotFound();
            }

            return View(souvenir);
        }

        // GET: Souvenirs/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID");
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID");
            return View();
        }

        // POST: Souvenirs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PSouvenir souvenir)
        {
            if (ModelState.IsValid)
            {
                var sou = new Souvenir();
                using (var ms = new MemoryStream())
                {
                   
                    await souvenir.image.CopyToAsync(ms);
                    sou.SouImage = ms.ToArray();
                    sou.SouQuantity = souvenir.SouQuantity;
                    sou.SouDescription = souvenir.SouDescription;
                    sou.SouName = souvenir.SouName;
                    sou.SouPrice = souvenir.SouPrice;
                    sou.SupplierID = souvenir.SupplierID;
                    sou.CategoryID = souvenir.CategoryID;

                }

                _context.Add(sou);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", souvenir.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", souvenir.SupplierID);
            return View(souvenir);
        }

        // GET: Souvenirs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs.FindAsync(id);
            if (souvenir == null)
            {
                return NotFound();
            }
            var sou = new EditSouvenir();
            using (var ms = new MemoryStream())
            {

                sou.SouvenirID = souvenir.SouvenirID;
                sou.sImage = souvenir.SouImage;
                sou.SouQuantity = souvenir.SouQuantity;
                sou.SouDescription = souvenir.SouDescription;
                sou.SouName = souvenir.SouName;
                sou.SouPrice = souvenir.SouPrice;
                sou.SupplierID = souvenir.SupplierID;
                sou.CategoryID = souvenir.CategoryID;

            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", souvenir.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", souvenir.SupplierID);
            return View(sou);
        }

        // POST: Souvenirs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,EditSouvenir souvenir)
        {
            if (id != souvenir.SouvenirID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
              
                try
                {

                    var sou = new Souvenir();
                    using (var ms = new MemoryStream())
                    {

                        await souvenir.image.CopyToAsync(ms);
                        sou.SouvenirID = souvenir.SouvenirID;
                        sou.SouImage = ms.ToArray();
                        sou.SouQuantity = souvenir.SouQuantity;
                        sou.SouDescription = souvenir.SouDescription;
                        sou.SouName = souvenir.SouName;
                        sou.SouPrice = souvenir.SouPrice;
                        sou.SupplierID = souvenir.SupplierID;
                        sou.CategoryID = souvenir.CategoryID;

                    }
                    _context.Update(sou);
                    await _context.SaveChangesAsync();
                }

                catch (DbUpdateConcurrencyException)
                {
                    if (!SouvenirExists(souvenir.SouvenirID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryID", souvenir.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", souvenir.SupplierID);
            return View(souvenir);
        }

        // GET: Souvenirs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var souvenir = await _context.Souvenirs
                .Include(s => s.Category)
                .Include(s => s.Supplier)
                .FirstOrDefaultAsync(m => m.SouvenirID == id);
            if (souvenir == null)
            {
                return NotFound();
            }

            return View(souvenir);
        }

        // POST: Souvenirs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var souvenir = await _context.Souvenirs.FindAsync(id);
            _context.Souvenirs.Remove(souvenir);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SouvenirExists(int id)
        {
            return _context.Souvenirs.Any(e => e.SouvenirID == id);
        }
    }
}
