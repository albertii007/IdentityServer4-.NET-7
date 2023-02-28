using ASP.IdentityServer.Core.Application.Exceptions;
using ASP.IdentityServer.Core.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ASP.IdentityServer.Core.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BadRequestException) ExceptionResponse(context, HttpStatusCode.BadRequest);

            else if (context.Exception is UnauthorizedException) ExceptionResponse(context, HttpStatusCode.Unauthorized);

            else if (context.Exception is OnLoginFailureException) ExceptionResponse(context, HttpStatusCode.BadRequest);

            else ExceptionResponse(context, HttpStatusCode.InternalServerError);
        }

        private void ExceptionResponse(ExceptionContext context, HttpStatusCode statusCode)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.Result = new JsonResult(new { context.Exception.Message });
            context.HttpContext.Response.StatusCode = (int)statusCode;
        }

    }
}
