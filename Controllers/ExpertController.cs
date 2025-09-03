using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[Authorize(Roles = "Expert")]
public class ExpertController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}