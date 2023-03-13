using Fsoft.Web.Locdx7.Common.Error;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Crypto;
using System.Net;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;

namespace Fsoft.Web.Locdx7.API.Middleware
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (AccessViolationException avEx)
            {
                _logger.LogError($"A new violation exception has been thrown: {avEx}");
                await HandleExceptionAsync(httpContext, avEx);
            } 
            catch (ApplicationException aEx)
            {
                _logger.LogError($"A new application exception has been thrown: {aEx}");
                await HandleExceptionAsync(httpContext, aEx);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext,ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Request.ContentType = "application/json";
            var response = httpContext.Response;


            var errorDetail = new ErrorDetail();
            errorDetail.StatusCode = response.StatusCode;

            switch (exception)
            {
                case AccessViolationException ex:
                    errorDetail.Message = "Access violation error from the custom middleware";

                    break;
                case ApplicationException ex:
                    errorDetail.Message = "Access application error from the custom middleware";

                    break;
                case InvalidOperationException  ex:
                    errorDetail.Message = "Access invalid operation error from the custom middleware";

                    break;
                case ArgumentException ex:
                    errorDetail.Message = "Access argument error from the custom middleware";
                
                    break;
                case NullReferenceException ex:
                    errorDetail.Message = "Access null refer error from the custom middleware";

                    break;
                default:
                    errorDetail.StatusCode = (int) HttpStatusCode.InternalServerError;
                    errorDetail.Message = "Internet Server Error";
                    break;
            }

            _logger.LogError(exception.Message);
            await httpContext.Response.WriteAsync(errorDetail.ToString());



        }
    }
}
