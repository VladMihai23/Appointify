using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AboutUs()
    {
        return View();
    }
    

    

    public IActionResult RegisterSuccessfully()
    {
        return View();
    }

    public IActionResult Services()
    {
        return View();
    }

    public IActionResult LoginSuccessfully()
    {
        return View();
    }

    

    public IActionResult BookingTuns()
    {
        return View();
    }

    public IActionResult BookingSuccessfully()
    {
        return View();
    }

    public IActionResult BookingMedical()
    {
        return View();
    }

    public IActionResult BookingMasaj()
    {
        return View();
    }

    public IActionResult BookingIT()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

        
    
  

    public IActionResult Reviews()
    {
        return View();
    }

    public IActionResult TermeniSiConditii()
    {
        return View();
    }

    public IActionResult PoliticaDeConfidentialitate()
    {
        return View();
    }

    public IActionResult BecomeAnExpert()
    {
        return View();
    }
    
    
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}