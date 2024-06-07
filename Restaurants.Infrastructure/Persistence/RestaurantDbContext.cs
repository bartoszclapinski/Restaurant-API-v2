using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Persistence;

internal class RestaurantDbContext : DbContext
{
	public DbSet<Restaurant> Restaurants { get; set; }
	public DbSet<Dish> Dishes { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RestaurantsV2Db;Trusted_Connection=True;");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Restaurant>()
			.OwnsOne(r => r.Address);
		
		modelBuilder.Entity<Restaurant>()
			.HasMany(r => r.Dishes)
			.WithOne()
			.HasForeignKey(d => d.RestaurantId);
	}
}