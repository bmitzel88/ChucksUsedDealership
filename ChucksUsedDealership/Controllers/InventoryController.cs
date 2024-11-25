using ChucksUsedDealership.Data;
using ChucksUsedDealership.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace ChucksUsedDealership.Controllers
{
    public class InventoryController : Controller
    {
        private readonly DealershipDbContext? _context;

        // Inject dealershipDB
        public InventoryController(DealershipDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET: InventoryController
        public async Task<IActionResult> Index(int page = 1, int pageSize = 12)
        {
            var totalItems = _context.CarInventories.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            //If the current page is greater then the amount of total pages, redirect user to last page
            if (page > totalPages)
            {
                page = totalPages;
            }

            var carInventory = await _context.CarInventories
                .OrderByDescending(c => c.CarId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var model = new PaginationViewModel<CarInventory>
            {
                Items = carInventory,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages
            };

            return View(model);
        }

        // GET: Displays details about a car for sale
        public async Task<IActionResult> Details(int id, int page = 1, int pageSize = 12)
        {

            var carToDisplay = await _context.CarInventories.FindAsync(id);


            if (carToDisplay == null)
            {
                return NotFound();
            }

            ViewData["CurrentPage"] = page;
            ViewData["PageSize"] = pageSize;

            return View(carToDisplay);
        }

        // GET: Asks to create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actually creates car
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CarInventory car)
        {
            if (ModelState.IsValid)
            {
                _context.CarInventories.Add(car);
                await _context.SaveChangesAsync();

                // Show success message on page
                ViewData["Message"] = $"{car.Make} {car.Model} was added successfully!";
                return RedirectToAction("Index");
            }

            return View(car);
        }

        // GET: Displays the edit confirmation view
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int id, int page = 1, int pageSize = 12)
        {
            CarInventory carToEdit = await _context.CarInventories.FindAsync(id);

            if (carToEdit == null)
            {
                return NotFound();
            }
            ViewData["CurrentPage"] = page;
            ViewData["PageSize"] = pageSize;

            return View(carToEdit);
        }

        // POST: Actually edits the car after confirmation
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditConfirmed(CarInventory carToBeEdited, int currentPage, int pageSize)
        {
            
            // Get the car ID from the form
            int carIdToEdit = carToBeEdited.CarId;

            if (carIdToEdit == null)
            {
                ModelState.AddModelError("", "Car information is missing or could not be found.");
                return View("Edit", carToBeEdited);
            }


            if (ModelState.IsValid)
            {
                _context.CarInventories.Update(carToBeEdited);
                await _context.SaveChangesAsync();
                //SuccessMessage tempdata is set in the shared layout for all pages to use when submitted
                TempData["SuccessMessage"] = $"{carToBeEdited.Make} {carToBeEdited.Model} was updated successfully";
                return RedirectToAction(nameof(Index), new { page = currentPage, pagesize = pageSize} );
            }

            return View("Edit", carToBeEdited);
        }

        // GET: Displays the delete confirmation view
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id, int page, int pageSize)
        {
            CarInventory carToDelete = await _context.CarInventories.FindAsync(id);

            if (carToDelete == null)
            {
                return NotFound();
            }

            ViewData["CurrentPage"] = page;
            ViewData["PageSize"] = pageSize;

            return View(carToDelete);
        }

        // POST: Actually deletes the car after confirmation
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int CarId, int currentPage, int pageSize)
        {
            var carToDelete = await _context.CarInventories.FindAsync(CarId);

            if (carToDelete != null)
            {
                _context.CarInventories.Remove(carToDelete);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"{carToDelete.Make} {carToDelete.Model} was deleted successfully";

                return RedirectToAction(nameof(Index), new { page = currentPage, pageSize = pageSize } );
            }

            TempData["SuccessMessage"] = "This product does not exist or has already been deleted";
            return RedirectToAction( nameof(Index), new { page = currentPage, pageSize = pageSize } );


        }

        // POST : Redirects a user from the details page to a ContactForm with
        //        the subject box filled out already and disabled

        [HttpPost]
        public IActionResult RedirectToContact(int carId, string carMake, string carModel)
        {
            // Pass the information needed to ContactForm controller
            return RedirectToAction("Inquiry", "ContactForm", new { carId, carMake, carModel });
        }
    }
}
