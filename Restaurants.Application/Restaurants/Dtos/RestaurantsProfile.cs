using AutoMapper;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantsProfile : Profile
{
	public RestaurantsProfile()
	{
		CreateMap<Restaurant, RestaurantDto>()
			.ForMember(dto => dto.City, opt => opt.MapFrom(r => r.Address == null? null : r.Address.City))
			.ForMember(dto => dto.Street, opt => opt.MapFrom(r => r.Address == null? null : r.Address.Street))
			.ForMember(dto => dto.PostalCode, opt => opt.MapFrom(r => r.Address == null? null : r.Address.PostalCode))
			.ForMember(dto => dto.Dishes, opt => opt.MapFrom(restaurant => restaurant.Dishes));
		
		CreateMap<CreateRestaurantDto, Restaurant>()
			.ForMember(r => r.Address, opt => opt.MapFrom(dto => new Address
			{
				City = dto.City,
				Street = dto.Street,
				PostalCode = dto.PostalCode
			}));
	}
}