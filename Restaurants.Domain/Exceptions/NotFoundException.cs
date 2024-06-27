namespace Restaurants.Domain.Exceptions;

public class NotFoundException(string resourceType, string resourceIdentifier)
	: Exception($"{resourceType} with identifier {resourceIdentifier} was not found.");
