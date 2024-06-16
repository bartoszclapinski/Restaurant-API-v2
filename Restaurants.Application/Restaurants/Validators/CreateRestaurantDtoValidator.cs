using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Validators;

public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantDto>
{
	private readonly List<string> _validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];
	
	public CreateRestaurantDtoValidator()
	{
		RuleFor(dto => dto.Name).NotEmpty().Length(3, 100);
		RuleFor(dto => dto.Description).NotEmpty().WithMessage("You need to insert a valid description");
		RuleFor(dto => dto.Category).NotEmpty().Must(category => _validCategories.Contains(category))
			.WithMessage("Please provide a valid category");
		RuleFor(dto => dto.City).NotEmpty();
		RuleFor(dto => dto.Street).NotEmpty();
		RuleFor(dto => dto.PostalCode).NotEmpty();
		RuleFor(dto => dto.ContactEmail).EmailAddress().WithMessage("Please provide a valid email address");
		RuleFor(dto => dto.ContactNumber).Matches(@"^\d{2}-\d{3}$")
			.WithMessage("Please provide a valid phone number (xx-xxx)");
	}
}