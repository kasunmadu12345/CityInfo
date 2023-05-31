using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using CityInfo.API.Entities;
namespace CityInfo.API.Context
{

    public class CityInfoContext : DbContext
    {

        public DbSet<City> Cities { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity mappings, if any
        }

        public void SeedData()
        {
            if (!Cities.Any())
            {
                Cities.AddRange(
                    new City { Name = "City 1", Description = "Description 1" },
                    new City { Name = "City 2", Description = "Description 2" },
                    new City { Name = "City 3", Description = "Description 3" }
                );

                SaveChanges();
            }
        }
    }
}

