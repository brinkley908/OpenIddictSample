using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using OpeniddictServer.Models;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace OpeniddictServer
{
    public class Worker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker( IServiceProvider serviceProvider )
            => _serviceProvider = serviceProvider;

        public async Task StartAsync( CancellationToken cancellationToken )
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureCreatedAsync( cancellationToken );

            await RegisterApplicationsAsync( scope.ServiceProvider );
            await RegisterScopesAsync( scope.ServiceProvider );

            static async Task RegisterApplicationsAsync( IServiceProvider provider )
            {
                var manager = provider.GetRequiredService<IOpenIddictApplicationManager>();

                // Angular UI client
                if ( await manager.FindByClientIdAsync( "Travelx-React-Client" ) is null )
                {
                    await manager.CreateAsync( new OpenIddictApplicationDescriptor
                    {
                        ClientId = "Travelx-React-Client",
                        ConsentType = ConsentTypes.Explicit,
                        DisplayName = "React TravelX client PKCE",
                        DisplayNames =
                        {
                            [CultureInfo.GetCultureInfo("fr-FR")] = "Application cliente MVC"
                        },
                        PostLogoutRedirectUris =
                        {
                            new Uri("http://localhost:3000")
                        },
                        RedirectUris =
                        {
                            new Uri("http://localhost:3000/signin-oidc")
                        },
                        Permissions =
                        {
                            Permissions.Endpoints.Authorization,
                            Permissions.Endpoints.Logout,
                            Permissions.Endpoints.Token,
                            Permissions.Endpoints.Revocation,
                            Permissions.GrantTypes.AuthorizationCode,
                            Permissions.GrantTypes.RefreshToken,
                            Permissions.ResponseTypes.Code,
                            Permissions.Scopes.Email,
                            Permissions.Scopes.Profile,
                            Permissions.Scopes.Roles,
                            Permissions.Prefixes.Scope + "travelxData"
                        },
                        Requirements =
                        {
                            Requirements.Features.ProofKeyForCodeExchange
                        }
                    } );
                }

                // API
                if ( await manager.FindByClientIdAsync( "rs_travelxDataApi" ) == null )
                {
                    var descriptor = new OpenIddictApplicationDescriptor
                    {
                        ClientId = "rs_travelxDataApi",
                        ClientSecret = "travelxDataSecret",
                        Permissions =
                        {
                            Permissions.Endpoints.Introspection
                        }
                    };

                    await manager.CreateAsync( descriptor );
                }
            }

            static async Task RegisterScopesAsync( IServiceProvider provider )
            {
                var manager = provider.GetRequiredService<IOpenIddictScopeManager>();

                if ( await manager.FindByNameAsync( "travelxData" ) is null )
                {
                    await manager.CreateAsync( new OpenIddictScopeDescriptor
                    {
                        DisplayName = "travelxData API access",
                        DisplayNames =
                        {
                            [CultureInfo.GetCultureInfo("fr-FR")] = "Accès à l'API de démo"
                        },
                        Name = "travelxData",
                        Resources =
                        {
                            "rs_travelxDataApi"
                        }
                    } );
                }
            }
        }

        public Task StopAsync( CancellationToken cancellationToken ) => Task.CompletedTask;
    }
}
