using Microsoft.AspNetCore.Mvc;

namespace Airly.Controllers;

public class AdminPanelController : Controller
{
    // GET
    [AdminOnly]
    public IActionResult Index()
    {
        return View();
    }
}