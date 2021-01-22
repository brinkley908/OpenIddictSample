using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Validation.AspNetCore;
using ResourceService.Filters;
using ResourceService.Models.Entity.Travelx;
using ResourceService.Repository;
using ResourceService.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceService
{
    public class Startup
    {


        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices( IServiceCollection services )
        {

            services.AddMvc( options =>
            {
                options.Filters.Add<JsonExceptionFilter>();
                options.Filters.Add<RequireHttps>();
                options.Filters.Add<LinkRewritingFilter>();
            } );

            var txConn = Configuration.GetConnectionString( "DefaultConnection" );

            services.AddDbContext<azTravelXEntities>( options =>
               options.UseSqlServer( txConn )
          );

            services.AddAutoMapper( options => options.AddProfile<MappingProfile>() );

            //services.AddResponseCompression( options =>
            //{
            //    options.Providers.Add<GzipCompressionProvider>();
            //    options.EnableForHttps = true;
            //} );

            services.AddControllersWithViews();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles( configuration =>
             {
                 configuration.RootPath = "ClientApp/build";
             } );


            services.AddCors( options =>
            {
                options.AddPolicy( "AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowCredentials()
                            .WithOrigins(
                                new string[] { "https://localhost:44306", "https://localhost:44369" } )
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    } );
            } );

            var guestPolicy = new AuthorizationPolicyBuilder()
             .RequireAuthenticatedUser()
             .RequireClaim( "scope", "travelxData" )
             .Build();

            services.AddAuthentication( options =>
            {
                options.DefaultScheme = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme;
            } );

            services.AddOpenIddict()
              .AddValidation( options =>
              {
                  // Note: the validation handler uses OpenID Connect discovery
                  // to retrieve the address of the introspection endpoint.
                  options.SetIssuer( "https://localhost:44395/" );
                  options.AddAudiences( "rs_travelxDataApi" );

                  // Configure the validation handler to use introspection and register the client
                  // credentials used when communicating with the remote introspection endpoint.
                  options.UseIntrospection()
                       .SetClientId( "rs_travelxDataApi" )
                       .SetClientSecret( "travelxDataSecret" );

                  // Register the System.Net.Http integration.
                  options.UseSystemNetHttp();

                  // Register the ASP.NET Core host.
                  options.UseAspNetCore();
              } );

            services.AddScoped<IAuthorizationHandler, RequireScopeHandler>();

            services.AddAuthorization( options =>
            {
                options.AddPolicy( "travelxDataPolicy", policyUser =>
                {
                    policyUser.Requirements.Add( new RequireScope() );
                } );
            } );

            services
                   .AddScoped( typeof( IDataRepositoryBase<> ), typeof( DataRepositoryBase<> ) )
                   .AddScoped<ICustomerRepository, CustomerRepository>();
            //       .AddScoped<IAirportsRepository, AirportsRepository>();

            services

                    .AddScoped<ICustomerService, CustomerService>();
            //        .AddScoped<IAirportService, AirportService>()
            //        .AddScoped<IUserService, UserService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure( IApplicationBuilder app, IWebHostEnvironment env )
        {
            if ( env.IsDevelopment() )
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler( "/Error" );
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors( "AllowAllOrigins" );

            //   app.UseResponseCompression();

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller}/{action=Index}/{id?}" );
             } );

            app.UseSpa( spa =>
             {
                 spa.Options.SourcePath = "ClientApp";

                 if ( env.IsDevelopment() )
                 {
                     spa.UseReactDevelopmentServer( npmScript: "start" );
                 }
             } );
        }
    }


    public class RequireScopeHandler : AuthorizationHandler<RequireScope>
    {

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            RequireScope requirement
        )
        {
            if ( context == null )
                throw new ArgumentNullException( nameof( context ) );
            if ( requirement == null )
                throw new ArgumentNullException( nameof( requirement ) );

            var scopeClaim = context.User.Claims.FirstOrDefault( t => t.Type == "scope" );

            if ( scopeClaim != null && scopeClaim.Value.Contains( "travelxData" ) )
            {
                context.Succeed( requirement );
            }

            return Task.CompletedTask;
        }
    }

    public class RequireScope : IAuthorizationRequirement { }
}
