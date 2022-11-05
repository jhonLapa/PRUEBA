using FluentValidation;
using POS.Application.Dtos.User.Request;

namespace POS.Application.Validators.User
{
    public class UserValidator : AbstractValidator<UserRequestDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("El campo UserName no puede ser nulo.")
               .NotEmpty().WithMessage("El campo UserName no puede ser vacío.");


        }
    }
}
