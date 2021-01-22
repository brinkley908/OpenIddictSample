
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService.Models
{
    public class SortOptions<T, TEntity> : IValidatableObject
    {



        public string[] OrderBy { get; set; }

        //public SortOptions( AutoMapper.IConfigurationProvider mappingConfig )
        //{
        //    _mappingConfig = mappingConfig;
        //}

        public IEnumerable<ValidationResult> Validate( ValidationContext validationContext )
        {
            var processor = new SortOptionsProcessor<T, TEntity>( OrderBy );

            var validTerms = processor.GetValidTerms().Select( x => x.Name );

            var invalidTerms = processor.GetAllTerms()
                .Where( x => !validTerms.Any( v => v.Equals( x.Name, StringComparison.OrdinalIgnoreCase ) ) )
                .Select( x => x.Name );

            foreach ( var term in invalidTerms )
                yield return new ValidationResult( $"Invalid sort term {term}", new[] { nameof( OrderBy ) } );

        }

        public IQueryable<TEntity> Apply( IQueryable<TEntity> query )
        {
            var processor = new SortOptionsProcessor<T, TEntity>( OrderBy );

            return processor.Apply( query );
        }
    }
}
