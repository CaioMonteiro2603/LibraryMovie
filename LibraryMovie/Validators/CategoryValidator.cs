using FluentValidation;
using LibraryMovie.DTOs;

namespace LibraryMovie.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDto>
    {
        public void AddcategoryValidator()
        {
            RuleFor(c => c.Theme)
                .NotEmpty()
                .MaximumLength(10)
                .MinimumLength(3);

            RuleFor(c => c.UserId)
                .NotEmpty(); 
        }
    }
}
