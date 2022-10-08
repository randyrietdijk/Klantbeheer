using Common.Data.Models;

namespace CustomerManagement.API.Domain.Models
{
    public class UpdateCustomerCommand : ICommand
    {
		public int Id { get; set; }
        public string Name { get; set; }
    }

	public class UpdateCustomerCommandResult : IResult
	{

	}
}
