using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize] 
    public class SubmitRequestController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public SubmitRequestController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "User, Expert")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "User, Expert")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExpertRequest request)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            request.UserId = user.Id;
            _context.ExpertRequests.Add(request);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Request submitted successfully!";
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var requests = _context.ExpertRequests.ToList();
            return View(requests);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var request = _context.ExpertRequests.Find(id);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(ExpertRequest request)
        {
            _context.ExpertRequests.Update(request);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = _context.ExpertRequests.Find(id);
            if (request != null)
            {
                _context.ExpertRequests.Remove(request);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        
    }
}
