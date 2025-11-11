using FluentValidation;
using GSW_Core.Requests.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSW_Core.Validators
{
    public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
    {
        public UpdateRoleRequestValidator()
        {
            RuleFor(a => a.Role)
                .NotEmpty()
                .WithMessage("Role should not be empty.");
        }
    }
}
