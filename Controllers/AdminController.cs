using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public AdminController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            
            var requests = _context.ExpertRequests.Where(r => !r.IsApproved).ToList();
            var messages = _context.Contacts.ToList();
            
            var viewModel = new AdminDashBoardViewModel
            {
                    ExpertRequests = requests,
                    Contacts = messages
            };
            return View(viewModel);
        }

        public async Task<IActionResult> ApproveRequest(int id)
        {
            var request = _context.ExpertRequests.Find(id);

            if (request != null)
            {
                var user = await _userManager.FindByIdAsync(request.UserId);
                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, "Expert");
                    request.IsApproved = true;
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult DenyRequest(int id)
        {
            var request = _context.ExpertRequests.Find(id);

            if (request != null)
            {
                _context.ExpertRequests.Remove(request);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteContact(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null) 
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            TempData["Message"] = "Your contact has been deleted.";
            return RedirectToAction("Index");
        }
    }
}