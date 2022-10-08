using Common.Data;
using CustomerManagement.API.Core.Data;
using CustomerManagement.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Handlers.Customers
{
	public class GetCustomerListQueryHandler : QueryHandler<GetCustomerListQuery, GetCustomerListQueryResult>
	{
		private readonly ILogger<GetCustomerListQueryHandler> _logger;
		private readonly ApplicationDbContext _context;

		public GetCustomerListQueryHandler(ILogger<GetCustomerListQueryHandler> logger, ApplicationDbContext context)
			: base(logger)
		{
			_logger = logger;
			_context = context;
		}

		protected override async Task<GetCustomerListQueryResult> HandleAsync(GetCustomerListQuery query)
		{
			var result = new GetCustomerListQueryResult();

			result.Customers = await _context.Customers.AsNoTracking()
				.Select(x => new CustomerListItem
				{
					Id = x.Id,
					Name = x.Name
				})
				.ToListAsync();

			return result;
		}
	}
}
