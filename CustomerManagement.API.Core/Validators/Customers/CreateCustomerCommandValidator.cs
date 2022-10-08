using CustomerManagement.API.Domain.Models;
using FluentValidation;

namespace CustomerManagement.API.Core.Validators.Customers
{
	public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
	{
		public CreateCustomerCommandValidator()
		{
			RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
				.NotEmpty().WithMessage("De naam is verplicht.")
				.MaximumLength(50).WithMessage("De maximale lengte voor de naam is 50 tekens.");
		}
	}
}
