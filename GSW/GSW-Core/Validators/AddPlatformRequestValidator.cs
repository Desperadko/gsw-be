using FluentValidation;
using GSW_Core.Requests.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Validators
{
    public class AddPlatformRequestValidator : AbstractValidator<AddPlatformRequest>
    {
        public AddPlatformRequestValidator()
        {
            RuleFor(a => a.Platform.Name)
                .NotEmpty()
                .WithMessage("Name should not be empty.");
        }
    }
}
