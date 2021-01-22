using AutoMapper;
using ResourceService.Models.Entity.Travelx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Repository
{
    public class CustomerRepository : DataRepositoryBase<Customer>, ICustomerRepository
    {
        private readonly IConfigurationProvider _mappingConfig;

        private readonly IMapper _mapper;

        private readonly azTravelXEntities _txContext;

        public CustomerRepository( azTravelXEntities txContext, IMapper mapper, IConfigurationProvider mappingConfig ) : base( txContext )
        {
            _txContext = txContext;

            _mapper = mapper;

            _mappingConfig = mappingConfig;
        }




    }
}
