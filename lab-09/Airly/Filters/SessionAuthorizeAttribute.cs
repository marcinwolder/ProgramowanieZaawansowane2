using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class SessionAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var isLoggedIn = (context.HttpContext.Session.GetInt32("IsLoggedIn") ?? 0) == 1;

        if (!isLoggedIn)
        {
            context.Result = new RedirectToActionResult("Login", "Auth", null);
        }
    }
}