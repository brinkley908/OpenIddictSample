using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ResourceService.Models.Customer;
using ResourceService.Models.Entity.Travelx;
using ResourceService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Service
{
    public class CustomerService : ICustomerService
    {

        private readonly IConfigurationProvider _mappingConfig;

        private readonly IMapper _mapper;

        public readonly ICustomerRepository _customerRepository;

        public readonly azTravelXEntities _txContext;

        public CustomerService(
            azTravelXEntities txContext,
            IMapper mapper,
            IConfigurationProvider mappingConfig,
            ICustomerRepository customerRepository )
        {
            _txContext = txContext;
            _mapper = mapper;
            _mappingConfig = mappingConfig;
            _customerRepository = customerRepository;
        }


        public CustomerDetail GetCustomerDetail( int customerId )
        {
            var customer = _customerRepository.GetByID( customerId );

            return _mapper.Map<CustomerDetail>( customer );
        }

        public int SaveChanges()
        {
            return _txContext.SaveChanges();
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _txContext.SaveChangesAsync();
        }


    }
}
