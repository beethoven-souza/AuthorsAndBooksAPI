using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class HandleForbiddenMiddleware
{
    private readonly RequestDelegate _next;

    public HandleForbiddenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
        {
            context.Response.ContentType = "application/json";

            var response = new
            {
                message = "Você não tem permissão para realizar esta ação."
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}
