using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Airly.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace lab_09.Controllers;

public class AuthController : Controller
{
    private readonly AirlyContext _context;

    public AuthController(AirlyContext context)
    {
        _context = context;
    }
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(IFormCollection form)
    {
        if (String.IsNullOrEmpty(form["email"]) || String.IsNullOrEmpty(form["password"]))
        {
            return View();
        }
        string email = form["email"]!;
        string password = form["password"]!;

        byte[] inputBytes = Encoding.UTF8.GetBytes(password);
        byte[] hashBytes = MD5.HashData(inputBytes);

        StringBuilder sb = new();
        for (int i = 0; i < hashBytes.Length; i++)
        {
            sb.Append(hashBytes[i].ToString("x2"));
        }
        password = sb.ToString();
        Console.WriteLine(password);

        var destination = await _context.User.FirstOrDefaultAsync(m => m.Email == email && m.Password == password);
        if (destination != null)
        {
            HttpContext.Session.SetInt32("LoggedIn", 1);
            HttpContext.Session.SetString("Email", email);
            return RedirectToAction("success", "auth");
        }
        return View();
    }

    [SessionAuthorize]
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    [SessionAuthorize]
    public IActionResult Success()
    {
        return View();
    }

    public IActionResult Error()
    {
        return View();
    }
}