using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mission08_Team0303.Models;

namespace Mission08_Team0303.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;  // ✅ Logger instance for logging application events

    // ✅ Constructor to inject the logger dependency
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // ✅ Action method to return the Index view (homepage)
    public IActionResult Index()
    {
        return View();
    }

    // ✅ Action method to return the Privacy view
    public IActionResult Privacy()
    {
        return View();
    }

    // ✅ Handles error responses and returns an error view with relevant request details
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}