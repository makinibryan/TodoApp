﻿using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using TodoApp.Models;

namespace TodoApp.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public async ValueTask<bool>TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            _logger.LogError($"An error occurred while processing your request: {exception.Message}");
            var errorResponse = new ErrorResponse
            {
                Message = exception.Message
            };
            switch (exception)
            {
                case BadHttpRequestException:
                    errorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Title = exception.GetType().Name;

                    break;
                default:
                    errorResponse.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Title = "Internal Server Error";
                    break;
            }
            httpContext.Response.StatusCode = errorResponse.StatusCode;
            await httpContext
                .Response
                .WriteAsJsonAsync (errorResponse, cancellationToken);
            return true;
        }
    }
}
