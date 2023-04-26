using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsAndCategories.Models;

namespace ProductsAndCategories.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private MyContext _context;

    public ProductController(ILogger<ProductController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    //CREATE
    [HttpPost("/products/create")]
    public IActionResult ProductCreate(Product newProduct)
    {
        if (ModelState.IsValid)
        {
            _context.Add(newProduct);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        MyViewModel ViewModel = new MyViewModel
        {
            AllProducts = _context.Products.ToList()
        };
        return View("Products", ViewModel);
    }
    [HttpPost("/products/category-association/create")]
    public IActionResult AssociationCreate(Association newAssociation)
    {
        MyViewModel ViewModel = new MyViewModel
        {
            Product = _context.Products.Include( p => p.Categories).ThenInclude( a => a.Category).Where(c => c.ProductId == newAssociation.ProductId).First(),
            AllCategories = _context.Categories.ToList()
        };
        if (ModelState.IsValid){
            if (_context.Associations.Any(a => a.CategoryId == newAssociation.CategoryId && a.ProductId == newAssociation.ProductId))
            {
                ModelState.AddModelError("CategoryId", "Product already in Category");
                ViewBag.AllCategories = _context.Categories.ToList();
                ViewBag.ProductId = newAssociation.ProductId;
                return View("DisplayProduct", ViewModel);
            }
            _context.Associations.Add(newAssociation);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");            
        }
        ViewBag.AllCategories = _context.Categories.ToList();
        ViewBag.ProductId = newAssociation.ProductId;
        return View("DisplayProduct", ViewModel);
    }

    //READ
    [HttpGet("/products/{id}")]
    public IActionResult ViewProduct(int id)
    {
        MyViewModel ViewModel = new MyViewModel
        {
            Product = _context.Products.Include( p => p.Categories).ThenInclude( a => a.Category).Where(c => c.ProductId == id).First(),
            AllCategories = _context.Categories.ToList()
        };
        ViewBag.AllCategories = _context.Categories.ToList();
        ViewBag.ProductId = id;
        return View("DisplayProduct", ViewModel);
    }

    //DESTROY

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}