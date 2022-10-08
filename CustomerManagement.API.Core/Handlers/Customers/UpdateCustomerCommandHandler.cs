using Autofac;
using Common.Data;
using CustomerManagement.API.Core.Data;
using CustomerManagement.API.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Handlers.Customers
{
	public class UpdateCustomerCommandHandler : CommandHandler<UpdateCustomerCommand, UpdateCustomerCommandResult>
	{
		private readonly ILogger<UpdateCustomerCommandHandler> _logger;
		private readonly ApplicationDbContext _context;

		public UpdateCustomerCommandHandler(ILogger<UpdateCustomerCommandHandler> logger, IComponentContext componentContext, ApplicationDbContext context)
			: base(logger, componentContext)
		{
			_logger = logger;
			_context = context;
		}

		protected override async Task<UpdateCustomerCommandResult> HandleAsync(UpdateCustomerCommand command)
		{
			var result = new UpdateCustomerCommandResult();

			var dbCustomer = _context.Customers.Find(command.Id);

			dbCustomer.Name = command.Name;

			await _context.SaveChangesAsync();

			return result;
		}
	}
}
