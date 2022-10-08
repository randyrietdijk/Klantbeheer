using System.Collections.Immutable;
using System.Security.Claims;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.Server.AspNetCore;

namespace CustomerManagement.IDP.Controllers
{
	public class AuthorizationController : Controller
	{
		private readonly IOpenIddictScopeManager _openIddictScopeManager;

		public AuthorizationController(IOpenIddictScopeManager openIddictScopeManager)
		{
			_openIddictScopeManager = openIddictScopeManager;
		}

		[HttpGet("~/connect/authorize")]
		[HttpPost("~/connect/authorize")]
		[AllowAnonymous]
		[IgnoreAntiforgeryToken]
		public async Task<IActionResult> Authorize()
		{
			var request = HttpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

			var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			if (!result.Succeeded)
			{
				return Challenge(
					authenticationSchemes: CookieAuthenticationDefaults.AuthenticationScheme,
					properties: new AuthenticationProperties
					{
						RedirectUri = Request.PathBase + Request.Path + QueryString.Create(
							Request.HasFormContentType ? Request.Form.ToList() : Request.Query.ToList())
					});
			}

			// Create a new claims principal
			var claims = new List<Claim>
			{
                new Claim(OpenIddictConstants.Claims.Subject, result.Principal.Identity.Name)
					.SetDestinations(OpenIddictConstants.Destinations.AccessToken, OpenIddictConstants.Destinations.IdentityToken)
			};

			var claimsIdentity = new ClaimsIdentity(claims, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
			var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

			// Set requested scopes (this is not done automatically)
			ImmutableArray<string> scopes = request.GetScopes();
			List<string> resources = await _openIddictScopeManager.ListResourcesAsync(scopes).ToListAsync();

			claimsPrincipal.SetScopes(scopes);
			claimsPrincipal.SetResources(resources);

			// Signing in with the OpenIddict authentiction scheme trigger OpenIddict to issue a code (which can be exchanged for an access token)
			return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
		}

		[HttpPost("~/connect/token")]
		[AllowAnonymous]
		public async Task<IActionResult> Exchange()
		{
			var request = HttpContext.GetOpenIddictServerRequest() ?? throw new InvalidOperationException("The OpenID Connect request cannot be retrieved.");

			ClaimsPrincipal claimsPrincipal;

			if (request.IsAuthorizationCodeGrantType())
			{
				// Retrieve the claims principal stored in the authorization code
				claimsPrincipal = (await HttpContext.AuthenticateAsync(OpenIddictServerAspNetCoreDefaults.AuthenticationScheme)).Principal;
			}
			else
			{
				throw new InvalidOperationException("The specified grant type is not supported.");
			}

			return SignIn(claimsPrincipal, OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
		}
	}
}