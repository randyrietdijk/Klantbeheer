using CustomerManagement.API.Domain.Models;
using FluentValidation;

namespace CustomerManagement.API.Core.Validators.Customers
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {

        }
    }
}
