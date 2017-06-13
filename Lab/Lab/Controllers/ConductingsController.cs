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
    [Authorize(Roles = "Doctor")]
    public class ConductingsController : DefaultController
    {
        public ConductingsController(ApplicationDbContext context):base(context)
        {
        }

        // GET: Conductings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Conductings.Include(x => x.Patient).Include(x => x.Test).ToListAsync());
        }

        // GET: Conductings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conducting = await _context.Conductings.Include(x=> x.Patient).Include(x => x.Test)
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
            var d = new Lab.Models.ViewModels.ViewConducting()
            {
                Tests = _context.Tests.Select(x => new SelectListItem()
                {
                    Text = x.TestName,
                    Value = x.Id.ToString()
                }),
                Patients = _context.Patients.Select(x => new SelectListItem()
                {
                    Text = x.FirstName + " " + x.Lastname,
                    Value = x.Id.ToString()
                })
            };
            return View(d);
        }

        // POST: Conductings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TestDate,Comments")] Conducting conducting, int Tests, int Patients)
        {
            if (ModelState.IsValid)
            {

                var Test = await _context.Tests.SingleOrDefaultAsync(x => x.Id == Tests);
                if (Test != null)
                {
                    conducting.Test = Test;
                }
                else
                {
                    NotFound();
                }
                var Patient = await _context.Patients.SingleOrDefaultAsync(x => x.Id == Patients);
                if (Patient != null)
                {
                    conducting.Patient = Patient;
                }
                else
                {
                    NotFound();
                }

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
            
            var conducting = await _context.Conductings.Include(x => x.Patient).Include(x => x.Test).SingleOrDefaultAsync(m => m.Id == id);

            if (conducting == null)
            {
                return NotFound();
            }

            var d = new Lab.Models.ViewModels.ViewConducting()
            {
                Comments = conducting.Comments,
                Id = conducting.Id,
                Patient = conducting.Patient,
                Test = conducting.Test,
                Tests = _context.Tests.Select(x => new SelectListItem()
                {
                    Selected = x.Id == conducting.Test.Id,
                    Text = x.TestName,
                    Value = x.Id.ToString()
                }),
                Patients = _context.Patients.Select(x => new SelectListItem()
                {
                    Selected = x.Id == conducting.Patient.Id,
                    Text = x.FirstName+" "+x.Lastname,
                    Value = x.Id.ToString()
                })
            };

            return View(d);
        }

        // POST: Conductings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TestDate,Comments")] Conducting conducting, int Tests, int Patients)
        {
            if (id != conducting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var Test = await _context.Tests.SingleOrDefaultAsync(x => x.Id == Tests);
                    if (Test != null)
                    {
                        conducting.Test = Test;
                    }
                    else
                    {
                        NotFound();
                    }
                    var Patient = await _context.Patients.SingleOrDefaultAsync(x => x.Id == Patients);
                    if (Patient != null)
                    {
                        conducting.Patient = Patient;
                    }
                    else
                    {
                        NotFound();
                    }

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

            var conducting = await _context.Conductings.Include(x => x.Patient).Include(x => x.Test)
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
