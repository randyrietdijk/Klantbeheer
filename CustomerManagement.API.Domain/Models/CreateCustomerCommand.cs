using Common.Data.Models;

namespace CustomerManagement.API.Domain.Models
{
    public class CreateCustomerCommand : ICommand
    {
        public string Name { get; set; }
    }

	public class CreateCustomerCommandResult : IResult
	{
		public int Id { get; set; }
	}
}
