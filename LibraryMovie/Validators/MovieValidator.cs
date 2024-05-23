using FluentValidation;
using LibraryMovie.DTOs;

namespace LibraryMovie.Validators
{
    public class MovieValidator : AbstractValidator<MoviesDto>
    {
        public void AddMovieValidator()
        {
            RuleFor(m => m.Title)
                .NotEmpty()
                .MaximumLength(15)
                .MinimumLength(3);

            RuleFor(m => m.RunningTime)
                .NotEmpty();

            RuleFor(m => m.RegistrationDate)
                .NotEmpty();

            RuleFor(m => m.CategoryId)
                .NotEmpty();

            RuleFor(m => m.UserId)
                .NotEmpty();
        }
    }
}
