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
        Console.WriteLine(form["email"] + ", " + form["password"]);
        // Sprawdzenie czy to w ogóle działa
        if (true)
        {
            return RedirectToAction("successfull", "auth"); //Redirect("auth/successfull");
        }
        // return View();
    }

    public IActionResult Successfull()
    {
        return View();
    }

    public string Index()
    {
        return "Index";
    }
}