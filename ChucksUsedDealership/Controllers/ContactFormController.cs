using ChucksUsedDealership.Data;
using ChucksUsedDealership.Models;
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

        [HttpPost]
        public async Task<IActionResult> SubmitContactForm(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                _context.ContactForms.Add(model);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Your message has been successfully submitted!";
                return RedirectToAction("Index");
            }

            return View("ContactFormView", model);
        }

        public IActionResult ContactFormList()
        {
            var contactForms = _context.ContactForms.ToList();
            return View(contactForms);
        }

        [HttpGet]
        public async Task<IActionResult> ContactFormDelete(int id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactFormDeleteConfirmed(int id)
        {
            var contactForm = await _context.ContactForms.FindAsync(id);
            _context.ContactForms.Remove(contactForm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ContactFormList));
        }
    }
}
