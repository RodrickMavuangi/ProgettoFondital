using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

public class LogUserNameMiddleware
{
    private readonly RequestDelegate _next;

    public LogUserNameMiddleware(RequestDelegate next)
    {
        this._next = next;
    }

    public Task Invoke(HttpContext context)
    {
        LogContext.PushProperty("User", context.User.Identity.Name);

        return _next(context);
    }
}