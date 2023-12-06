using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shop.Attributes;

public class CustomAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
	public void OnAuthorization(AuthorizationFilterContext context)
	{
		var user = context.HttpContext.User;

		if (user.Identity is null || !user.Identity.IsAuthenticated)
		{
			context.Result = new RedirectToActionResult("Index", "Login", null);
			return;
		}

		if (!string.IsNullOrEmpty(Roles))
		{
			var roles = Roles.Split(',').Select(r => r.Trim());

			if (!roles.Any(r => user.IsInRole(r)))
			{
				context.Result = new ForbidResult();
				return;
			}
		}
	}
}
