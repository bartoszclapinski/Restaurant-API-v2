using FluentValidation;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
	private int[] allowedPageSizes = [5, 10, 15, 20, 25, 30];
	
	public GetAllRestaurantsQueryValidator()
	{
		RuleFor(x => x.PageNumber).GreaterThan(1);
		RuleFor(x => x.PageSize)
			.Must(value => allowedPageSizes.Contains(value))
			.WithMessage($"Page size must be one of the following: {string.Join(", ", allowedPageSizes)}");
	}
}