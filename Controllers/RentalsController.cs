using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRentalMVC.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CarRentalMVC.Controllers
{
    public class RentalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Rentals.Include(r => r.Car);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars.Select(c => new { c.Id, DisplayText = c.Make + " " + c.Model }), "Id", "DisplayText");
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CarId,RenterName,RentalDate,ReturnDate")] Rental rental)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == rental.CarId);
            if (!_context.Cars.Any(c => c.Id == rental.CarId))
            {
                ModelState.AddModelError("CarId", "Wybrany samochód nie istnieje.");
            }
            bool isOverlapping = _context.Rentals.Any(r => r.CarId == rental.CarId && r.RentalDate < rental.ReturnDate && r.ReturnDate > rental.RentalDate);
            if (isOverlapping)
            {
                ModelState.AddModelError("", "Wybrany samochód jest już zarezerwowany w tym terminie.");
            }
            if(!car.IsAvailable)
            {
                ModelState.AddModelError("", "Wybrany samochód nie jest dostępny");
            }
            if (ModelState.IsValid)
            {
                Console.WriteLine($"Rental Data: {rental.CarId}, {rental.RenterName}, {rental.RentalDate}, {rental.ReturnDate}");
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars.Select(c => new { c.Id, DisplayText = c.Make + " " + c.Model }), "Id", "DisplayText", rental.CarId);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).ToList();
                // Logowanie błędów, jeśli są
                foreach (var error in errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                // Debugowanie, sprawdź dane rental
                Console.WriteLine($"CarId: {rental.CarId}, RenterName: {rental.RenterName}, RentalDate: {rental.RentalDate}, ReturnDate: {rental.ReturnDate}");
            }
            return View(rental);
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", rental.CarId);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CarId,RenterName,RentalDate,ReturnDate")] Rental rental)
        {
            if (id != rental.Id)
            {
                return NotFound();
            }           
            bool isOverlapping = _context.Rentals.Any(r => r.CarId == rental.CarId && r.Id != rental.Id && r.RentalDate < rental.ReturnDate && r.ReturnDate > rental.RentalDate);

            if (isOverlapping)
                {
                    ModelState.AddModelError("", "Wybrany samochód jest już zarezerwowany w tym terminie.");
                }
            var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == rental.CarId);
            if (car != null && !car.IsAvailable)
            {
                ModelState.AddModelError("", "Wybrany samochód nie jest dostępny.");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.Id))
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
            ViewData["CarId"] = new SelectList(_context.Cars.Select(c => new { c.Id, DisplayText = c.Make + " " + c.Model }), "Id", "DisplayText", rental.CarId);
            return View(rental);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rental = await _context.Rentals
                .Include(r => r.Car)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental != null)
            {
                _context.Rentals.Remove(rental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.Id == id);
        }
    }
}
