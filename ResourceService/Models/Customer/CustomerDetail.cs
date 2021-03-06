﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Models.Customer
{
    public class CustomerDetail
    {

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

        public DateTime? DateRegistered { get; set; }

        public DateTime? LastUpdated { get; set; }

    }
}
