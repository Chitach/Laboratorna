using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lab.Data;
using Lab.Models;
using Microsoft.AspNetCore.Authorization;
using Lab.Models.ViewModels;

namespace Lab.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DemandsController : DefaultController
    {
        public DemandsController(ApplicationDbContext context):base(context)
        {
        }

        // GET: Demands
        public async Task<IActionResult> Index()
        {
            return View(await _context.Demands.Include(x => x.Test).ToListAsync());
        }

        // GET: Demands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var demand = await _context.Demands.Include(x => x.Test)
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
            var d = new Lab.Models.ViewModels.ViewDemand()
            {
                Tests = _context.Tests.Select(x => new SelectListItem()
                {
                    Text = x.TestName,
                    Value = x.Id.ToString()
                })
            };
            return View(d);
        }

        // POST: Demands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DemandName,LowerBorder,UpperBorder")] Demand demand, int Tests)
        {
            if (ModelState.IsValid)
            {
                var Test = await _context.Tests.SingleOrDefaultAsync(x => x.Id == Tests);
                if (Test != null)
                {
                    demand.Test = Test;
                }
                else
                {
                    NotFound();
                }
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

            var demand = await _context.Demands.Include(x => x.Test).SingleOrDefaultAsync(m => m.Id == id);
            if (demand == null)
            {
                return NotFound();
            }
            var d = new Lab.Models.ViewModels.ViewDemand()
            {
                DemandName = demand.DemandName,
                Id = demand.Id,
                LowerBorder = demand.LowerBorder,
                Test = demand.Test,
                UpperBorder = demand.UpperBorder,
                Tests = _context.Tests.Select(x => new SelectListItem()
                {
                    Selected = x.Id == demand.Test.Id,
                    Text = x.TestName,
                    Value = x.Id.ToString()
                })
            };
            return View(d);
        }

        // POST: Demands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DemandName,LowerBorder,UpperBorder")] Demand demand, int Tests)
        {
            if (id != demand.Id)
            {
                return NotFound();
            }

            var Test = await _context.Tests.SingleOrDefaultAsync(x => x.Id == Tests);
            if(Test != null)
            {
                demand.Test = Test;
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

            var demand = await _context.Demands.Include(x => x.Test)
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
