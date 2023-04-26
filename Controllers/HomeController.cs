using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProductsAndCategories.Models;

namespace ProductsAndCategories.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private MyContext _context;

    public HomeController(ILogger<HomeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    [HttpGet("/")]
    public IActionResult Index()
    {
        MyViewModel ViewModel = new MyViewModel 
        {
            AllProducts = _context.Products.ToList()
        };

        return View("Index", ViewModel);
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
