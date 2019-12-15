using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace BikeStore.Filters
{
    public class CustomExceptionFilterAttribute:ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exceptionMessage = actionExecutedContext.Exception.InnerException != null ? actionExecutedContext.Exception.InnerException.Message : actionExecutedContext.Exception.Message;
            var errorMessage = new HttpError(exceptionMessage) { { "IsSuccess", false } };

            actionExecutedContext.Response =
                actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.OK, errorMessage);
        }
    }
}