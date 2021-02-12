using System;
using System.Linq;
using FluentAssertions;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;
using GameShop.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using TestsLib.DataForTests;
using Xunit;

namespace TestsLib
{    //Info: Not tested AddPhotoForProduct()
    public class PhotosContollerTest : TestBase, IDisposable
    {

        private readonly PhotosController _sut;
         public PhotosContollerTest()
         {
             _sut = new PhotosController(_mapper, _cloudinaryConfig, _unitOfWork, _addPhotoToCloud);
         }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(7)]
        public void IntegrationTest_Given_PhotoId_When_GetPhoto_Then_Return_PhotoForReturnDtoWithOkStatus(int photoId)
        {
            //Arrange
            var expected = Data.PhotoForReturnDto();

            //Act
            var result = _sut.GetPhoto(photoId).Result;

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

            //Act
            var result = _sut.GetPhoto(photoId).Result;

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
            //Act
            var result = _sut.SetMainPhoto(productId, photoId).Result;
  

            //Assert
            result.Should().BeOfType(typeof(BadRequestObjectResult));
            result.As<BadRequestObjectResult>().Value.Should().Be("Trying to change photo that do not exists for that product");
        }


        [Theory]
        [InlineData(1,1)]
        [InlineData(2,2)]
        [InlineData(5,5)]
        [InlineData(7,7)]
        public void IntegrationTest_Given_ProductIdAndPhotoIdThatIsAlreadyMainPhoto_When_SetMainPhoto_Then_Return_BadRequest(int productId, int photoId)
        {

            //Act
            var result = _sut.SetMainPhoto(productId, photoId).Result;
  

            //Assert
            result.Should().BeOfType(typeof(BadRequestObjectResult));
            result.As<BadRequestObjectResult>().Value.Should().Be("This is already main photo");
        }

        [Fact]
        public void IntegrationTest_Given_ProductIdAndPhotoId_When_SetMainPhoto_Then_Return_NoContent_CheckedIfPreviousMainPhotoIsMainPropertyIsSetToFalse_And_CurrentMainPhotoIsMainPropertyIsSetToTrue()
        {
            //Arrange
            int productId = 2;
            int photoId = 8; 

            var additionalPhotoForTesting = new Photo()
            {
                 Id = photoId,
                 Url = "http://placehold.it/200x300.jpg",
                 isMain = false,
                 DateAdded = DateTime.Parse("2020-07-17"),
                 ProductId = productId
            };

            _unitOfWork.Photo.Add(additionalPhotoForTesting);


            //Act
            var result = _sut.SetMainPhoto(productId, photoId).Result;

            var previousMainPhoto = _context.Photos.Where(p => p.ProductId == productId && p.Id != photoId)
                        .FirstOrDefault();     
            var currentMainPhoto = _context.Photos.Where(p => p.Id == photoId).FirstOrDefault();

            //Assert
            result.Should().BeOfType<NoContentResult>();

            currentMainPhoto.isMain.Should().BeTrue();

            previousMainPhoto.isMain.Should().BeFalse();

            
        }

        [Theory]
        [InlineData(1,1)]
        [InlineData(2,2)]
        [InlineData(7,7)]
        public void IntegrationTest_Given_ProductIdAndPhotoId_When_DeletePhoto_Then_Return_NoContentStatus(int productId,int photoId)
        {
            //Arrange
            var photoBeforeRunningAct = _unitOfWork.Photo.GetAsync(photoId).Result;

            //Act
            var result = _sut.DeletePhoto(productId, photoId).Result;

            var photoAfterRunningAct = _unitOfWork.Photo.GetAsync(photoId).Result;
      

            //Assert
            result.Should().BeOfType(typeof(NoContentResult));
            photoBeforeRunningAct.Should().NotBeNull();
            photoAfterRunningAct.Should().BeNull();    
        }

    }
}