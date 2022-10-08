using Microsoft.EntityFrameworkCore;
using OpenIddict.Abstractions;
using System.Resources;
using System.Xml.Linq;

namespace CustomerManagement.IDP.Seeds
{
	public class OpenIddictDataSeed : IHostedService
	{
		private readonly IServiceProvider _serviceProvider;

		public OpenIddictDataSeed(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				var context = scope.ServiceProvider.GetRequiredService<DbContext>();
				await context.Database.EnsureCreatedAsync(cancellationToken);

				var appManager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

				if (await appManager.FindByClientIdAsync("mvc", cancellationToken) is null)
				{
					await appManager.CreateAsync(new OpenIddictApplicationDescriptor
					{
						ClientId = "mvc",
						DisplayName = "mvc",
						RedirectUris = { new Uri("https://localhost:4002/signin-oidc") },
						Permissions =
					{
						OpenIddictConstants.Permissions.Endpoints.Authorization,
						OpenIddictConstants.Permissions.Endpoints.Token,

						OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,

						OpenIddictConstants.Permissions.Prefixes.Scope + "api",
						OpenIddictConstants.Permissions.ResponseTypes.Code
					}
					}, cancellationToken);
				}

				var scopeManager = scope.ServiceProvider.GetRequiredService<IOpenIddictScopeManager>();

				if (await scopeManager.FindByNameAsync("api", cancellationToken) is null)
				{
					await scopeManager.CreateAsync(new OpenIddictScopeDescriptor
					{
						Name = "api",
						DisplayName = "api",
						Resources =
						{
							"api"
						}
					}, cancellationToken);
				}
			}
		}

		public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
	}
}