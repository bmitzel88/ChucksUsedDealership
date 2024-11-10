using ChucksUsedDealership.Data;
using ChucksUsedDealership.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult ContactFormList()
        {
            var contactForms = _context.ContactForms.ToList();
            return View(contactForms);
        }

        [Authorize(Roles = "Admin, Authorized")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
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

            return View(contactForm);
        }

        [Authorize(Roles = "Admin, Authorized")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ContactFormId)
        {
            var contactForm = await _context.ContactForms.FindAsync(ContactFormId);
            if (contactForm == null)
            {
                return NotFound();
            }
            _context.ContactForms.Remove(contactForm);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Contact Form deleted successfully!";
            return RedirectToAction(nameof(ContactFormList));
        }

        [Authorize(Roles = "Admin, Authorized")]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
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

            return View(contactForm);
        }

        [Authorize(Roles = "Admin, Authorized")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
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

            return View(contactForm);
        }

        [Authorize(Roles = "Admin, Authorized")]
        [HttpPost]
        public async Task<IActionResult> EditConfirmed(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                _context.ContactForms.Update(model);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Contact Form updated successfully!";
                return RedirectToAction(nameof(ContactFormList));
            }

            return View("Edit", model);
        }
    }
}
