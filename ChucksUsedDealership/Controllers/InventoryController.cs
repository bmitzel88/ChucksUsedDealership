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
        public async Task<IActionResult> Details(int id)
        {

            var carToDisplay = await _context.CarInventories.FindAsync(id);


            if (carToDisplay == null)
            {
                return NotFound();
            }

            var car = new InventoryViewModel(new List<CarInventory> { carToDisplay }, 1, 1); 


            return View(car);
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            CarInventory carToEdit = _context.CarInventories.Find(id);

            if (carToEdit == null)
            {
                return NotFound();
            }

            var car = new InventoryViewModel(new List<CarInventory> { carToEdit }, 1, 1);
            
                return View(car);
        }

        // POST: Actually edits the car after confirmation
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(InventoryViewModel inventoryModel)
        {
            
            var car = inventoryModel.Cars.FirstOrDefault();

            // Get the car ID from the form
            int carId = inventoryModel.Cars.FirstOrDefault()?.CarId ?? 0;

            var carToEdit = inventoryModel.Cars.FirstOrDefault(c => c.CarId == carId);

            if (carToEdit == null)
            {
                ModelState.AddModelError("", "Car information is missing or could not be found.");
                return View(inventoryModel);
            }


            if (ModelState.IsValid)
            {
                _context.CarInventories.Update(carToEdit);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{car.Make} {car.Model} was updated successfully";
                return RedirectToAction("Index");
            }

            return View(inventoryModel);
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
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, int currentPage, int currentPageSize)
        {
            var carToDelete = await _context.CarInventories.FindAsync(id);

            if (carToDelete != null)
            {
                _context.CarInventories.Remove(carToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"{carToDelete.Make} {carToDelete.Model} was deleted successfully";

                return RedirectToAction(nameof(Index), new { page = currentPage, pageSize = currentPageSize } );
            }

            TempData["Message"] = "This product does not exist or has already been deleted";
            return RedirectToAction( nameof(Index), new { page = currentPage, pageSize = currentPageSize } );


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
