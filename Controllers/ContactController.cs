using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ContactController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(Contact contact)
        {
            var user = await _userManager.GetUserAsync(User);
            contact.UserId = user?.Id;

            if (ModelState.IsValid)
            { 
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Contact submitted successfully!";
                return RedirectToAction("Index", "Home");
            }
            return View(contact);
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = User.IsInRole("Admin");

            var messages = _context.Contacts
                .Where(c => c.UserId == user.Id || isAdmin)
                .ToList();

            return View(messages);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = User.IsInRole("Admin");

            var contact = _context.Contacts.FirstOrDefault(c => c.Id == id && (c.UserId == user.Id || isAdmin));

            if (contact == null) return NotFound();

            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            var user = await _userManager.GetUserAsync(User);
            var isAdmin = User.IsInRole("Admin");

            var originalContact = _context.Contacts.FirstOrDefault(c => c.Id == contact.Id);

            if (originalContact == null || (originalContact.UserId != user.Id && !isAdmin))
                return Unauthorized();

            originalContact.Name = contact.Name;
            originalContact.Email = contact.Email;
            originalContact.Message = contact.Message;

            _context.Contacts.Update(originalContact);
            await _context.SaveChangesAsync();

            return RedirectToAction("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Id == id);

            if (contact == null)
            {
                return NotFound();
                
            }
            
            var UserId = _userManager.GetUserId(User);
            if (User.IsInRole("Admin") || contact.UserId == UserId)
            {

                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Contact deleted successfully!";
                return RedirectToAction("List");
            }

            return Forbid();
        }
    }
}
