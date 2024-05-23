using FluentValidation;
using LibraryMovie.DTOs;

namespace LibraryMovie.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public void AddUserValidator()
        {
            RuleFor(u => u.Name)
                .NotEmpty()
                    .WithMessage("Write only your first name, cannot be null!")
                .MaximumLength(10)
                .MinimumLength(3);

            RuleFor(u => u.Email)
                .NotEmpty();
            RuleFor(u => u.Role)
                .NotEmpty();
        }
    }
}
