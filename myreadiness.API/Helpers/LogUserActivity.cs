using System;
using System.Security.Claims;
using System.Threading.Tasks;
using myreadiness.API.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace myreadiness.API.Helpers
{
    public class LogUserActivity
    {
         public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var userId = Guid.Parse(resultContext.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier).Value);
            var repo = resultContext.HttpContext.RequestServices.GetService<IRepository>();
            var repoUser = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
            var user = await repoUser.GetUser(userId);
            user.LastActive = DateTime.Now;
            await repo.SaveAll();
        }
    }
}