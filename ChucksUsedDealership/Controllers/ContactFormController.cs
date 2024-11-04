using ChucksUsedDealership.Data;
using ChucksUsedDealership.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                .OrderBy(c => c.DateSubmitted)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalItems = _context.ContactForms.Count();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            var model = new ContactFormListViewModel
            {
                ContactForms = contactForms,
                PageNumber = page,
                PageSize = pageSize,
                TotalPages = totalPages
            };

            return View(model);
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
