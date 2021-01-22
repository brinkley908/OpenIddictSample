using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Models.Entity.Travelx
{
    public class azTravelXEntities : DbContext
    {
        public azTravelXEntities( DbContextOptions<azTravelXEntities> options ) : base( options )
        {
        }


        protected override void OnModelCreating( ModelBuilder builder )
        {
            base.OnModelCreating( builder );

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public virtual DbSet<Customer> Customer { get; set; }

        //   public virtual DbSet<Client> Clients { get; set; }

        //   public virtual DbSet<CustomerPinned> CustomerPinneds { get; set; }

    }
}
