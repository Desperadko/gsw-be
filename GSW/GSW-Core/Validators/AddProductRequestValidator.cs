using FluentValidation;
using GSW_Core.Requests.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Validators
{
    public class AddProductRequestValidator : AbstractValidator<AddProductRequest>
    {
        public AddProductRequestValidator()
        {
            RuleFor(r => r.Product.Name)
                .NotEmpty()
                .WithMessage("Product name should not be empty")
                .OverridePropertyName(nameof(AddProductRequest.Product.Name));

            RuleFor(r => r.Product.Description)
                .NotEmpty()
                .WithMessage("Product description should not be empty")
                .OverridePropertyName(nameof(AddProductRequest.Product.Description));

            RuleFor(r => r.Product.ReleaseDate)
                .NotEmpty()
                .WithMessage("Product release date should not be empty")
                .OverridePropertyName(nameof(AddProductRequest.Product.ReleaseDate));

            RuleFor(r => r.Product.Price)
                .NotEmpty()
                .WithMessage("Product price should not be empty")
                .GreaterThan(0)
                .WithMessage("Price should not be a negative number")
                .OverridePropertyName(nameof(AddProductRequest.Product.Price));

            RuleFor(r => r.Product.GenresIds)
                .NotEmpty()
                .WithMessage("Product genres should not be empty")
                .OverridePropertyName(nameof(AddProductRequest.Product.GenresIds));

            RuleForEach(r => r.Product.GenresIds)
                .GreaterThanOrEqualTo(0)
                .WithMessage("genres should not contain a negative id")
                .OverridePropertyName(nameof(AddProductRequest.Product.GenresIds));

            RuleFor(r => r.Product.PlatformsIds)
                .NotEmpty()
                .WithMessage("Product platforms should not be empty")
                .OverridePropertyName(nameof(AddProductRequest.Product.PlatformsIds));

            RuleForEach(r => r.Product.PlatformsIds)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Platforms should not contain a negative id")
                .OverridePropertyName(nameof(AddProductRequest.Product.PlatformsIds));

            RuleFor(r => r.Product.DevelopersIds)
                .NotEmpty()
                .WithMessage("Product developers should not be empty")
                .OverridePropertyName(nameof(AddProductRequest.Product.DevelopersIds));

            RuleForEach(r => r.Product.DevelopersIds)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Developers should not contain a negative id")
                .OverridePropertyName(nameof(AddProductRequest.Product.DevelopersIds));

            RuleForEach(r => r.Product.PublishersIds)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Publishers should not contain a negative id")
                .OverridePropertyName(nameof(AddProductRequest.Product.PublishersIds));
        }
    }
}
