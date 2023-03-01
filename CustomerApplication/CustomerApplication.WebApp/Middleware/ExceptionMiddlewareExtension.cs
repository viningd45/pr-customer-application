using CustomerApplication.Models.RequestModel;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace CustomerApplication.WebApp.Middleware;

public static class ExceptionMiddlewareExtension
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
    {
        app.UseExceptionHandler(handler =>
        {
            handler.Run(async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    logger.LogError(contextFeature.Error, "There was an unhandled exception");
                    logger.LogError("Exception text: {contextFeature.Error}", contextFeature.Error.Message);
                    JsonSerializerOptions options = new JsonSerializerOptions();
                    options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    await context.Response.WriteAsync(JsonSerializer.Serialize(new ResponseMessageRequest($"There was an error processing the request: {contextFeature.Error.Message}"), options));
                }
            });
        });
    }
}
