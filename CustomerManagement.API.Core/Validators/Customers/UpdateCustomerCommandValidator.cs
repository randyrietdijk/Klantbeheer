using CustomerManagement.API.Core.Data;
using CustomerManagement.API.Domain.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerManagement.API.Core.Validators.Customers
{
	public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
	{
		private readonly ApplicationDbContext _context;

		public UpdateCustomerCommandValidator(ApplicationDbContext context)
		{
			_context = context;

			RuleFor(x => x.Id).Cascade(CascadeMode.Stop)
				.MustAsync(DoesCustomerExist).WithMessage("De klant is niet gevonden.")
				.DependentRules(() =>
				{
					RuleFor(x => x.Name).Cascade(CascadeMode.Stop)
						.NotEmpty().WithMessage("De naam is verplicht.")
						.MaximumLength(50).WithMessage("De maximale lengte voor de naam is 50 tekens.");
				});
		}

		private async Task<bool> DoesCustomerExist(int id, CancellationToken cancellationToken)
		{
			var dbCustomer = await _context.Customers.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
			return dbCustomer != null;
		}
	}
}
