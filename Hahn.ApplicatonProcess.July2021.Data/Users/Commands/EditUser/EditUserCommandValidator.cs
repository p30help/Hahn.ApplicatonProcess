using FluentValidation;

namespace Hahn.ApplicationProcess.July2021.Data.Users.Commands.EditUser
{
    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        public EditUserCommandValidator()
        {
            RuleFor(x => x.Street).NotEmpty()
                .WithMessage("Please enter Street");
            
            RuleFor(x => x.HouseNumber).NotEmpty()
                .WithMessage("Please enter House Number");

            RuleFor(x => x.PostalCode).GreaterThan(0)
                .WithMessage("Please enter Postal Code");
        }
    }
}
