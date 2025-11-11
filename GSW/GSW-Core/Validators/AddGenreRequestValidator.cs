using FluentValidation;
using GSW_Core.Requests.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Validators
{
    public class AddGenreRequestValidator : AbstractValidator<AddGenreRequest>
    {
        public AddGenreRequestValidator()
        {
            RuleFor(a => a.Genre.Name)
                .NotEmpty()
                .WithMessage("Name should not be empty.");
        }
    }
}
