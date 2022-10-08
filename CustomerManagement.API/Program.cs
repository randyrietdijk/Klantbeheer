using Autofac;
using Autofac.Extensions.DependencyInjection;
using CustomerManagement.API.Core.Data;
using CustomerManagement.API.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Autofac
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
	builder.RegisterModule(new MainModule());
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseInMemoryDatabase(nameof(ApplicationDbContext));
});

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.Authority = "https://localhost:4000";
		options.Audience = "api";
	});

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(options =>
{
	options.MapControllers().RequireAuthorization();
});

app.Run();