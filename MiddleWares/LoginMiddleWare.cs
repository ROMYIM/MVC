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
            // await context.Session.LoadAsync();
            var path = context.Request.Path.Value.Trim();
            // var headers = context.Request.Headers;
            // foreach (var header in headers)
            // {
            //     _logger.LogDebug($"key:{header.Key},value:{header.Value}");
            // }
            var session = context.Session;
            var id = session.GetString("ID");
            var status = session.GetString("Status");
            if (string.IsNullOrEmpty(id))
            {
                if (path == "/user/login" || path == "/" || path == "/equipment/opendoor")
                {
                    await _next.Invoke(context);
                    if (path == "/user/login")
                    {
                        id = context.Request.Form["ID"];
                        status = context.Request.Form["Status"];
                        session.SetString("ID", id);
                        session.SetString("Status", status);
                        // await session.CommitAsync();
                    }
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
                    if (!string.IsNullOrEmpty(status) && status == "Administrator")
                    {
                        context.Response.Redirect("/user");
                    }
                }
                await _next.Invoke(context);
            }
        }
    }
}