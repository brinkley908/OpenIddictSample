using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService
{
    [AttributeUsage( AttributeTargets.Property, AllowMultiple = false )]
    public class EntityAttribute : Attribute
    {
        public string EntityProperty { get; set; }
    }
}
