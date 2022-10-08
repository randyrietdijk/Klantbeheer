using Common.Data.Models;

namespace CustomerManagement.API.Domain.Models
{
    public class DeleteCustomerCommand : ICommand
    {
        public int Id { get; set; }
    }

	public class DeleteCustomerCommandResult : IResult
	{

	}
}
