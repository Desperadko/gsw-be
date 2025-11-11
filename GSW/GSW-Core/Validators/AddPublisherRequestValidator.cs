using FluentValidation;
using GSW_Core.Requests.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Validators
{
    public class AddPublisherRequestValidator : AbstractValidator<AddPublisherRequest>
    {
        public AddPublisherRequestValidator()
        {
            RuleFor(a => a.Publisher.Name)
                .NotEmpty()
                .WithMessage("Name should not be empty.");
        }
    }
}
