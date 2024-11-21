using Garage_3._0.Data;
using Garage_3._0.Models.ViewModels;
using Garage_3._0.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

public class VehiclesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public VehiclesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // GET: Vehicles
    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var vehicles = await _context.Vehicles
            .Where(v => v.ApplicationUserVehicleClasses.Any(auv => auv.ApplicationUserId == userId))
            .Include(v => v.VehicleType)
            .Include(v => v.ApplicationUserVehicleClasses)
            .ToListAsync();

        return View(vehicles);
    }

    // GET: Vehicles/Create
    public IActionResult Create()
    {
        var viewModel = new VehicleCreateViewModel
        {
            Vehicle = new Vehicle(),
            VehicleTypes = Enum.GetValues(typeof(VehicleTypes))
                .Cast<VehicleTypes>()
                .Select(vt => new SelectListItem
                {
                    Value = vt.ToString(), // Use ToString() for enum string values
                    Text = vt.ToString() // Display text
                }).ToList()
        };

        return View(viewModel);
    }

    // POST: Vehicles/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,RegistrationNumber,Color,Brand,Model,ParkingSpotSize,NumberOfWheels,VehicleType")] Vehicle vehicle)
    {
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine(error.ErrorMessage);
        }
        if (ModelState.IsValid)
        {
            // Add vehicle to the database
            _context.Add(vehicle);
            await _context.SaveChangesAsync();

            // Associate with the user
            var userId = _userManager.GetUserId(User);
            var applicationUserVehicle = new ApplicationUserVehicleClass
            {
                ApplicationUserId = userId,
                VehicleId = vehicle.Id
            };

            _context.Add(applicationUserVehicle);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Repopulate the VehicleTypes list for the view
        var viewModel = new VehicleCreateViewModel
        {
            Vehicle = vehicle,
            VehicleTypes = Enum.GetValues(typeof(VehicleTypes))
                .Cast<VehicleTypes>()
                .Select(vt => new SelectListItem
                {
                    Value = vt.ToString(),
                    Text = vt.ToString()
                }).ToList()
        };

        return View(viewModel);
    }


    // GET: Vehicles/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vehicle = await _context.Vehicles
            .Include(v => v.ApplicationUserVehicleClasses)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (vehicle == null)
        {
            return NotFound();
        }

        return View(vehicle);
    }

    // POST: Vehicles/Edit/5
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
        return View(vehicle);
    }

    // GET: Vehicles/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var vehicle = await _context.Vehicles
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
        //var vehicle = await _context.Vehicles.FindAsync(id);
        //var userId = _userManager.GetUserId(User);

        //// Find the association with the user and remove it
        //var association = await _context.ApplicationUserVehicleClasses
        //    .FirstOrDefaultAsync(auv => auv.ApplicationUserId == userId && auv.VehicleId == vehicle.Id);

        //if (association != null)
        //{
        //    _context.ApplicationUserVehicleClasses.Remove(association);
        //}

        //_context.Vehicles.Remove(vehicle);
        //await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool VehicleExists(int id)
    {
        return _context.Vehicles.Any(e => e.Id == id);
    }
}
