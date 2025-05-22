using Microsoft.AspNetCore.Mvc;
namespace Airly.Controllers;

public class TravelController : Controller
{
    [SessionAuthorize]
    public IActionResult Index()
    {
        return View();
    }
}