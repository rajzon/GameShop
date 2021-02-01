using System;
using System.Collections.Generic;
using FluentAssertions;
using GameShop.Domain.Model;
using GameShop.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTests.Controllers
{
    public class UsersControllerTest : UnitTestsBase, IDisposable
    {
        private readonly UsersController _cut;

        public UsersControllerTest()
        {
            _cut = new UsersController(_mockedUnitOfWork.Object, _mapper, _mockedUserManager.Object); 
        }

        [Fact]
        public void Given_None_When_GetUsers_ThenReturn_NotFound()
        {
            //Arrange
            var emptyUsersList = new List<User>();

            _mockedUnitOfWork.Setup(s => s.User.GetAllOrderedByAsync(u => u.Id))
                    .ReturnsAsync(emptyUsersList);
            //Act
            var result = _cut.GetUsers().Result;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Given_UserId_When_GetUser_ThenReturn_NotFound()
        {
            //Arrange
            int userId = 1;

            _mockedUnitOfWork.Setup(s => s.User.GetAsync(userId))
                    .ReturnsAsync((User)null);
            //Act
            var result = _cut.GetUserForAccInfo(userId).Result;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}