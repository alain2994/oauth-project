using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace exercice_2.IMWFramework.Extensions
{
    public static class HttpContextExtensions
    {
        public static void SetStatusCode(this HttpContext context, int code)
        {
            context.Response.StatusCode = code;
        }
    }
}
