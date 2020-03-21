using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GatewayAPI.Middlewares
{
    //Builder class
    public static class AuthenticationMiddlewareExtension
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder applicationBuilder, AppSettingsModel configuration)
        {
            return applicationBuilder.UseMiddleware<AuthenticationMiddleware>(configuration);
        }
    }

    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _UserKey = null;
        private readonly static string KEY = "KEY";

        public AuthenticationMiddleware(RequestDelegate next , AppSettingsModel configuration)
        {
            _next = next;
            var appSettings = configuration;
            _UserKey = appSettings.APIKEY;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                string APIKEY = null;

                if (httpContext.Request.Headers[KEY].Any())
                {
                    if (httpContext.Request.Headers.TryGetValue(KEY, out StringValues Key))
                    {
                        APIKEY = Key.FirstOrDefault();

                        await ValidateKey(httpContext, _next, string.Equals(APIKEY, _UserKey));
                    }
                    else
                        await UnauthorizedAccess(httpContext);
                }
                else
                    await UnauthorizedAccess(httpContext);

            }
            catch (Exception)
            {
                await UnauthorizedAccess(httpContext);
            }
        }

        public static async Task UnauthorizedAccess(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";

            string response = JsonConvert.SerializeObject(new ResponseModel<TransactionResponseDetail>
            {
                response = new ResponseDataModel<TransactionResponseDetail> { success = false, message = "Acesso não autorizado!" }
            });

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync(response);
        }

        private static async Task ValidateKey(HttpContext context, RequestDelegate _next, bool Validate)
        {
            if (Validate)
                await _next.Invoke(context);
            else
                await UnauthorizedAccess(context);
        }
    }
}


