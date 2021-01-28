using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Models.Entity.Travelx
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public int ClientId { get; set; }

        public string Username { get; set; }

        public bool AllowAccess { get; set; }

        public string Title { get; set; }

        public string GivenName { get; set; }

        public string MiddleNames { get; set; }

        public string Surname { get; set; }

        public bool? VIP { get; set; }

        public bool? Partner { get; set; }

        public string DOB { get; set; }

        public int? UpdateCount { get; set; }

        public int? HitCount { get; set; }

        public string DateRegistered { get; set; }

        public string LastUpdated { get; set; }

        public virtual ICollection<CustomerPinned> CustomerPinneds { get; set; }

    }
}
