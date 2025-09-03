using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly UserManager<AppUser> _userManager;

        public ReviewController(IReviewService reviewService, UserManager<AppUser> userManager)
        {
            _reviewService = reviewService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return View(reviews);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review review)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            if (!ModelState.IsValid)
                return View(review);

            await _reviewService.SubmitReviewAsync(review, user.Id);
            TempData["SuccessMessage"] = "Recenzia a fost trimisă!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            var user = await _userManager.GetUserAsync(User);

            if (review == null || review.UserId != user?.Id)
                return Forbid();

            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Review review)
        {
            var user = await _userManager.GetUserAsync(User);

            if (review.UserId != user?.Id)
                return Forbid();

            if (!ModelState.IsValid)
                return View(review);

            await _reviewService.UpdateAsync(review);
            TempData["SuccessMessage"] = "Recenzia a fost actualizată!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            var user = await _userManager.GetUserAsync(User);

            if (review == null || review.UserId != user?.Id)
                return Forbid();

            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            var user = await _userManager.GetUserAsync(User);

            if (review == null || review.UserId != user?.Id)
                return Forbid();

            await _reviewService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Recenzia a fost ștearsă!";
            return RedirectToAction("Index");
        }
    }
}
