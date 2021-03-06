﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService
{
    [AttributeUsage( AttributeTargets.Property, AllowMultiple = false )]
    public class SearchableRegionAttribute : SearchableAttribute
    {
        public SearchableRegionAttribute()
        {
            ExpressionProvider = new RegionSearchExpressionProvider();
        }
    }
}
