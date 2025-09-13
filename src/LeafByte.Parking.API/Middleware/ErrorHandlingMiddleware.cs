using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LeafByte.Parking.API.Middleware;

public static class ErrorHandlingMiddleware
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        FluentValidation.ValidationException => (int)HttpStatusCode.BadRequest,
                        _ => (int)HttpStatusCode.InternalServerError
                    };

                    await context.Response.WriteAsync(new ProblemDetails
                    {
                        Status = context.Response.StatusCode,
                        Title = contextFeature.Error.GetType().Name,
                        Detail = contextFeature.Error.Message
                    }.ToString());
                }
            });
        });
    }
}
