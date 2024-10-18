using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using GestaoDeResiduos.Exceptions;
using GestaoDeResiduos.ViewModels.Responses;

namespace GestaoDeResiduos.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            BaseApiResponse<object> apiResponse;
            int statusCode;
            
            if (exception is NotFoundException)
            {
                apiResponse = new BaseApiResponse<object>(exception.Message, null);
                statusCode = 404;
            }
            else if (exception is ConflictException)
            {
                apiResponse = new BaseApiResponse<object>(exception.Message, null);
                statusCode = 409;
            }
            else
            {
                apiResponse = new BaseApiResponse<object>(exception.Message, null);
                statusCode = 500;
            }

            context.Result = new ObjectResult(apiResponse)
            {
                StatusCode = statusCode
            };

            context.ExceptionHandled = true;
        }
    }
}