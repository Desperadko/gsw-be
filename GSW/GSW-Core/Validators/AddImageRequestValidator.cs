using FluentValidation;
using GSW_Core.Requests.Image;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Validators
{
    public class AddImageRequestValidator : AbstractValidator<AddImageRequest>
    {
        public AddImageRequestValidator()
        {
            RuleFor(r => r.Image)
                .NotEmpty()
                .WithMessage("Image should not be empty")
                .OverridePropertyName(nameof(AddImageRequest.Image));

            RuleFor(r => r.Image.ProductId)
                .NotEmpty()
                .WithMessage("Product ID should not be empty")
                .OverridePropertyName(nameof(AddImageRequest.Image.ProductId));

            RuleFor(r => r.Image.ProductId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product ID should be grater than or equal to 0")
                .OverridePropertyName(nameof(AddImageRequest.Image.ProductId));

            RuleFor(r => r.Image.ImageData)
                .NotEmpty()
                .WithMessage("Image Data should not be empty")
                .OverridePropertyName(nameof(AddImageRequest.Image.ImageData));
        }
    }
}
