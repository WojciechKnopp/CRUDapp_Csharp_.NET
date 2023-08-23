using CookiesApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace CookiesApp.Controllers;

public class ManufacturerController : Controller
{
    private readonly DataContext _context;
    
    public ManufacturerController(DataContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        return View(_context.Manufacturers.ToList());
    }
    
    public IActionResult Create()
    {
        ViewBag.Action = "Dodaj";
        return View("Form");
    }
    
    [HttpPost]
    public IActionResult Create(Manufacturer manufacturer)
    {
        Console.WriteLine("Dodaj producenta");
        if (ModelState.IsValid)
        {
            Console.WriteLine("Model jest valid");
            _context.Manufacturers.Add(manufacturer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            Console.WriteLine("Model NIE jest valid");
            ViewBag.Action = "Dodaj";
            return View("Form");
        }
    }
    
    public IActionResult Edit(int id)
    {
        var manufacturer = _context.Manufacturers.Find(id);
        if (manufacturer == null)
            return NotFound("Nie znaleziono danego producenta.");
        
        ViewBag.Action = "Edytuj";
        return View("Form", manufacturer);
    }
    
    [HttpPost]
    public IActionResult Edit(Manufacturer manufacturer)
    {
        if (ModelState.IsValid)
        {
            _context.Manufacturers.Update(manufacturer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            ViewBag.Action = "Edytuj";
            return View("Form", manufacturer);
        }
    }

    public IActionResult Delete(int id)
    {
        var manufacturer = _context.Manufacturers.Find(id);
        if (manufacturer == null)
            return NotFound("Nie znaleziono danego producenta.");
        
        _context.Manufacturers.Remove(manufacturer);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    
    public IActionResult Details(int id)
    {
        var manufacturer = (
                from c in _context.Manufacturers
                where c.Id == id
                select c)
                .FirstOrDefault();
        
        if (manufacturer == null)
            return NotFound("Nie znaleziono danego producenta.");
        
        ViewBag.Cookies = _context.Cookies.Where(c => c.ManufacturerId == id).ToList();
        return View(manufacturer);
    }
}