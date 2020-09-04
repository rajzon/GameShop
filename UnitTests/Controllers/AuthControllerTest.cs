using System;
using System.Threading.Tasks;
using FluentAssertions;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;
using GameShop.UI.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTests.Controllers
{

    //Info: Test For Register method - case when wrong user shape was passed is already tested in Integration test library

    //To Do: 1.Move GenerateJwtToken() to another class and mock it, and test it
    //       2. Make unit test for Login - case when logged in successfully
    public class AuthControllerTest : UnitTestsBase , IDisposable
    {


        [Fact]
        public void Given_AnyButPassedUserForRegisterDtoOnlyForCallMethod_When_Register_ThenRetrun_CreatedStatus()
        {
            //Arrange
            var userForRegisterDto = new UserForRegisterDto()
            {
                Username = "David",
                Email = "david.example@example.com",
                Password = "Password"
            };

            string customerRole = "Customer";

            _mockedUserManager.Setup(s => s.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                        .Returns(Task.FromResult(IdentityResult.Success));

            _mockedUserManager.Setup(s => s.AddToRoleAsync(It.IsAny<User>(), customerRole))
                        .Returns(Task.FromResult(IdentityResult.Success));

            var cut = new AuthController(_mockedConfig.Object, _mockedUserManager.Object, _mockedSignInManager.Object);

            //Act
            var result = cut.Register(userForRegisterDto).Result;
            


            //Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockedUserManager.Verify(v => v.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            _mockedUserManager.Verify(v => v.AddToRoleAsync(It.IsAny<User>(), customerRole), Times.Once);
        }

        [Fact]
        public void Given_AnyButPassedUserForRegisterDtoOnlyForCallMethod_When_Register_ThenRetrun_BadRequestSatus_BecauseFailOccuredDuringAddingRole()
        {
            //Arrange
            var userForRegisterDto = new UserForRegisterDto()
            {
                Username = "David",
                Email = "david.example@example.com",
                Password = "Password"
            };

            _mockedUserManager.Setup(s => s.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                        .Returns(Task.FromResult(IdentityResult.Success));

            _mockedUserManager.Setup(s => s.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                        .Returns(Task.FromResult(new IdentityResult()));

            var cut = new AuthController(_mockedConfig.Object, _mockedUserManager.Object, _mockedSignInManager.Object);

            //Act
            var result = cut.Register(userForRegisterDto).Result;
            


            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _mockedUserManager.Verify(v => v.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
            _mockedUserManager.Verify(v => v.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }


        [Fact]
        public void Given_UserForLoginDtoThatNotExist_When_Login_ThenRetrun_Unauthorized()
        {
            //Arrange
            var userForLoginDto = new UserForLoginDto()
            {
                Username = "UserThatNotExist",
                Password = "Password"
            };

            _mockedUserManager.Setup(s => s.FindByNameAsync(userForLoginDto.Username))
                        .Returns(Task.FromResult<User>(null));

            var cut = new AuthController(_mockedConfig.Object, _mockedUserManager.Object, _mockedSignInManager.Object);

            //Act
            var result = cut.Login(userForLoginDto).Result;
            


            //Assert
            result.Should().BeOfType<UnauthorizedResult>();

            _mockedUserManager.Verify(v => v.FindByNameAsync(userForLoginDto.Username), Times.Once);
            // _startup.mockedSignInManager.Verify(v => v.CheckPasswordSignInAsync(It.IsAny<User>(), userForLoginDto.Password, false), Times.Once);
        }

        [Fact]
        public void Given_UserForLoginDtoWithBadPassword_When_Login_ThenRetrun_Unauthorized()
        {
            //Arrange
            var userForLoginDto = new UserForLoginDto()
            {
                Username = "Holly",
                Password = "BadPassword"
            };

            _mockedUserManager.Setup(s => s.FindByNameAsync(userForLoginDto.Username))
                        .Returns(Task.FromResult<User>(new User(){UserName = userForLoginDto.Username}));

            _mockedSignInManager.Setup(s => s.CheckPasswordSignInAsync(It.IsAny<User>(), userForLoginDto.Password, false))
                        .Returns(Task.FromResult(Microsoft.AspNetCore.Identity.SignInResult.Failed));

            var cut = new AuthController(_mockedConfig.Object, _mockedUserManager.Object, _mockedSignInManager.Object);

            //Act
            var result = cut.Login(userForLoginDto).Result;
            


            //Assert
            result.Should().BeOfType<UnauthorizedResult>();

            _mockedUserManager.Verify(v => v.FindByNameAsync(userForLoginDto.Username), Times.Once);
            _mockedSignInManager.Verify(v => v.CheckPasswordSignInAsync(It.IsAny<User>(), userForLoginDto.Password, false), Times.Once);
        }


    }
}