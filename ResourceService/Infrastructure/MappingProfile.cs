using AutoMapper;
using ResourceService.Models;
using ResourceService.Models.Customer;
using ResourceService.Models.Entity.Travelx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            var deleteRelation = Form.DeleteRelation;

            CreateMap<Customer, CustomerDetail>();

            //CreateMap<InterestingFactEntity, Fact>()
            //  .ForMember( d => d.Self, o => o.MapFrom( src => Link.To( nameof( Controllers.FactsController.GetFact ), new { id = src.recID } ) ) );

            //CreateMap<vwAirportEntity, Airport>()
            //  .ForMember( d => d.Self, o => o.MapFrom( src => Link.To( nameof( Controllers.AirportsController.GetAirport ), new { code = src.AIR_CODE } ) ) )
            //  .ForMember( d => d.Delete, o => o.MapFrom( src => FormMetadata.FromModel(null, Link.ToDelete( nameof(Controllers.AirportsController.DeleteAirportByCode), new { code =src.AIR_CODE }, deleteRelation ))))
            //  .ForMember( d => d.Code, o => o.MapFrom( src => src.AIR_CODE ) )
            //  .ForMember( d => d.Name, o => o.MapFrom( src => src.AIR_NAME ) )
            //  .ForMember( d => d.Town, o => o.MapFrom( src => src.AIR_TOWN ) )
            //  .ForMember( d => d.City, o => o.MapFrom( src => src.AIR_CITY ) )
            //  .ForMember( d => d.Country, o => o.MapFrom( src => src.AIR_COUNTR ) )
            //  .ForMember( d => d.CountryCode, o => o.MapFrom( src => src.COUNTRY_CODE ) )
            //  .ForMember( d => d.Region, o => o.MapFrom( src => src.AIR_REGION ) )
            //  .ForMember( d => d.GenericCode, o => o.MapFrom( src => src.GENERICCODE ) )
            //  .ForMember( d => d.OBTHide, o => o.MapFrom( src => src.FreeWay_Hide ) )
            //  ;

            //CreateMap<Airport, AirportEntity>()
            //   .ForMember( d => d.AIR_CODE, o => o.MapFrom( src => src.Code ) )
            //   .ForMember( d => d.AIR_NAME, o => o.MapFrom( src => src.Name ) )
            //   .ForMember( d => d.AIR_TOWN, o => o.MapFrom( src => src.Town ) )
            //   .ForMember( d => d.AIR_CITY, o => o.MapFrom( src => src.City ) )
            //   .ForMember( d => d.AIR_COUNTR, o => o.MapFrom( src => src.Country ) )
            //   .ForMember( d => d.COUNTRY_CODE, o => o.MapFrom( src => src.CountryCode ) )
            //   .ForMember( d => d.AIR_REGION, o => o.MapFrom( src => src.Region.ToString().PadLeft( 2, '0' ) ) )
            //   .ForMember( d => d.GENERICCODE, o => o.MapFrom( src => src.GenericCode ) )
            //   .ForMember( d => d.FreeWay_Hide, o => o.MapFrom( src => src.OBTHide ) )
            //   ;

            //CreateMap<AspNetUser, User>()
            //  .ForMember( dest => dest.Self, opt => opt.MapFrom( src =>
            //      Link.To( nameof( Controllers.UsersController.GetUserById ),
            //      new { userId = src.Id } ) ) );

        }


        public static string GetMappedOriginField<TEntity, T>( IConfigurationProvider mapper, string fieldName )
                => mapper
                ?.FindTypeMapFor<TEntity, T>()
                ?.PropertyMaps
                ?.FirstOrDefault( x => fieldName.Equals( x.DestinationName, StringComparison.OrdinalIgnoreCase ) )
                ?.SourceMember
                ?.Name

                ?? fieldName;


    }
}
