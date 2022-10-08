using Autofac;
using Common.Data;
using CustomerManagement.API.Core.Data;
using CustomerManagement.API.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Handlers.Customers
{
	public class CreateCustomerCommandHandler : CommandHandler<CreateCustomerCommand, CreateCustomerCommandResult>
	{
		private readonly ILogger<CreateCustomerCommandHandler> _logger;
		private readonly ApplicationDbContext _context;

		public CreateCustomerCommandHandler(ILogger<CreateCustomerCommandHandler> logger, IComponentContext componentContext, ApplicationDbContext context)
			: base(logger, componentContext)
		{
			_logger = logger;
			_context = context;
		}

		protected override async Task<CreateCustomerCommandResult> HandleAsync(CreateCustomerCommand command)
		{
			var result = new CreateCustomerCommandResult();

			var dbCustomer = new Customer
			{
				Name = command.Name
			};

			_context.Customers.Add(dbCustomer);

			await _context.SaveChangesAsync();

			result.Id = dbCustomer.Id;

			return result;
		}
	}
}
