using CarTroubleSolver.Data.Data;
using CarTroubleSolver.Logic.Dto.User;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Validation.UserValidators
{
    public class ChangePasswordUserDtoValidator : AbstractValidator<ChangePasswordUserDto>
    {
        public ChangePasswordUserDtoValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithMessage("Fill Old Password Input")
                .NotEqual(e => e.NewPassword)
                .WithMessage("Both passwords should not be the same");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Fill Password Input")
                .MinimumLength(8).WithMessage("Password should be longer than 8 characters")
                .Matches("[A-Z]").WithMessage("Password should contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password should contain at least one lowercase letter")
                .Matches("[!@#$%^&*()_+{}|:;<>,.?~]").WithMessage("Password should contain at least one special character");

            RuleFor(x => x.ConfirmedNewPassword)
                .Equal(e => e.NewPassword)
                .WithMessage("Both passwords should be the same");

        }
    }
}
