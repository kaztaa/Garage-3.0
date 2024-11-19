using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_3._0.Data;
using Garage_3._0.Models;

namespace Garage_3._0.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VehiclesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vehicles.Include(v => v.VehicleType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }


        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Name");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleTypeId,RegistrationNumber,Color,Brand,Model,ParkingSpotSize,NumberOfWheels")] Vehicle vehicle)
        {
            // Debugging ModelState for validation errors
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                            .Where(ms => ms.Value.Errors.Any())
                            .SelectMany(x => x.Value.Errors)
                            .Select(x => x.ErrorMessage)
                            .ToList();
                foreach (var error in errors)
                {
                    Console.WriteLine($"Validation Error: {error}");
                }
            }

            if (ModelState.IsValid)
            {
                // Check if a vehicle with the same RegistrationNumber already exists
                var existingVehicle = await _context.Vehicles
                    .FirstOrDefaultAsync(v => v.RegistrationNumber == vehicle.RegistrationNumber);

                if (existingVehicle != null)
                {
                    // Add an error to the ModelState if the registration number already exists
                    ModelState.AddModelError("RegistrationNumber", "This registration number already exists.");
                }

                if (ModelState.IsValid)
                {
                    _context.Add(vehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            // Repopulate dropdown in case of validation failure
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Name", vehicle.VehicleTypeId);
            return View(vehicle);
        }




        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Name", vehicle.VehicleTypeId);
            return View(vehicle);
        }



        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegistrationNumber,Color,Brand,Model,ParkingSpotSize,NumberOfWheels,VehicleTypeId")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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
            ViewData["VehicleTypeId"] = new SelectList(_context.VehicleTypes, "Id", "Name", vehicle.VehicleTypeId);
            return View(vehicle);
        }





        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicles
                .Include(v => v.VehicleType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }


        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }

    }
}
