using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _service;
        private readonly UserManager<AppUser> _userManager;

        public AppointmentController(IAppointmentService service, UserManager<AppUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult BookingTuns()
        {
            ViewBag.ServiceName = "Tuns";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string serviceName, string date, string time)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            if (string.IsNullOrWhiteSpace(date) || string.IsNullOrWhiteSpace(time))
            {
                TempData["ErrorMessage"] = "Completează data și ora.";
                return RedirectToAction("BookingTuns");
            }

            try
            {
                DateTime localDateTime = DateTime.ParseExact($"{date} {time}", "yyyy-MM-dd HH:mm", null);
                DateTime utcDateTime = DateTime.SpecifyKind(localDateTime, DateTimeKind.Utc);

                var appointment = new Appointment
                {
                    UserId = user.Id,
                    ServiceName = serviceName,
                    DateTime = utcDateTime,
                    Status = "Pending"
                };

                await _service.CreateAppointmentAsync(appointment);

                TempData["SuccessMessage"] = $"Programare pentru {serviceName} realizată!";
                return RedirectToAction("MyAppointments");
            }
            catch
            {
                TempData["ErrorMessage"] = "Data/ora invalidă.";
                return RedirectToAction("BookingTuns");
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyAppointments()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var appointments = await _service.GetAppointmentsByUserAsync(user.Id);
            return View(appointments);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _service.GetByIdAsync(id);
            var user = await _userManager.GetUserAsync(User);

            if (appointment == null || appointment.UserId != user?.Id)
                return Forbid();

            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Appointment updated)
        {
            var user = await _userManager.GetUserAsync(User);
            if (updated.UserId != user?.Id)
                return Forbid();

            if (!ModelState.IsValid)
                return View(updated);

            updated.DateTime = DateTime.SpecifyKind(updated.DateTime, DateTimeKind.Local).ToUniversalTime();

            await _service.UpdateAppointmentAsync(updated);
            TempData["SuccessMessage"] = "Programarea a fost modificată.";
            return RedirectToAction("MyAppointments");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _service.GetByIdAsync(id);
            var user = await _userManager.GetUserAsync(User);

            if (appointment == null || appointment.UserId != user?.Id)
                return Forbid();

            return View(appointment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _service.GetByIdAsync(id);
            var user = await _userManager.GetUserAsync(User);

            if (appointment == null || appointment.UserId != user?.Id)
                return Forbid();

            await _service.DeleteAppointmentAsync(appointment);
            TempData["SuccessMessage"] = "Programarea a fost ștearsă!";
            return RedirectToAction("MyAppointments");
        }
    }
}
