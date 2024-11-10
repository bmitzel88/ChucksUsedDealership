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

        // GET: InventoryController
        public async Task<IActionResult> Index(int? id)
        {

            const int NumCarsToDisplayPerPage = 3;
            const int PageOffset = 1; // Need a page offset to use current page and figure out number of products to skip


            // Check for id value
            //                            if true   if false
            int currPage = id.HasValue ? id.Value : 1;

            int totalNumOfCars = await _context.CarInventories.CountAsync();
            double maxNumPages = Math.Ceiling((double)totalNumOfCars / NumCarsToDisplayPerPage);
            int lastPage = Convert.ToInt32(maxNumPages); // Rounds pages up to the next whole page number

            // Get all cars from the database
            List<CarInventory> cars = await (from car in _context.CarInventories
                                             select car)
                                             .Skip(NumCarsToDisplayPerPage * (currPage - PageOffset))
                                             .Take(NumCarsToDisplayPerPage)
                                             .ToListAsync();

            InventoryViewModel inventoryModel = new(cars, lastPage, currPage);

            return View(inventoryModel);
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
        public async Task<ActionResult> Delete(int id)
        {
            CarInventory carToDelete = await _context.CarInventories.FindAsync(id);

            if (carToDelete == null)
            {
                return NotFound();
            }

            var carModel = new InventoryViewModel(new List<CarInventory>{ carToDelete }, 1, 1);

            return View(carModel);
        }

        // POST: Actually deletes the car after confirmation
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var carToDelete = await _context.CarInventories.FindAsync(id);

            if (carToDelete != null)
            {
                _context.CarInventories.Remove(carToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"{carToDelete.Make} {carToDelete.Model} was deleted successfully";

                return RedirectToAction("Index");
            }

            TempData["Message"] = "This product was already deleted";
            return RedirectToAction("Index");


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
