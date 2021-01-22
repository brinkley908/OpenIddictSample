using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Filters
{
    public class RequireHttps : RequireHttpsAttribute
    {

        protected override void HandleNonHttpsRequest( AuthorizationFilterContext context )
        {
            context.Result = new StatusCodeResult( 400 );
        }

    }
}
