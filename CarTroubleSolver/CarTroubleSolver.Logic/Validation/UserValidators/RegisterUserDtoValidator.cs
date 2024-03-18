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
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(){ }
        public RegisterUserDtoValidator(CarTroubleSolverDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Fill Name Input");

            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Fill Surname Input");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Fill Password Input")
                .MinimumLength(8).WithMessage("Password should be longer than 8 characters")
                .Matches("[A-Z]").WithMessage("Password should contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password should contain at least one lowercase letter")
                .Matches("[!@#$%^&*()_+{}|:;<>,.?~]").WithMessage("Password should contain at least one special character");

            RuleFor(x => x.PasswordConfirmed)
                .Equal(e => e.Password)
                .WithMessage("Both passwords should be the same");

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var usedEmail = dbContext.Users.Any(u => u.Email == value);

                    if (usedEmail)
                        context.AddFailure("Email", "This email is taken");
                })
                .NotEmpty()
                .WithMessage("Fill Email Input")
                .EmailAddress()
                .WithMessage("Invalid email format. Please enter a valid email address.");

            RuleFor(x => x.PhoneNumber)
                .Must(x => x.ToString().Length == 9)
                .WithMessage("telephone number should have 9 digits");
        }
    }
}
