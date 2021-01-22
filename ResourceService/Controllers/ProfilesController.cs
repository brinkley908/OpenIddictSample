using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResourceService.Models.Customer;
using ResourceService.Models.Entity.Travelx;
using ResourceService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Controllers
{
    [ApiController]
    [Route( "/[controller]" )]
    public class ProfilesController : ControllerBase
    {

        private readonly ICustomerService _customerService;

        private readonly azTravelXEntities _txContext;

        public ProfilesController( azTravelXEntities txContext, ICustomerService customerService )
        {
            _txContext = txContext;

            _customerService = customerService;
        }

        [Authorize( "dataEventRecordsPolicy" )]
        [HttpGet( "CustomerDetails/{id}", Name = nameof( CustomerDetails ) )]
        //[Route( "DetailsSection" )]
        public IActionResult CustomerDetails( int id )
        {

            return Ok( _customerService.GetCustomerDetail( id ) );


        }


    }
}
