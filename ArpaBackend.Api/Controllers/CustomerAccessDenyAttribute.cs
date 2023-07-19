using ArpaBackend.Core.Model.Base;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using ArpaBackend.Core.Helpers;

namespace ArpaBackend.Api.Controllers
{
    public class CustomerAccessDenyAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Check(context);
        }
        public async Task Check(AuthorizationFilterContext context)
        {
            try
            {
                string Role = context.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
                if (!string.IsNullOrEmpty(Role) && Role == "Customer")
                {
                    await context.HttpContext.Response.WriteAsync(new BaseDTO(401, "دسترسی غیر مجاز", null).ToSerilizedString());
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
