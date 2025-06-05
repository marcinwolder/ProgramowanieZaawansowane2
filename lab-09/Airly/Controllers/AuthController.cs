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

        var inputBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = MD5.HashData(inputBytes);

        StringBuilder sb = new();
        foreach (var t in hashBytes)
        {
            sb.Append(t.ToString("x2"));
        }
        var hashPassword = sb.ToString();

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.PasswordHash == hashPassword);
        if (user == null) return View();
        HttpContext.Session.SetInt32("IsLoggedIn", 1);
        HttpContext.Session.SetInt32("IsAdmin", (email == "admin@airly.com")? 1 : 0);
        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Email", email);
        return RedirectToAction("success", "auth");
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