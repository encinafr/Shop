﻿
namespace Shop.Web.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.AspNetCore.Identity;
    using Helpers;

    public class SeedDb
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {

            var user = await this.userHelper.GetUserByEmailAsync("encinajavier7@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Javier",
                    LastName = "Encina",
                    Email = "encinajavier7@gmail.com",
                    UserName = "encinajavier7@gmail.com",
                    PhoneNumber = "12348765"
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }

            if (!this.context.Products.Any())
            {
                this.AddProduct("First Product", user);
                this.AddProduct("Second Product", user);
                this.AddProduct("Third Product", user);
                await this.context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            this.context.Products.Add(new Product
            {
                Name = name,
                Price = this.random.Next(100),
                IsAvailabe = true,
                Stock = this.random.Next(100),
                User = user
            });
    
        }

        //private void AddProduct(string name)
        //{
        //    this.context.Products.Add(new Product
        //    {
        //        Name = name,
        //        Price = this.random.Next(1000),
        //        IsAvailabe = true,
        //        Stock = this.random.Next(100)
        //    });
        //}


    }

}
