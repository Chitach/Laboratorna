using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab.Data;
using Lab.Models;

namespace Lab.Controllers
{
    public class ConductingsController : DefaultController
    {
        public ConductingsController(ApplicationDbContext context):base(context)
        {
        }

        // GET: Conductings
        public async Task<IActionResult> Index()
        {
            
            return View(await _context.Conductings.ToListAsync());
        }

        // GET: Conductings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conducting = await _context.Conductings
                .SingleOrDefaultAsync(m => m.Id == id);
            if (conducting == null)
            {
                return NotFound();
            }

            return View(conducting);
        }

        // GET: Conductings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conductings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TestDate,Comments")] Conducting conducting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conducting);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(conducting);
        }

        // GET: Conductings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conducting = await _context.Conductings.SingleOrDefaultAsync(m => m.Id == id);
            if (conducting == null)
            {
                return NotFound();
            }
            return View(conducting);
        }

        // POST: Conductings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TestDate,Comments")] Conducting conducting)
        {
            if (id != conducting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conducting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConductingExists(conducting.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(conducting);
        }

        // GET: Conductings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conducting = await _context.Conductings
                .SingleOrDefaultAsync(m => m.Id == id);
            if (conducting == null)
            {
                return NotFound();
            }

            return View(conducting);
        }

        // POST: Conductings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conducting = await _context.Conductings.SingleOrDefaultAsync(m => m.Id == id);
            _context.Conductings.Remove(conducting);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ConductingExists(int id)
        {
            return _context.Conductings.Any(e => e.Id == id);
        }
    }
}