using CustomerManagement.ApiClient.DependencyInjection;
using CustomerManagement.UI.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
	options.Filters.Add(typeof(AutoValidateAntiforgeryTokenAttribute));
})
	.AddRazorRuntimeCompilation();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Authentication
builder.Services.AddAuthentication(options =>
	{
		options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
	})
	.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
	{

	})
	.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
	{
		options.Authority = "https://localhost:4000";

		options.ClientId = "mvc";
		options.ResponseType = "code";

		options.Scope.Clear();
		options.Scope.Add("openid");
		options.Scope.Add("api");

		options.UsePkce = true;
		options.SaveTokens = true;

		options.TokenValidationParameters.NameClaimType = ClaimTypes.NameIdentifier;
	});

builder.Services.AddHttpContextAccessor();

// API client
builder.Services.AddCustomerManagement<CookieAuthenticationProvider>();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(options =>
{
	options.MapDefaultControllerRoute();
	options.MapControllers().RequireAuthorization();
});

app.Run();