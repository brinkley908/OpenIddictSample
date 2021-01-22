using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService
{
    public class AuthTokensAttribute : Attribute, IAuthorizationFilter
    {

        private readonly string _roles;

        public AuthTokensAttribute( string Roles = null )
        {
            _roles = Roles;
        }

        public void OnAuthorization( AuthorizationFilterContext context )
        {

            var user = context.HttpContext.User;

            if ( !user.Identity.IsAuthenticated )
            {
                // it isn't needed to set unauthorized result 
                // as the base class already requires the user to be authenticated
                // this also makes redirect to a login page work properly
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}
