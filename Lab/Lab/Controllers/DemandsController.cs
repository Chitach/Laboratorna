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
    public class DemandsController : DefaultController
    {
        public DemandsController(ApplicationDbContext context):base(context)
        {
        }

        // GET: Demands
        public async Task<IActionResult> Index()
        {
            return View(await _context.Demands.ToListAsync());
        }

        // GET: Demands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demand = await _context.Demands
                .SingleOrDefaultAsync(m => m.Id == id);
            if (demand == null)
            {
                return NotFound();
            }

            return View(demand);
        }

        // GET: Demands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Demands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DemandName,LowerBorder,UpperBorder")] Demand demand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(demand);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(demand);
        }

        // GET: Demands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demand = await _context.Demands.SingleOrDefaultAsync(m => m.Id == id);
            if (demand == null)
            {
                return NotFound();
            }
            return View(demand);
        }

        // POST: Demands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DemandName,LowerBorder,UpperBorder")] Demand demand)
        {
            if (id != demand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(demand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DemandExists(demand.Id))
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
            return View(demand);
        }

        // GET: Demands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demand = await _context.Demands
                .SingleOrDefaultAsync(m => m.Id == id);
            if (demand == null)
            {
                return NotFound();
            }

            return View(demand);
        }

        // POST: Demands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var demand = await _context.Demands.SingleOrDefaultAsync(m => m.Id == id);
            _context.Demands.Remove(demand);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DemandExists(int id)
        {
            return _context.Demands.Any(e => e.Id == id);
        }
    }
}
