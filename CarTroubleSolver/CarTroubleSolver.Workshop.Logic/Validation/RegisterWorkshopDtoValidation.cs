using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Workshop.Logic.Dto.Workshop;
using FluentValidation;

namespace CarTroubleSolver.Workshop.Logic.Validation
{
    public class RegisterWorkshopDtoValidation : AbstractValidator<RegisterWorkshopDto>
    {
        public RegisterWorkshopDtoValidation() { }
        public RegisterWorkshopDtoValidation(CarTroubleSolverDbContext dbContext)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Fill Name Input");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Fill Password Input")
                .MinimumLength(8).WithMessage("Password should be longer than 8 characters")
                .Matches("[A-Z]").WithMessage("Password should contain one uppercase letter")
                .Matches("[a-z]").WithMessage("Password should contain one lowercase letter")
                .Matches("[!@#$%^&*()_+{}|:;<>,.?~]").WithMessage("Password should contain one special character");

            RuleFor(x => x.ConfirmedPassword)
                .Equal(e => e.Password)
                .WithMessage("Both passwords should be the same");

            RuleFor(x => x.Email)
                .Custom((value, context) =>
                {
                    var usedEmail = dbContext.Workshops.Any(u => u.Email == value);

                    if (usedEmail)
                        context.AddFailure("Email", "This email is taken");
                })
                .NotEmpty()
                .WithMessage("Fill Email Input")
                .EmailAddress()
                .WithMessage("Please enter a valid email address.");

            RuleFor(x => x.PhoneNumber)
                .Must(x => x.ToString().Length == 9)
                .WithMessage("telephone number should have 9 digits");

            RuleFor(x => x.NIP)
                .Must(x => x.ToString().Length == 10 || x.ToString().Length == 11)
                .WithMessage("telephone number should have 10 or 11 digits")
                .Custom((value, context) =>
                {
                    var usedNIP = dbContext.Workshops.Any(u => u.NIP == value);

                    if (usedNIP)
                        context.AddFailure("NIP", "This NIP is taken");
                });

            RuleFor(x => x.Street)
                .NotNull()
                .NotEmpty()
                .WithMessage("Street is required");

            RuleFor(x => x.Street.StreetName)
                .NotEmpty().WithMessage("Street name is required.");

            RuleFor(x => x.Street.StreetNumber)
                .NotEmpty().WithMessage("Street number is required.");

            RuleFor(x => x.Street.PostalCode)
                .NotEmpty().WithMessage("Postal code is required.");

            RuleFor(x => x.Street.City)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(x => x.Street.Country)
                .NotEmpty().WithMessage("Country is required.");

            RuleFor(x => x.Street.Province)
                .NotEmpty().WithMessage("Province is required.")
                .Must(BeAValidProvince).WithMessage("Invalid province.");
        }
        private bool BeAValidProvince(string province)
        {
            return Enum.TryParse(typeof(Province), province, out _);
        }
    }
}