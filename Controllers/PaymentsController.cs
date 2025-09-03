using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly AppDbContext _context;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult MakePayment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessPayment(string name, decimal amount, string cardNumber, string cardHolderName, string expiryDate, string cvv)
        {
            if (string.IsNullOrEmpty(name) || amount <= 0 || string.IsNullOrEmpty(cardNumber) || string.IsNullOrEmpty(cardHolderName) || string.IsNullOrEmpty(expiryDate) || string.IsNullOrEmpty(cvv))
            {
                TempData["ErrorMessage"] = "Toate câmpurile trebuie completate corect.";
                return RedirectToAction("MakePayment");
            }

            var payment = new Payment
            {
                Name = name,
                Amount = amount,
                CardNumber = cardNumber,
                CardHolderName = cardHolderName,
                ExpiryDate = expiryDate,
                CVV = cvv
            };

            _context.Payments.Add(payment);
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Plata a fost înregistrată cu succes!";
            return RedirectToAction("PaymentHistory");
        }

        [HttpGet]
        public IActionResult PaymentHistory()
        {
            var payments = _context.Payments.OrderByDescending(p => p.CreatedAt).ToList();
            return View(payments);
        }
    }
}