using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Models.Entity.Travelx
{
    public class CustomerPinned
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CustomerId { get; set; }
    }
}
