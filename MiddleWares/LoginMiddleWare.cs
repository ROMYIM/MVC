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
            var headers = context.Request.Headers;
            foreach (var header in headers)
            {
                _logger.LogDebug($"key:{header.Key},value:{header.Value}");
            }
            var session = context.Session;
            var ID = session.GetString("ID");
            if (string.IsNullOrEmpty(ID))
            {
                if (path == "/user/login" || path == "/")
                {
                    await _next.Invoke(context);
                    if (path == "/user/login")
                    {
                        ID = "4444";
                        session.SetString("ID", ID);
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
                session.GetString("ID");
                if (path != "/user/login" && path != "/")
                {
                    await _next.Invoke(context);
                }
            }
        }
    }
}