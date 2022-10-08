using CustomerManagement.IDP.Seeds;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
	.AddRazorRuntimeCompilation();

// DbContext
builder.Services.AddDbContext<DbContext>(options =>
{
	options.UseInMemoryDatabase(nameof(DbContext));
	options.UseOpenIddict();
});

// OpenIddict
builder.Services.AddOpenIddict()
	.AddCore(options =>
	{
		options.UseEntityFrameworkCore()
			.UseDbContext<DbContext>();
	})
	.AddServer(options =>
	{
		options
			.AllowAuthorizationCodeFlow().RequireProofKeyForCodeExchange();

		options
			.SetTokenEndpointUris("/connect/token")
			.SetAuthorizationEndpointUris("/connect/authorize");
	
		options
			.AddEphemeralEncryptionKey()
			.AddEphemeralSigningKey()
			.DisableAccessTokenEncryption();
	
		options.RegisterScopes("api");
	
		options
			.UseAspNetCore()
			.EnableTokenEndpointPassthrough()
			.EnableAuthorizationEndpointPassthrough();
	});

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			  .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

builder.Services.AddHostedService<OpenIddictDataSeed>();

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