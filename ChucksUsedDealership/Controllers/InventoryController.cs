﻿using ChucksUsedDealership.Data;
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
            var carDetails = await _context.CarInventories.FindAsync(id);

            if (carDetails == null)
            {
                return NotFound();
            }


            return View(carDetails);
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
            CarInventory carToEdit = await _context.CarInventories.FindAsync(id);

            if (carToEdit == null)
            {
                return NotFound();
            }
            return View(carToEdit);
        }

        // POST: Actually edits the car after confirmation
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CarInventory car)
        {
            if (ModelState.IsValid)
            {
                _context.CarInventories.Update(car);
                await _context.SaveChangesAsync();

                TempData["Message"] = $"{car.Make} {car.Model} was updated successfully";
                return RedirectToAction("Index");
            }

            return View(car);
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

            return View(carToDelete);
        }

        // POST: Actually deletes the car after confirmation
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id, IFormCollection collection)
        {
            CarInventory carToDelete = await _context.CarInventories.FindAsync(id);

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
    }
}
