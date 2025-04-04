using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FormBudAdmin.Data;
using FormBudAdmin.Models;

namespace FormBudAdmin.Controllers
{
    public class BuyersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BuyersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Buyers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Buyer.ToListAsync());
        }

        // GET: Buyers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = await _context.Buyer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buyer == null)
            {
                return NotFound();
            }

            return View(buyer);
        }

        // GET: Buyers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buyers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BuyerName,BuyerEmail,BuyerPhone")] Buyer buyer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buyer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buyer);
        }

        // GET: Buyers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = await _context.Buyer.FindAsync(id);
            if (buyer == null)
            {
                return NotFound();
            }
            return View(buyer);
        }

        // POST: Buyers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BuyerName,BuyerEmail,BuyerPhone")] Buyer buyer)
        {
            if (id != buyer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buyer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuyerExists(buyer.Id))
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
            return View(buyer);
        }

        // GET: Buyers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = await _context.Buyer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (buyer == null)
            {
                return NotFound();
            }

            return View(buyer);
        }

        // POST: Buyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            //Ta bort alla bud innan köpare tas bort
            var bids = await _context.Bid.Where(b => b.ProductId == id).ToListAsync();
            if (bids != null && bids.Count > 0)
            {
                _context.Bid.RemoveRange(bids);
            }

            var buyer = await _context.Buyer.FindAsync(id);
            if (buyer != null)
            {
                _context.Buyer.Remove(buyer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuyerExists(int id)
        {
            return _context.Buyer.Any(e => e.Id == id);
        }
    }
}
