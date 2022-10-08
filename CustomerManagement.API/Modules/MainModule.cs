using Autofac;
using Common.Data;
using CustomerManagement.API.Core.Data;
using FluentValidation;
using System.Reflection;
using Module = Autofac.Module;

namespace CustomerManagement.API.Modules
{
	public class MainModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			Assembly assembly = typeof(ApplicationDbContext).Assembly;

			builder.RegisterType<QueryDispatcher>().As<IQueryDispatcher>();
			builder.RegisterType<CommandDispatcher>().As<ICommandDispatcher>();

			builder.RegisterAssemblyTypes(assembly)
				.AsClosedTypesOf(typeof(IQueryHandler<,>));

			builder.RegisterAssemblyTypes(assembly)
				.AsClosedTypesOf(typeof(ICommandHandler<,>));

			builder.RegisterAssemblyTypes(assembly)
				.AsClosedTypesOf(typeof(IValidator<>));
		}
	}
}
