using Microsoft.AspNetCore.Mvc;
namespace lab_09.Controllers;

public class Auth : Controller
{
    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Login(IFormCollection form)
    {
        Console.WriteLine("Login: " + HttpContext.Session.Keys.Contains("LoggedIn"));
        if (String.IsNullOrEmpty(form["email"]) || String.IsNullOrEmpty(form["password"]))
        {
            return View();
        }
        string email = form["email"]!;
        string password = form["password"]!;

        // Sprawdzenie czy konto istnieje
        if (true)
        {
            HttpContext.Session.SetInt32("LoggedIn", 1);
            HttpContext.Session.SetString("Email", email);
            return RedirectToAction("success", "auth");
        }
        // return View();
    }

    public IActionResult Success()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }

    public string Index()
    {
        return "Index";
    }
}