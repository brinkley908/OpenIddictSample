using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Models
{
    public class PagingOptions
    {
        [Range( 1, 99999, ErrorMessage = "Offset must be > 0" )]
        public int? Offset { get; set; }

        [Range( 1, 100, ErrorMessage = "Limit must be > 0 and <= 100" )]
        public int? Limit { get; set; }

        public PagingOptions Replace( PagingOptions opts )
            => new PagingOptions
            {
                Offset = opts.Offset ?? this.Offset,
                Limit = opts.Limit ?? this.Limit
            };


    }
}
