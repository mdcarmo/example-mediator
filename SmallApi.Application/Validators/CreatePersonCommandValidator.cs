using FluentValidation;
using SmallApi.Application.Commands;

namespace SmallApi.Application.Validators
{
    public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator()
        {
            RuleFor(o => o.FirstName).NotEmpty().WithMessage("First Name is required").WithErrorCode("1001");

            RuleFor(o => o.LastName).NotEmpty();

            RuleFor(o => o.Type).NotEmpty().Must(IsValidType).WithMessage("Type incorrect");

            RuleFor(o => o.Active).NotEmpty();

            RuleFor(x => x.FirstName).Length(0, 10);

            RuleFor(x => x.Email).EmailAddress();

            RuleFor(x => x.Age).InclusiveBetween(18, 60);

            RuleFor(o => o.Register).GreaterThan(1);
        }

        /// <summary>
        /// TODO:Documentar
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsValidType(string type)
        {
            var isValid = type.Equals("EMPLOYEE", System.StringComparison.OrdinalIgnoreCase) || type.Equals("TRAINEE", System.StringComparison.OrdinalIgnoreCase);
            return isValid;
        }

    }
}
