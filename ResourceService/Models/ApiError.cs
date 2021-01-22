using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Models
{
    public class ApiError
    {
        public ApiError()
        {
        }
        public ApiError( string message )
        {
            Message = message;
        }

        public ApiError( string message, string detail )
        {
            Message = message;
            Detail = detail;
        }

        public ApiError( ModelStateDictionary modelState )
        {
            Message = "Invalid parameters";
            Detail = modelState
                .FirstOrDefault( x => x.Value.Errors.Any() ).Value.Errors
                .FirstOrDefault().ErrorMessage;
        }

        public string Message { get; set; }

        public string Detail { get; set; }
    }
}
