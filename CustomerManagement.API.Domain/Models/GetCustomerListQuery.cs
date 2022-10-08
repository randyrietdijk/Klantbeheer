using Common.Data.Models;
using System.Collections.Generic;

namespace CustomerManagement.API.Domain.Models
{
    public class GetCustomerListQuery : IQuery
    {
        
    }

	public class GetCustomerListQueryResult : IResult
	{
        public List<CustomerListItem> Customers { get; set; }
	}
}
