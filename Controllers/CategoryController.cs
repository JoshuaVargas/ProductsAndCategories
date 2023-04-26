using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Models;

namespace ProductsAndCategories.Controllers;

public class CategoryController : Controller
{
    private readonly ILogger<CategoryController> _logger;
    private MyContext _context;

    public CategoryController(ILogger<CategoryController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("/categories/create")]
    public IActionResult CategoryCreate(Category newCategory)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newCategory);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        MyViewModel ViewModel = new MyViewModel
        {
            AllCategories = _context.Categories.ToList()
        };
        return View("Categories", ViewModel);
    }

    [HttpPost("/categories/product-association/create")]
    public IActionResult AssociationCreate(Association newAssociation)
    {
        MyViewModel ViewModel = new MyViewModel
        {
            Category = _context.Categories.Include( c => c.Products).ThenInclude(a => a.Product).Where(c => c.CategoryId == newAssociation.CategoryId).First(),
            AllProducts = _context.Products.ToList()
        };
        ViewBag.AllProducts = _context.Products.ToList();
        ViewBag.CategoryId = newAssociation.CategoryId;
        if (ModelState.IsValid){
            if (_context.Associations.Any(a => a.CategoryId == newAssociation.CategoryId && a.ProductId == newAssociation.ProductId))
            {
                ModelState.AddModelError("ProductId", "Product already in Category");
                return View("DisplayCategory", ViewModel);
            }
            _context.Associations.Add(newAssociation);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");            
        }
        return View("DisplayCategory", ViewModel);
    }

    [HttpGet("/categories")]
    public IActionResult Index()
    {
        MyViewModel ViewModel = new MyViewModel
        {
            AllCategories = _context.Categories.ToList()
        };
        
        return View("Categories", ViewModel);
    }
    
    [HttpGet("/categories/{id}")]
    public IActionResult ViewCategory(int id)
    {
        MyViewModel ViewModel = new MyViewModel
        {
            Category = _context.Categories.Include( c => c.Products).ThenInclude(a => a.Product).Where(c => c.CategoryId == id).First(),
            AllProducts = _context.Products.ToList()
        };
        ViewBag.AllProducts = _context.Products.ToList();
        ViewBag.CategoryId = id;
        return View("DisplayCategory", ViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}