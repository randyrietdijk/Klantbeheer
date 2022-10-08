using Autofac;
using Common.Data;
using CustomerManagement.API.Core.Data;
using CustomerManagement.API.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Handlers.Customers
{
	public class DeleteCustomerCommandHandler : CommandHandler<DeleteCustomerCommand, DeleteCustomerCommandResult>
	{
		private readonly ILogger<DeleteCustomerCommandHandler> _logger;
		private readonly ApplicationDbContext _context;

		public DeleteCustomerCommandHandler(ILogger<DeleteCustomerCommandHandler> logger, IComponentContext componentContext, ApplicationDbContext context)
			: base(logger, componentContext)
		{
			_logger = logger;
			_context = context;
		}

		protected override async Task<DeleteCustomerCommandResult> HandleAsync(DeleteCustomerCommand command)
		{
			var result = new DeleteCustomerCommandResult();

			var dbCustomer = _context.Customers.Find(command.Id);
			_context.Customers.Remove(dbCustomer);

			await _context.SaveChangesAsync();

			return result;
		}
	}
}
