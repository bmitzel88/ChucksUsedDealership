using ChucksUsedDealership.Data;
using ChucksUsedDealership.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace ChucksUsedDealership.Controllers
{
    public class ContactFormController : Controller
    {
        private readonly DealershipDbContext _context;

        public ContactFormController(DealershipDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // GET : Used for inquiring about a car in the inventory
        [HttpGet]
        public IActionResult Inquiry(int carId, string carMake, string carModel)
        {
            // Build a string for the subject box
            var carSubject = $"Car ID: {carId}, Make: {carMake}, Model: {carModel}";

            ViewData["carSubject"] = carSubject;
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitContactForm(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                model.DateSubmitted = DateTime.Now; // Set the date submitted to the current date and time
                _context.ContactForms.Add(model);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Your message has been successfully submitted!";
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }

        [Authorize(Roles ="Admin, Authorized")]
        [HttpGet]
        public async Task<IActionResult> ContactFormList(int page = 1, int pageSize = 10)
        {
            var contactForms = await _context.ContactForms
                .OrderByDescending(c => c.DateSubmitted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalItems = _context.ContactForms.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var model = new PaginationViewModel<ContactForm>
            {
                Items = contactForms,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages
            };

            return View(model);
        }


        [Authorize(Roles = "Admin, Authorized")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id, int page = 1, int pageSize = 10)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contactForm = await _context.ContactForms.FindAsync(id);
            if (contactForm == null)
            {
                return NotFound();
            }

            ViewData["CurrentPage"] = page;
            ViewData["PageSize"] = pageSize;

            return View(contactForm);
        }

        [Authorize(Roles = "Admin, Authorized")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ContactFormId, int currentpage, int pageSize)
        {
            var contactForm = await _context.ContactForms.FindAsync(ContactFormId);
            if (contactForm == null)
            {
                return NotFound();
            }
            _context.ContactForms.Remove(contactForm);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Contact Form deleted successfully!";
            return RedirectToAction(nameof(ContactFormList), new { page = currentpage, pageSize = pageSize});
        }

        [Authorize(Roles = "Admin, Authorized")]
        [HttpGet]
        public async Task<IActionResult> Details(int id, int page = 1, int pageSize = 10)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contactForm = await _context.ContactForms.FindAsync(id);
            if (contactForm == null)
            {
                return NotFound();
            }

            ViewData["PageNumber"] = page;
            ViewData["PageSize"] = pageSize;

            return View(contactForm);
        }

        [Authorize(Roles = "Admin, Authorized")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id, int page = 1, int pageSize = 10)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contactForm = await _context.ContactForms.FindAsync(id);
            if (contactForm == null)
            {
                return NotFound();
            }

            ViewData["CurrentPage"] = page;
            ViewData["PageSize"] = pageSize;

            return View(contactForm);
        }

        [Authorize(Roles = "Admin, Authorized")]
        [HttpPost]
        public async Task<IActionResult> EditConfirmed(ContactForm model, int currentPage, int pageSize)
        {
            if (ModelState.IsValid)
            {
                _context.ContactForms.Update(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Contact Form updated successfully!";
                return RedirectToAction(nameof(ContactFormList), new { page = currentPage, pageSize = pageSize });
            }

            return View("Edit", model);
        }
    }
}
