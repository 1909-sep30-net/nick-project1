using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using RestaurantReviews.DataAccess.Entities;
using RestaurantReviews.DataAccess.Repositories;
using Xunit;

namespace RestaurantReviews.Tests.DataAccess.Repositories
{
    public class RestaurantRepositoryTest
    {
        [Fact]
        public void GetRestaurantByIdShouldReturnResult()
        {
            // arrange
            var options = new DbContextOptionsBuilder<RestaurantReviewsDbContext>()
                .UseInMemoryDatabase("GetRestaurantByIdShouldReturnResult")
                .Options;
            using var arrangeContext = new RestaurantReviewsDbContext(options);
            var id = 5;
            arrangeContext.Restaurant.Add(new Restaurant { Id = id, Name = "Abc" });
            arrangeContext.SaveChanges();

            using var actContext = new RestaurantReviewsDbContext(options);
            var repo = new RestaurantRepository(actContext, new NullLogger<RestaurantRepository>());

            // act
            var result = repo.GetRestaurantById(id);

            // assert
            // if I needed to check the actual database here, i would use a separate assertContext as well.
            Assert.NotNull(result);
        }

        [Fact]
        public void AddRestaurantShouldAdd()
        {
            // arrange
            var options = new DbContextOptionsBuilder<RestaurantReviewsDbContext>()
                .UseInMemoryDatabase("AddRestaurantShouldAdd")
                .Options;
            var name = "Abc";
            var restaurant = new RestaurantReviews.Library.Models.Restaurant { Id = 5, Name = name };

            using var actContext = new RestaurantReviewsDbContext(options);
            var repo = new RestaurantRepository(actContext, new NullLogger<RestaurantRepository>());

            // act
            repo.AddRestaurant(restaurant);
            repo.Save();

            // assert
            using var assertContext = new RestaurantReviewsDbContext(options);
            var rest = assertContext.Restaurant.First(r => r.Name == name);
            Assert.NotNull(rest);
        }
    }
}
