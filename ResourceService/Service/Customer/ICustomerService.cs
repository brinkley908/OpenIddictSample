using Microsoft.EntityFrameworkCore;
using ResourceService.Models.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Service
{
    public interface ICustomerService
    {

        CustomerDetail GetCustomerDetail( int customerId );

        int SaveChanges();

        Task<int> SaveChangesAsync();

    }
}
