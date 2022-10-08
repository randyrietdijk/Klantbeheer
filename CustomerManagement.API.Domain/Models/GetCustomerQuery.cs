using Common.Data.Models;

namespace CustomerManagement.API.Domain.Models
{
    public class GetCustomerQuery : IQuery
    {
        public int Id { get; set; }
    }

	public class GetCustomerQueryResult : IResult
	{
        public CustomerVM Customer { get; set; }
	}
}
