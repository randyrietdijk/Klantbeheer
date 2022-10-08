using AutoMapper;
using CustomerManagement.ApiClient.Models.Customers;

namespace CustomerManagement.API.Profiles
{
	public class CustomerProfile : Profile
	{
		public CustomerProfile()
		{
			CreateMap<CustomerVM, CustomerUpdateForm>();
		}
	}
}
