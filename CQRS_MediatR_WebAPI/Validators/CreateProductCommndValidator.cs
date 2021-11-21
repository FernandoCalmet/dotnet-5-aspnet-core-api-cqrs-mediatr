using CQRS_MediatR_WebAPI.Application.Features.ProductFeatures.Commands;
using FluentValidation;

namespace CQRS_MediatR_WebAPI.Validators
{
    public class CreateProductCommndValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommndValidator()
        {
            RuleFor(c => c.Barcode).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
        }
    }
}
