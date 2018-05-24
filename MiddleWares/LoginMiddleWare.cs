using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MVC.Extensions;
using MVC.Models;

namespace MVC.MiddleWares
{
    public class LoginMiddleWare
    {

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoginMiddleWare(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            this._next = next;
            this._logger = loggerFactory.CreateLogger<LoginMiddleWare>();
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value.Trim();
            var user = context.Session.Get<User>("user");
            if (user == null)
            {
                if (path == "/user/login" || path == "/" || path == "/equipment/opendoor")
                {
                    await _next.Invoke(context);
                }
                else
                {
                    context.Response.Redirect("/");
                }    
            }
            else
            {
                if (path == "/user/login" || path == "/")
                {
                    if (user.Status == Status.Administrator)
                    {
                        context.Response.Redirect("/user");
                    }
                }
                await _next.Invoke(context);
            }
        }
    }
}