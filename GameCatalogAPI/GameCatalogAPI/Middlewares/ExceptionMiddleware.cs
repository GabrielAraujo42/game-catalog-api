using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace GameCatalogAPI.Middlewares
{
    public class ExceptionMiddleware
    {
        readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch
            {
                await HandleExceptionAsync(context);
            }
        }

        static async Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsJsonAsync(new { Message = "There was a request error, please try again later." });
        }
    }
}
