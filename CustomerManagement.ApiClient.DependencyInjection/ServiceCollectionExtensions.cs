using Common.ApiClient;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerManagement.ApiClient.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static void AddCustomerManagement<TAuthenticationProvider>(this IServiceCollection services)
			where TAuthenticationProvider : class, IAuthenticationProvider
		{
			services.AddScoped<IAuthenticationProvider, TAuthenticationProvider>();
			services.AddScoped<IApiClient, Common.ApiClient.ApiClient>();

			services.AddScoped<ICustomerManagementApiClient, CustomerManagementApiClient>();
		}
	}
}