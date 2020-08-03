using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;
using GameShop.UI.Controllers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace TestsLib
{
    public class Class1
    {
        [Fact]
        public  void Test1() 
        {

            //Assert
            var config = new Mock<IConfiguration>();
            var userManager = new Mock<UserManager<User>>(
                    new Mock<IUserStore<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<User>>().Object,
                    new IUserValidator<User>[0],
                    new IPasswordValidator<User>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<User>>>().Object);
            
                    
            var signInManager = new Mock<SignInManager<User>>(
                    userManager.Object,
                    new Mock<IHttpContextAccessor>().Object,
                    new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<ILogger<SignInManager<User>>>().Object,
                    new Mock<IAuthenticationSchemeProvider>().Object,
                    new Mock<IUserConfirmation<User>>().Object   
            );

            var userToCreate = new UserForRegisterDto 
            {
                Username = "Example",
                Email = "takiTam@example.pl",
                Password = "AlaMaKota123"
            };

            userManager.Setup(x=>x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            userManager.Setup(x => x.AddToRoleAsync(It.IsAny<User>(), It.Is<string>(x => x.Contains("Customer")))).Returns(Task.FromResult(IdentityResult.Success)).Verifiable();

            //var testtsts = signInManager.Object;
            
            var authController = new AuthController(config.Object, userManager.Object, signInManager.Object);


           
            
            //Act
            var result = authController.Register(userToCreate);


            //Assert
            Assert.IsNotType<BadRequestObjectResult>(result.Result);
            Assert.IsType<OkObjectResult>(result.Result);


            
            

        }
    }
}
