using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Models
{
    public class RootResponse : Resource
    {

        public Link Airports { get; set; }
        public Link Facts { get; set; }

    }
}
