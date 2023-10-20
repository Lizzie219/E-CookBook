using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_CookBook.Data;
using E_CookBook.Models;

namespace E_CookBook.Controllers
{
    public class QuantityMetricsController : Controller
    {
        private readonly TastyDbContext _context;

        public QuantityMetricsController(TastyDbContext context)
        {
            _context = context;
        }

        // GET: QuantityMetrics
        public async Task<IActionResult> Index()
        {
              return _context.QuantityMetric != null ? 
                          View(await _context.QuantityMetric.ToListAsync()) :
                          Problem("Entity set 'TastyDbContext.QuantityMetric'  is null.");
        }

        // GET: QuantityMetrics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.QuantityMetric == null)
            {
                return NotFound();
            }

            var quantityMetric = await _context.QuantityMetric
                .FirstOrDefaultAsync(m => m.ID == id);
            if (quantityMetric == null)
            {
                return NotFound();
            }

            return View(quantityMetric);
        }

        // GET: QuantityMetrics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuantityMetrics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] QuantityMetric quantityMetric)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quantityMetric);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quantityMetric);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task Create(string metricName)
        {
            if (!QuantityMetricExists(metricName))
            {
                QuantityMetric metric = new QuantityMetric();
                metric.Name = metricName;

                _context.QuantityMetric.Add(metric);
                await _context.SaveChangesAsync();
            }
        }

        // GET: QuantityMetrics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.QuantityMetric == null)
            {
                return NotFound();
            }

            var quantityMetric = await _context.QuantityMetric.FindAsync(id);
            if (quantityMetric == null)
            {
                return NotFound();
            }
            return View(quantityMetric);
        }

        // POST: QuantityMetrics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name")] QuantityMetric quantityMetric)
        {
            if (id != quantityMetric.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quantityMetric);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuantityMetricExists(quantityMetric.ID))
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
            return View(quantityMetric);
        }

        // GET: QuantityMetrics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.QuantityMetric == null)
            {
                return NotFound();
            }

            var quantityMetric = await _context.QuantityMetric
                .FirstOrDefaultAsync(m => m.ID == id);
            if (quantityMetric == null)
            {
                return NotFound();
            }

            return View(quantityMetric);
        }

        // POST: QuantityMetrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.QuantityMetric == null)
            {
                return Problem("Entity set 'TastyDbContext.QuantityMetric'  is null.");
            }
            var quantityMetric = await _context.QuantityMetric.FindAsync(id);
            if (quantityMetric != null)
            {
                _context.QuantityMetric.Remove(quantityMetric);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public int GetQuantityMetric(string name)
        {
            return _context.QuantityMetric.Where(q => q.Name == name).Select(q => q.ID).FirstOrDefault();
        }

        private bool QuantityMetricExists(int id)
        {
          return (_context.QuantityMetric?.Any(e => e.ID == id)).GetValueOrDefault();
        }
        private bool QuantityMetricExists(string name)
        {
            return (_context.QuantityMetric?.Any(e => e.Name == name)).GetValueOrDefault();
        }
    }
}
