using FluentValidation;
using POS.Application.Dtos.Product.Request;
namespace POS.Application.Validators.Product
{
    public class ProductValidator : AbstractValidator<ProductRequestDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name)
               .Cascade(CascadeMode.Stop)
               .NotNull().WithMessage("El campo Nombre no puede ser nulo.")
               .NotEmpty().WithMessage("El campo Nombre no puede ser vacío.");
        }

    }
}
