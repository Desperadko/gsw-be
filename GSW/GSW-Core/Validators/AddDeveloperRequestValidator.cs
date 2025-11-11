using FluentValidation;
using GSW_Core.Requests.Developer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Validators
{
    public class AddDeveloperRequestValidator : AbstractValidator<AddDeveloperRequest>
    {
        public AddDeveloperRequestValidator()
        {
            RuleFor(a => a.Developer.Name)
                .NotEmpty()
                .WithMessage("Name should not be empty.");
        }
    }
}
