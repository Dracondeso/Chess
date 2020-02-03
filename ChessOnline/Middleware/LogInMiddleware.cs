using ChessOnline.Controllers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessOnline.Middleware
{
    public class LogInMiddleware : IMiddleware
    {
        private const string CookieKey = "AuthCookie";

        public Task InvokeAsync(HttpContext context, RequestDelegate next)// redirect to loginPage 

        {
            string url;
#if DEBUG
            url = @"http:\\localhost:5001\home\LogIn";
#else
            url = "www.urlinproduzione.it";
#endif
            if (context.Request.Path == "/home/WaitingPage")
            {
                while (HomeController.DataClient.User.Side == Models.Enum.Side.NotAssigned)
                {

                    context.Response.Redirect("WaitingPage");
                    return next.Invoke(context);
                }
                context.Response.Redirect("ChessBoard");
            }

            return next.Invoke(context);
        }

    }
}
