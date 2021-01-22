using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Models.Entity.Travelx
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }

        public string ClientName { get; set; }
    }
}
