using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;


namespace Restaurants.Application.Users.Commands;

public class UpdateUserDetailsCommandHandler(
	ILogger<UpdateUserDetailsCommandHandler> logger,
	IUserContext userContext,
	IUserStore<User> userStore) : IRequestHandler<UpdateUserDetailsCommand> {

	public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
	{
		CurrentUser currentUser = userContext.GetCurrentUser();
		logger.LogInformation("Updating user: {UserId}, with {@Request}", currentUser.Id, request);

		User dbUser = await userStore.FindByIdAsync(currentUser.Id, cancellationToken)
		              ?? throw new NotFoundException(nameof(User), currentUser.Id);

		dbUser.Nationality = request.Nationality;
		dbUser.DateOfBirth = request.DateOfBirth;

		await userStore.UpdateAsync(dbUser, cancellationToken);
	}
}