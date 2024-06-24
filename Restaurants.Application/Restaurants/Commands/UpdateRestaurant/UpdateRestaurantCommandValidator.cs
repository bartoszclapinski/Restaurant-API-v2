using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
	public UpdateRestaurantCommandValidator()
	{
		RuleFor(c => c.Name).NotEmpty().Length(3, 100);
	}
}