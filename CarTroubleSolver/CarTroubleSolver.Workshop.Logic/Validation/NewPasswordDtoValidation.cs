using CarTroubleSolver.Workshop.Logic.Dto.Workshop;
using FluentValidation;

namespace CarTroubleSolver.Workshop.Logic.Validation
{
    public class NewPasswordDtoValidation : AbstractValidator<NewPasswordDto>
    {
        public NewPasswordDtoValidation()
        {

            RuleFor(x => x.NewPassword)
                    .NotEmpty().WithMessage("Fill Password Input")
                    .MinimumLength(8).WithMessage("Password should be longer than 8 characters")
                    .Matches("[A-Z]").WithMessage("Password should contain one uppercase letter")
                    .Matches("[a-z]").WithMessage("Password should contain one lowercase letter")
                    .Matches("[!@#$%^&*()_+{}|:;<>,.?~]").WithMessage("Password should contain one special character");

            RuleFor(x => x.NewConfirmedPassword)
                .Equal(e => e.NewPassword)
                .WithMessage("Both passwords should be the same");
        }
    }
}
