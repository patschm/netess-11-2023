using System.Diagnostics;
using InfraStructure;
using Microsoft.AspNetCore.Mvc;
using Webbie.Models;

namespace Webbie.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICounter _counter;

    public HomeController(ILogger<HomeController> logger, ICounter counter)
    {
        _logger = logger;
        _counter = counter;
    }

    public IActionResult Index()
    {
        _counter.Increment();
        _counter.Show();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
