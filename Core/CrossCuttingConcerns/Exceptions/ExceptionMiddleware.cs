using System;
using System.Text.Json;
using Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Microsoft.AspNetCore.Http;

namespace Core.CrossCuttingConcerns.Exceptions
{
    public class ExceptionMiddleware 
    {
        private readonly RequestDelegate _next; // Request Delegate => Middleware'in özelliğidir.
        //Request Delegate => Middleware'in bölme işleminden sonrasında çalışması için kullanılan classdır.

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex) 
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
              
                //Her class kendi hata yönetimini kendi içerisinde yapsın.
                //ValidationException, BusinessException
                if (ex is BusinessExeption)
                {
                    ProblemDetails problemDetails = new ProblemDetails();
                    problemDetails.Title = "Business Rule Violation";
                    problemDetails.Detail = ex.Message;
                    problemDetails.Type = "BusinessException";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                }
                
            }
        }

    }
}

//Middleware, genellikle Invoke metodu içerisinde tanımlanır ve bu metodun aldığı parametreler aracılığıyla isteği işler veya sonlandırır.