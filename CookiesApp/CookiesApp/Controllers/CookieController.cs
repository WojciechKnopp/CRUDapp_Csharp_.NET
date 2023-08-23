using CookiesApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CookiesApp.Controllers;

public class CookieController : Controller
{
    private readonly DataContext _context;
    
    public CookieController(DataContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        var cookies = _context.Cookies
            .Include(m => m.Manufacturer)
            .ToList();

        return View(cookies);
    }
    
    public IActionResult Create()
    {
        ViewBag.Action = "Dodaj";
        ViewBag.ManufacturersId = new SelectList(_context.Manufacturers, "Id", "Name", null);
        return View("Form");
    }
    
    [HttpPost]
    public IActionResult Create([Bind("Name,Allergens,ImageUrl,ManufacturerId")] Cookie cookie)
    {
        if (ModelState.IsValid)
        {
            _context.Cookies.Add(cookie);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            ViewBag.Action = "Dodaj";
            ViewBag.ManufacturersId = new SelectList(_context.Manufacturers, "Id", "Name", cookie.Manufacturer);
            return View("Form");
        }
    }
    
    public IActionResult Edit(int id)
    {
        var cookie = (
            from c in _context.Cookies 
            where c.Id == id 
            select c)
            .FirstOrDefault();
        
        if (cookie == null)
            return NotFound("Nie znaleziono danych ciastek.");
        
        ViewBag.ManufacturersId = new SelectList(_context.Manufacturers, "Id", "Name", cookie.ManufacturerId);
        ViewBag.Action = "Edytuj";
        return View("Form", cookie);
    }
    
    [HttpPost]
    public IActionResult Edit(Cookie cookie)
    {
        if (ModelState.IsValid)
        {
            _context.Cookies.Update(cookie);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            ViewBag.ManufacturersId = new SelectList(_context.Manufacturers, "Id", "Name", cookie.ManufacturerId);
            ViewBag.Action = "Edytuj";
            return View("Form");
        }
    }
    
    [HttpPost]
    public IActionResult Delete(int id)
    {
        var cookie = (
                from c in _context.Cookies 
                where c.Id == id 
                select c)
            .FirstOrDefault();
        
        if (cookie == null)
            return NotFound("Nie znaleziono danych ciastek.");
        
        _context.Cookies.Remove(cookie);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult Details(int id)
    {
        var foundCookie = (
                from c in _context.Cookies 
                where c.Id == id 
                select c)
                .FirstOrDefault();
        
        if (foundCookie == null)
            return NotFound("Nie znaleziono danych ciastek.");
        var man = (
                from m in _context.Manufacturers 
                where m.Id == foundCookie.ManufacturerId 
                select m)
                .FirstOrDefault();
        return View(foundCookie);
    }

    public IActionResult CookieAllergens()
    {
        ViewBag.Manufacturers = _context.Manufacturers.ToList();
        return View(_context.Cookies.ToList());
    }

    public ActionResult AllergenList(int id)
    {
        var Allergens = 
            from c in _context.Cookies
            where c.Id == id
            select c.Allergens;
        
        return PartialView("AllergenList", Allergens.FirstOrDefault());
    }
}