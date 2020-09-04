using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using FluentAssertions;
using GameShop.Application.Interfaces;
using GameShop.Domain.Dtos;
using GameShop.UI;
using GameShop.UI.Controllers;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using TestsLib.DataForTests;
using Xunit;

namespace TestsLib
{    //To do: add tests for SetMainPhoto() and for DeletePhoto()
    public class PhotosContollerTest : TestBase, IDisposable
    {

         public PhotosContollerTest()
         {
             
         }

        // [Theory]
        // [InlineData(1,1)]
        //  public void IntegrationTest_Given_ProductIdAndPhotoId_When_DeletePhoto_Then_Return_PhotoForReturnDtoWithOkStatus(int productId,int photoId)
        //  {
        //      //Arrange
        //       var photoBeforeRunningAct = _unitOfWork.Photo.GetAsync(photoId).Result;

        //      //Act
        //      var httpRespone = _client.DeleteAsync($"/api/admin/product/{productId}/photos/{photoId}").Result;

        //      var photoAfterRunningAct = _unitOfWork.Photo.GetAsync(photoId).Result;
           

        //     //Assert
        //       httpRespone.StatusCode.Should().Be(HttpStatusCode.NoContent);
        //       photoBeforeRunningAct.Should().NotBeNull();
        //       photoAfterRunningAct.Should().BeNull();

             
        //  }

        [Theory]
        [InlineData(1,1)]
        [InlineData(2,2)]
        [InlineData(7,7)]
        public void IntegrationTest_Given_ProductIdAndPhotoId_When_DeletePhoto_Then_Return_NoContentStatus(int productId,int photoId)
        {
            //Arrange 
            var httpContext = new DefaultHttpContext();

            var photoBeforeRunningAct = _unitOfWork.Photo.GetAsync(photoId).Result;

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new PhotosController(_mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.DeletePhoto(productId, photoId).Result;

            var photoAfterRunningAct = _unitOfWork.Photo.GetAsync(photoId).Result;
      

            //Assert
            result.Should().BeOfType(typeof(NoContentResult));
            photoBeforeRunningAct.Should().NotBeNull();
            photoAfterRunningAct.Should().BeNull();    
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(7)]
        public void IntegrationTest_Given_PhotoId_When_GetPhoto_Then_Return_PhotoForReturnDtoWithOkStatus(int photoId)
        {
            //Arrange 
            var httpContext = new DefaultHttpContext();

            var expected = Data.PhotoForReturnDto();

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new PhotosController(_mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.GetPhoto(photoId).Result;

      

            //Assert
            result.Should().BeOfType(typeof(OkObjectResult));
            result.As<OkObjectResult>().Value.As<PhotoForReturnDto>()
                    .Should()
                    .BeEquivalentTo(expected.First(p => p.Id == photoId));   
        }

        [Theory]
        [InlineData(8)]
        [InlineData(100)]
        [InlineData(1050)]
        public void IntegrationTest_Given_PhotoId_When_GetPhoto_Then_Return_NotFound(int photoId)
        {
            //Arrange 
            var httpContext = new DefaultHttpContext();

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new PhotosController(_mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.GetPhoto(photoId).Result;

      

            //Assert
            result.Should().BeOfType(typeof(NotFoundResult));
        }


        [Theory]
        [InlineData(1,2)]
        [InlineData(1,3)]
        [InlineData(7,1)]
        [InlineData(7,6)]
        public void IntegrationTest_Given_ProductIdAndPhotoIdThatAreNotRelated_When_SetMainPhoto_Then_Return_Unauthorized(int productId, int photoId)
        {
            //Arrange 
            var httpContext = new DefaultHttpContext();

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new PhotosController(_mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.SetMainPhoto(productId, photoId).Result;
  

            //Assert
            result.Should().BeOfType(typeof(UnauthorizedResult));
        }


        [Theory]
        [InlineData(1,1)]
        [InlineData(2,2)]
        [InlineData(5,5)]
        [InlineData(7,7)]
        public void IntegrationTest_Given_ProductIdAndPhotoIdThatIsAlreadyMainPhoto_When_SetMainPhoto_Then_Return_BadRequest(int productId, int photoId)
        {
            //Arrange 
            var httpContext = new DefaultHttpContext();

            var controllerContext = new ControllerContext() {
                HttpContext = httpContext
            };

            var sut = new PhotosController(_mapper, _cloudinaryConfig, _unitOfWork) 
            {
                ControllerContext = controllerContext
            };

            //Act
            var result = sut.SetMainPhoto(productId, photoId).Result;
  

            //Assert
            result.Should().BeOfType(typeof(BadRequestObjectResult));
            result.As<BadRequestObjectResult>().Value.Should().Be("This is already main photo");
        }

    }
}