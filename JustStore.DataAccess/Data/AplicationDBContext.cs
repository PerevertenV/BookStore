﻿using DataAccess.Entity;
using JustStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{
	public class AplicationDBContextcs : IdentityDbContext<IdentityUser>
	{
		public AplicationDBContextcs(DbContextOptions<AplicationDBContextcs> options) :
			base(options)
		{

		}
		public DbSet<CategoryEntity> Categories { get; set; }
		public DbSet<ProductEntity> Products { get; set; }
		public DbSet<ProductImagesEntity> ProductImages { get; set; }
        public DbSet<ApplicationUser> applicationUser {  get; set; } 
        public DbSet<CompanyEntity> CompanyUsers {  get; set; } 
        public DbSet<ShoppingCartEntity> ShoppingCarts {  get; set; } 
        public DbSet<OrderHeaderEntity> OrderHeaders {  get; set; } 
        public DbSet<OrderDetailsEntity> OrderDetails {  get; set; } 


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

            base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CategoryEntity>().HasData(
				new CategoryEntity { ID = 1, Name = "Action", DisplayOrder = 1 },
				new CategoryEntity { ID = 2, Name = "SciFi", DisplayOrder = 2 },
				new CategoryEntity { ID = 3, Name = "History", DisplayOrder = 3 },
				new CategoryEntity { ID = 4, Name = "Fairy Tail", DisplayOrder = 4 }
				);

			modelBuilder.Entity<ProductEntity>().HasData(
                new ProductEntity
                {
                    ID = 1,
                    Title = "Fortune of Time",
                    Author = "Billy Spark",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SWD9999001",
                    ListPrice = 99,
                    Price = 90,
                    Price50 = 85,
                    Price100 = 80,
					CategoryId = 1
				},
                new ProductEntity
                {
                    ID = 2,
                    Title = "Dark Skies",
                    Author = "Nancy Hoover",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
					CategoryId = 1

				},
                new ProductEntity
                {
                    ID = 3,
                    Title = "Vanish in the Sunset",
                    Author = "Julian Button",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
					CategoryId = 1
				},
                new ProductEntity
                {
                    ID = 4,
                    Title = "Cotton Candy",
                    Author = "Abby Muscles",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
					CategoryId = 3
				},
                new ProductEntity
                {
                    ID = 5,
                    Title = "Rock in the Ocean",
                    Author = "Ron Parker",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "SOTJ1111111101",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
					CategoryId = 4
				},
                new ProductEntity
                {
                    ID = 6,
                    Title = "Leaves and Wonders",
                    Author = "Laura Phantom",
                    Description = "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                    ISBN = "FOT000000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
					CategoryId = 1
				}
            );

            modelBuilder.Entity<CompanyEntity>().HasData(
            new CompanyEntity
            {
                Id = 1,
                Name = "SomeCompany",
                State = "Uk",
                City = "ZHmerenka",
                PhoneNumber = "+38097472917",
                PostalCode = "191",
                StreetAdress = "Gid 32"
            }
            );

        }
    }
}