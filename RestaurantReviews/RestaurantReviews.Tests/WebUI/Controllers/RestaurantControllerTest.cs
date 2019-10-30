using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantReviews.Library.Interfaces;
using RestaurantReviews.Library.Models;
using RestaurantReviews.WebUI.Controllers;
using RestaurantReviews.WebUI.Models;
using Xunit;

namespace RestaurantReviews.Tests.WebUI.Controllers
{
    public class RestaurantControllerTest
    {
        [Fact]
        public void DetailsShouldGetRestaurantIfExists()
        {
            // arrange
            var stubRepo = new RestaurantRepositoryStub();
            var controller = new RestaurantController(stubRepo);
            // should not test against the real repo - that would not be a unit test.

            // act
            var result = controller.Details(5);

            // assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);

            var restaurant = Assert.IsAssignableFrom<RestaurantViewModel>(viewResult.Model);

            Assert.Equal(5, restaurant.Id);
        }

        [Fact]
        public void EditShouldGetRestaurantIfExists()
        {
            // arrange
            var mockRepo = new Mock<IRestaurantRepository>();
            mockRepo.Setup(x => x.GetRestaurantById(It.IsAny<int>()))
                .Returns(new Restaurant { Id = 5, Name = "Abc" });
            var controller = new RestaurantController(mockRepo.Object);
            // should not test against the real repo - that would not be a unit test.

            // act
            var result = controller.Edit(5);

            // assert
            var viewResult = Assert.IsAssignableFrom<ViewResult>(result);

            var restaurant = Assert.IsAssignableFrom<RestaurantViewModel>(viewResult.Model);

            Assert.Equal(5, restaurant.Id);
        }
    }

    // this is too painful to do for every single test
    // so instead, we use "mocks" - Nuget has a library, Moq, to make them for us.
    public class RestaurantRepositoryStub : IRestaurantRepository
    {
        public Restaurant GetRestaurantById(int id)
        {
            return new Restaurant
            {
                Id = 5,
                Name = "Abc"
            };
        }

        public void AddRestaurant(Restaurant restaurant) => throw new NotImplementedException();
        public void AddReview(Review review, Restaurant restaurant = null) => throw new NotImplementedException();
        public void DeleteRestaurant(int restaurantId) => throw new NotImplementedException();
        public void DeleteReview(int reviewId) => throw new NotImplementedException();
        public IEnumerable<Restaurant> GetRestaurants(string search = null) => throw new NotImplementedException();
        public Review GetReviewById(int reviewId) => throw new NotImplementedException();
        public int RestaurantIdFromReviewId(int reviewId) => throw new NotImplementedException();
        public void Save() => throw new NotImplementedException();
        public void UpdateRestaurant(Restaurant restaurant) => throw new NotImplementedException();
        public void UpdateReview(Review review) => throw new NotImplementedException();
    }
}
