﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using ResourceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {

        private readonly IWebHostEnvironment _env;

        public JsonExceptionFilter( IWebHostEnvironment env )
        {
            _env = env;
        }

        public void OnException( ExceptionContext context )
        {
            var error = new ApiError();

            if ( _env.IsDevelopment() )
            {
                error.Message = context.Exception.Message;
                error.Detail = context.Exception.StackTrace;
            }
            else
            {
                error.Message = "A server error occurred";
                error.Detail = context.Exception.Message;
            }

            context.Result = new ObjectResult( error )
            {
                StatusCode = 500
            };
        }
    }
}
