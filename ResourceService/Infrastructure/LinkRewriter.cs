using Microsoft.AspNetCore.Mvc;
using ResourceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService
{
    public class LinkRewriter
    {

        private readonly IUrlHelper _urlHelper;

        public LinkRewriter( IUrlHelper urlHelper )
        {
            _urlHelper = urlHelper;
        }

        public Link ReWrite( Link original )
        {
            if ( original == null )
                return null;

            return new Link
            {
                Href = _urlHelper.Link( original.RouteName, original.RouteValues ),
                Method = original.Method,
                Relations = original.Relations
            };
        }

    }
}
