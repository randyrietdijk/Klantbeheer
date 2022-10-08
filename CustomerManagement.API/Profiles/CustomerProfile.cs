using AutoMapper;
using CustomerManagement.API.Domain.Models;
using CustomerManagement.API.ViewModels;

namespace CustomerManagement.API.Profiles
{
	public class CustomerProfile : Profile
	{
		public CustomerProfile()
		{
			CreateMap<CustomerCreateForm, CreateCustomerCommand>();
			CreateMap<CustomerUpdateForm, UpdateCustomerCommand>();
		}
	}
}
