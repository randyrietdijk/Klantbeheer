using Common.Data;
using CustomerManagement.API.Core.Data;
using CustomerManagement.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Handlers.Customers
{
	public class GetCustomerQueryHandler : QueryHandler<GetCustomerQuery, GetCustomerQueryResult>
	{
		private readonly ILogger<GetCustomerQueryHandler> _logger;
		private readonly ApplicationDbContext _context;

		public GetCustomerQueryHandler(ILogger<GetCustomerQueryHandler> logger, ApplicationDbContext context)
			: base(logger)
		{
			_logger = logger;
			_context = context;
		}

		protected override async Task<GetCustomerQueryResult> HandleAsync(GetCustomerQuery query)
		{
			var result = new GetCustomerQueryResult();

			result.Customer = await _context.Customers.AsNoTracking()
				.Where(x => x.Id == query.Id)
				.Select(x => new CustomerVM
				{
					Id = x.Id,
					Name = x.Name
				})
				.SingleOrDefaultAsync();

			return result;
		}
	}
}
