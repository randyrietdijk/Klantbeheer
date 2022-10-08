using Common.ApiClient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace CustomerManagement.UI.Authentication
{
	public class CookieAuthenticationProvider : IAuthenticationProvider
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public CookieAuthenticationProvider(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<string> GetAccessTokenAsync()
		{
			HttpContext currentContext = _httpContextAccessor.HttpContext;

			if (!currentContext.User.Identity.IsAuthenticated)
				return null;

			return await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken).ConfigureAwait(false);
		}
	}
}