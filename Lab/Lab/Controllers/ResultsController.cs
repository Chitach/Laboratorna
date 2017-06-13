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

namespace Lab.Controllers
{
    [Authorize]
    public class ResultsController : DefaultController
    {
        public ResultsController(ApplicationDbContext context) : base(context)
        {
        }

        // GET: Results
        public async Task<IActionResult> Index()
        {
            var d = await _context.Results.Include(x => x.Conducting).Include(x => x.Demand).Include(x => x.Conducting.Patient).Include(x => x.Conducting.Test).ToListAsync();
            return View(d);
        }

        // GET: Results/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results.Include(x => x.Conducting).Include(x => x.Demand).Include(x => x.Conducting.Patient).Include(x => x.Conducting.Test)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Results/Create
        public IActionResult Create()
        {
            var d = new Lab.Models.ViewModels.ViewResult()
            {
                Conductings = _context.Conductings.Select(x => new SelectListItem()
                {
                    Text = x.Patient.Lastname + " " + x.TestDate.ToString() + " " + x.Test.TestName,
                    Value = x.Id.ToString()
                }),
                Demands = _context.Demands.Select(x => new SelectListItem()
                {
                    Text = x.DemandName,
                    Value = x.Id.ToString()
                })
            };
            return View(d);
        }

        // POST: Results/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Comment")] Result result, int Conductings, int Demands)
        {
            if (ModelState.IsValid)
            {
                var Conducting = await _context.Conductings.SingleOrDefaultAsync(x => x.Id == Conductings);
                if (Conducting != null)
                {
                    result.Conducting = Conducting;
                }
                else
                {
                    NotFound();
                }
                var Demand = await _context.Demands.SingleOrDefaultAsync(x => x.Id == Demands);
                if (Demand != null)
                {
                    result.Demand = Demand;
                }
                else
                {
                    NotFound();
                }

                _context.Add(result);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(result);
        }

        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results.Include(x => x.Conducting).Include(x => x.Demand).Include(x => x.Conducting.Patient).Include(x => x.Conducting.Test).SingleOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }


            var d = new Lab.Models.ViewModels.ViewResult()
            {
                Comment = result.Comment,
                Conducting = result.Conducting,
                Demand = result.Demand,
                Id = result.Id,
                Value = result.Value,
                Conductings = _context.Conductings.Select(x => new SelectListItem()
                {
                    Text = x.Patient.Lastname + " " + x.TestDate.ToString() + " " + x.Test.TestName,
                    Value = x.Id.ToString()
                }),
                Demands = _context.Demands.Select(x => new SelectListItem()
                {
                    Text = x.DemandName,
                    Value = x.Id.ToString()
                })
            };

            return View(d);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Comment")] Result result, int Conductings, int Demands)
        {
            if (id != result.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var Conducting = await _context.Conductings.SingleOrDefaultAsync(x => x.Id == Conductings);
                    if (Conducting != null)
                    {
                        result.Conducting = Conducting;
                    }
                    else
                    {
                        NotFound();
                    }
                    var Demand = await _context.Demands.SingleOrDefaultAsync(x => x.Id == Demands);
                    if (Demand != null)
                    {
                        result.Demand = Demand;
                    }
                    else
                    {
                        NotFound();
                    }

                    _context.Update(result);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultExists(result.Id))
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
            return View(result);
        }

        // GET: Results/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _context.Results.Include(x => x.Conducting).Include(x => x.Demand).Include(x => x.Conducting.Patient).Include(x => x.Conducting.Test)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _context.Results.SingleOrDefaultAsync(m => m.Id == id);
            _context.Results.Remove(result);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ResultExists(int id)
        {
            return _context.Results.Any(e => e.Id == id);
        }
    }
}
