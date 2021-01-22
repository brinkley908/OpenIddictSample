using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Filters
{
    [System.AttributeUsage( System.AttributeTargets.Class | System.AttributeTargets.Method, AllowMultiple = true )]
    public class AuthorizeUserOrKeys : Attribute, IAuthorizationFilter
    {
        private const string ApiKeyHeaderName = "ApiKey";


        public string Role { get; set; }

        public void OnAuthorization( AuthorizationFilterContext context )
        {

            if ( !CheckUser( context ) && !CheckApiKey( context ) )
            {
                context.Result = new UnauthorizedResult();
                return;
            }

        }

        private bool CheckUser( AuthorizationFilterContext context )
        {
            var user = context.HttpContext.User;
            if ( !user.Identity.IsAuthenticated )
                return false;

            var allowedRoles = GetAllowedRoles().ToList();

            return !allowedRoles.Any() || allowedRoles.Any( x => user.IsInRole( x ) );

        }

        private bool CheckApiKey( AuthorizationFilterContext context )
        {
            if ( !context.HttpContext.Request.Headers.TryGetValue( ApiKeyHeaderName, out var potentialKey ) )
                return false;

            var apiKey = ApiKeys.GetKey( potentialKey );

            if ( apiKey == null || !apiKey.Value.Equals( potentialKey ) )
                return false;

            var allowedRoles = GetAllowedRoles().ToList();

            return !allowedRoles.Any() || allowedRoles.Any( x => IsInRole( x ) );


            bool IsInRole( string roleName ) => apiKey.Roles.Any( x => x.Equals( roleName, StringComparison.OrdinalIgnoreCase ) );

        }

        private IEnumerable<string> GetAllowedRoles()
        {
            if ( string.IsNullOrEmpty( Role ) )
                yield break;

            if ( Role.Contains( "," ) )
                foreach ( var r in Role.Split( ',' ) )
                    yield return r.Trim();
            else
                foreach ( var r in Role.Split( ' ' ) )
                    yield return r.Trim();

        }

    }
}
