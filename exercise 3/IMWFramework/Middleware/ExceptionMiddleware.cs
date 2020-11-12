using exercice_2.IMWFramework.Managers;
using exercice_2.IMWFramework.ResponseModels;

using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace exercice_2.IMWFramework.Middleware.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }

            catch(ApiException ex)
            {
                await HandleExceptionAsync(httpContext, ex, ex.message, ex.StatusCode);
            }

            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, string message = "An error has occurred", int code = (int)ApiError.InternalServerError)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
           
            return context.Response.WriteAsync(new ResponseObject()
            {
                Error = new ResponseError()
                {
                    Code = code,
                    Message = message
                }
            }.ToString()); ;
        }
    }

    public class ApiException : Exception
    {
        public int StatusCode;
        public string message;
        public ApiException(ApiError error, string message)
        {
            this.StatusCode = (int)error;
            this.message = message;
        }
    }

   
}

    
       
