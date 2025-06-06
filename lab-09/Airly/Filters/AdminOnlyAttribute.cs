using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AdminOnlyAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var isAdmin = (context.HttpContext.Session.GetString("Email") ?? "") == "admin@airly.com";

        if (!isAdmin)
        {
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}