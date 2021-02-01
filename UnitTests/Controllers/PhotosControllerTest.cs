using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FluentAssertions;
using GameShop.Domain.Dtos;
using GameShop.Domain.Model;
using GameShop.UI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTests.Controllers
{

    //TODO: change tests for adding photo

    //Info: AddPhotoForProduct() not tested uploading image to cloud
    // DeletePhoto() not tested deleting image from cloud
    public class PhotosControllerTest : UnitTestsBase, IDisposable
    {
        private readonly PhotosController _cut;

        public PhotosControllerTest()
        {
            _cut = new PhotosController(_mapper, _cloudinaryConfig, _mockedUnitOfWork.Object, _mockedAddPhotoToCloud.Object);
        }

        [Fact]
        public void Given_PhotoId_When_GetPhoto_ThenReturn_OkStatusWithPhotoForReturnDto()
        {
            //Arrange
            var photoId = 1;

            _mockedUnitOfWork.Setup(s => s.Photo.GetAsync(photoId)).ReturnsAsync(new Photo());

            //Act
            var result = _cut.GetPhoto(photoId).Result;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull();
            result.As<OkObjectResult>().Value.Should().BeOfType<PhotoForReturnDto>();

            _mockedUnitOfWork.Verify(v => v.Photo.GetAsync(photoId), Times.Once);
        }

        [Fact]
        public void Given_PhotoId_When_GetPhoto_ThenReturn_NotFound()
        {
            //Arrange
            var photoId = 1;

            _mockedUnitOfWork.Setup(s => s.Photo.GetAsync(photoId)).ReturnsAsync((Photo)null);

            //Act
            var result = _cut.GetPhoto(photoId).Result;

            //Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockedUnitOfWork.Verify(v => v.Photo.GetAsync(photoId), Times.Once);
        }


        [Fact]
        public void Given_ProductIdAndPhotoForCreationWithoutImage_When_AddPhotoForProduct_ThenReturn_BadRequest_BecausePhotoFileWasntSent()
        {
            //Arrange
            int productId = 1;
            var photoForCreationDto = new PhotoForCreationDto();

            _mockedUnitOfWork.Setup(s => s.Product.GetWithPhotosOnly(productId))
                    .ReturnsAsync(new Product());
            

            //Act
            var result = _cut.AddPhotoForProduct(productId, photoForCreationDto).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Photo file wasn't sent");

            _mockedUnitOfWork.Verify(v => v.Product.GetWithPhotosOnly(productId), Times.Once);
            
        }

        [Fact]
        public void Given_ProductIdAndPhotoForCreation_When_AddPhotoForProduct_ThenReturn_BadRequest_BecauseErrorOccuredDuringSavingPhoto()
        {
            //Arrange

            int productId = 1;
            var photoForCreationDto = new PhotoForCreationDto(){File = _mockedFormFile.Object};


            _mockedUnitOfWork.Setup(s => s.Product.GetWithPhotosOnly(productId))
                    .ReturnsAsync(new Product(){Photos = new List<Photo>()});

            _mockedAddPhotoToCloud.Setup(s => s.Do(It.IsAny<Cloudinary>(),photoForCreationDto)).Returns(true);

            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(false);
            

            //Act
            var result = _cut.AddPhotoForProduct(productId, photoForCreationDto).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be($"Could not add photo for product: {productId}");

            _mockedUnitOfWork.Verify(v => v.Product.GetWithPhotosOnly(productId), Times.Once);
            _mockedUnitOfWork.Verify(v => v.SaveAsync(), Times.Once);
            
        }

        [Fact]
        public void Given_ProductIdAndPhotoForCreation_When_AddPhotoForProduct_CaseWhenProductDoNotHaveMainPhoto_ThenVerifyIfPhotoWasSetAsMain()
        {
            //Arrange

            int productId = 1;
            var photoForCreationDto = new PhotoForCreationDto(){File = _mockedFormFile.Object};
            var emptyPhotoList = new List<Photo>();

            _mockedUnitOfWork.Setup(s => s.Product.GetWithPhotosOnly(productId))
                    .ReturnsAsync(new Product(){Photos = emptyPhotoList});

            _mockedAddPhotoToCloud.Setup(s => s.Do(It.IsAny<Cloudinary>(),photoForCreationDto)).Returns(true);

            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(true);
            

            //Act
            var result = _cut.AddPhotoForProduct(productId, photoForCreationDto).Result;

            //Assert
            result.Should().BeOfType<CreatedAtRouteResult>();
            result.As<CreatedAtRouteResult>().Value.As<PhotoForReturnDto>().isMain.Should().BeTrue();

            _mockedUnitOfWork.Verify(v => v.Product.GetWithPhotosOnly(productId), Times.Once);
            _mockedUnitOfWork.Verify(v => v.SaveAsync(), Times.Once);
            
        }

        [Fact]
        public void Given_ProductIdAndPhotoForCreation_When_AddPhotoForProduct_CaseWhenProductAlreadyHaveMainPhoto_ThenVerifyIfPhotoWasntSetAsMain()
        {
            //Arrange

            int productId = 1;
            var photoForCreationDto = new PhotoForCreationDto(){File = _mockedFormFile.Object};
            var productPhotoList = new List<Photo>(){new Photo(){isMain = true}};

            _mockedUnitOfWork.Setup(s => s.Product.GetWithPhotosOnly(productId))
                    .ReturnsAsync(new Product(){Photos = productPhotoList});
            
            _mockedAddPhotoToCloud.Setup(s => s.Do(It.IsAny<Cloudinary>(),photoForCreationDto)).Returns(true);

            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(true);
            

            //Act
            var result = _cut.AddPhotoForProduct(productId, photoForCreationDto).Result;

            //Assert
            result.Should().BeOfType<CreatedAtRouteResult>();
            result.As<CreatedAtRouteResult>().Value.As<PhotoForReturnDto>().isMain.Should().BeFalse();

            _mockedUnitOfWork.Verify(v => v.Product.GetWithPhotosOnly(productId), Times.Once);
            _mockedUnitOfWork.Verify(v => v.SaveAsync(), Times.Once);
            
        }

        [Fact]
        public void Given_ProductIdAndPhotoForCreation_When_AddPhotoForProduct_ThenReturn_CreatedStatusWithRouteToPhotoForReturnDtoValue()
        {
            //Arrange

            int productId = 1;
            int exptectedPhotoId = 0;
            var photoForCreationDto = new PhotoForCreationDto(){File = _mockedFormFile.Object};
            var productPhotoList = new List<Photo>(){new Photo(){isMain = true}};

            _mockedUnitOfWork.Setup(s => s.Product.GetWithPhotosOnly(productId))
                    .ReturnsAsync(new Product(){Photos = productPhotoList});

            _mockedAddPhotoToCloud.Setup(s => s.Do(It.IsAny<Cloudinary>(),photoForCreationDto)).Returns(true);

            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(true);
            

            //Act
            var result = _cut.AddPhotoForProduct(productId, photoForCreationDto).Result;

            //Assert
            result.Should().BeOfType<CreatedAtRouteResult>();

            result.As<CreatedAtRouteResult>().Value.Should().BeOfType<PhotoForReturnDto>();

            result.As<CreatedAtRouteResult>().Value.Should().NotBeNull();

            result.As<CreatedAtRouteResult>().RouteValues.Values.First().Should().Be(productId);
            result.As<CreatedAtRouteResult>().RouteValues.Values.Skip(1).First().Should().Be(exptectedPhotoId);

            _mockedUnitOfWork.Verify(v => v.Product.GetWithPhotosOnly(productId), Times.Once);
            _mockedUnitOfWork.Verify(v => v.SaveAsync(), Times.Once);
            
        }

        [Fact]
        public void Given_ProductIdAndPhotoId_When_SetMainPhoto_CaseWhenPhotoIsNotMain_ThenReturn_NoContent()
        {
            //Arrange
            int productId = 1;
            int photoId = 2;
            
            int photoIdOfCurrentMainPhoto = 1;

            _mockedUnitOfWork.Setup(s => s.Product.GetWithPhotosOnly(productId))
                                .ReturnsAsync(new Product() {Photos = new List<Photo>()
                                {
                                    new Photo(){Id = photoIdOfCurrentMainPhoto, isMain = true}, 
                                    new Photo(){Id = photoId}
                                }});
            
            _mockedUnitOfWork.Setup(s => s.Photo.GetAsync(photoId))
                                .ReturnsAsync(new Photo(){Id = photoId, isMain = false});
            
            _mockedUnitOfWork.Setup(s => s.Photo.FindAsync(p => p.ProductId == productId && p.isMain == true))
                                .ReturnsAsync(new Photo(){Id = photoIdOfCurrentMainPhoto, isMain = true});
            
            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(true);
            

            //Act
            var result = _cut.SetMainPhoto(productId, photoId).Result;

            //Assert
            result.Should().BeOfType<NoContentResult>();

            _mockedUnitOfWork.Verify(v => v.Product.GetWithPhotosOnly(productId), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Photo.GetAsync(photoId), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Photo.FindAsync(p => p.ProductId == productId && p.isMain == true), Times.Once);

            _mockedUnitOfWork.Verify(v => v.SaveAsync(), Times.Once);
 
            
        }

        [Fact]
        public void Given_ProductIdAndPhotoIdThatProductDoNotHave_When_SetMainPhoto_ThenReturn_BadRequestWithMessage()
        {
            //Arrange
            int productId = 1;
            int photoIdThatProductDoNotHave = 2;

            _mockedUnitOfWork.Setup(s => s.Product.GetWithPhotosOnly(productId))
                                .ReturnsAsync(new Product() {Photos = new List<Photo>()
                                {
                                    new Photo(){Id = 1}
                                }});
        
            

            //Act
            var result = _cut.SetMainPhoto(productId, photoIdThatProductDoNotHave).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Trying to change photo that do not exists for that product");


            _mockedUnitOfWork.Verify(v => v.Product.GetWithPhotosOnly(productId), Times.Once);
            
        }

        [Fact]
        public void Given_ProductIdAndPhotoIdThatIsAlreadyMainPhoto_When_SetMainPhoto_ThenReturn_BadRequestWithMessage()
        {
            //Arrange
            int productId = 1;
            int photoId = 2;

            var photoFromProductThatIsAlreadyMainPhoto = new Photo(){Id = photoId, isMain = true};

            _mockedUnitOfWork.Setup(s => s.Product.GetWithPhotosOnly(productId))
                                .ReturnsAsync(new Product() {Photos = new List<Photo>()
                                {
                                    photoFromProductThatIsAlreadyMainPhoto
                                }});
            
            _mockedUnitOfWork.Setup(s => s.Photo.GetAsync(photoId))
                        .ReturnsAsync(photoFromProductThatIsAlreadyMainPhoto);
        

            //Act
            var result = _cut.SetMainPhoto(productId, photoId).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("This is already main photo");


            _mockedUnitOfWork.Verify(v => v.Product.GetWithPhotosOnly(productId), Times.Once);
            _mockedUnitOfWork.Verify(v => v.Photo.GetAsync(photoId), Times.Once);
            
        }

        [Fact]
        public void Given_ProductIdAndPhotoId_When_SetMainPhoto_ThenReturn_BadRequestWithMessage_BecauseErrorOccuredDuringSaving()
        {
            //Arrange
            int productId = 1;
            int photoId = 2;

            _mockedUnitOfWork.Setup(s => s.Product.GetWithPhotosOnly(productId))
                                .ReturnsAsync(new Product() {Photos = new List<Photo>()
                                {
                                    new Photo(){Id = photoId}
                                }});
            
            _mockedUnitOfWork.Setup(s => s.Photo.GetAsync(photoId))
                        .ReturnsAsync(new Photo());

            _mockedUnitOfWork.Setup(s => s.Photo.FindAsync(p => p.ProductId == productId && p.isMain == true))
                        .ReturnsAsync(new Photo());

            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(false);
        

            //Act
            var result = _cut.SetMainPhoto(productId, photoId).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Could not save photo as main photo");


            _mockedUnitOfWork.Verify(v => v.Product.GetWithPhotosOnly(productId), Times.Once);
            _mockedUnitOfWork.Verify(v => v.Photo.GetAsync(photoId), Times.Once);
            _mockedUnitOfWork.Verify(v => v.Photo.FindAsync(p => p.ProductId == productId && p.isMain == true), Times.Once);
            _mockedUnitOfWork.Verify(v => v.SaveAsync(), Times.Once);
            
        }


        [Fact]
        public void Given_ProductIdAndPhotoId_When_DeletePhoto_ThenReturn_NoContent()
        {
            //Arrange
            int productId = 1;
            int photoId = 2;

            var photoFromProductThatIsSameAsPassedOne = new Photo(){Id = 2}; 

            _mockedUnitOfWork.Setup(s => s.Product.GetWithPhotosOnly(productId))
                                .ReturnsAsync(new Product() {Photos = new List<Photo>()
                                {
                                    photoFromProductThatIsSameAsPassedOne
                                }});

            _mockedUnitOfWork.Setup(s => s.Photo.GetAsync(photoId)).ReturnsAsync(new Photo(){Id = photoId});
        
            _mockedUnitOfWork.Setup(s => s.Photo.Delete(It.IsAny<Photo>()));

            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(true);

            //Act
            var result = _cut.DeletePhoto(productId, photoId).Result;

            //Assert
            result.Should().BeOfType<NoContentResult>();
            
            _mockedUnitOfWork.Verify(v => v.Product.GetWithPhotosOnly(productId), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Photo.GetAsync(photoId), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Photo.Delete(It.IsAny<Photo>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.SaveAsync(), Times.Once);
        }

        [Fact]
        public void Given_ProductIdAndPhotoIdThatNotExistForThatProduct_When_DeletePhoto_ThenReturn_Unauthorized()
        {
            //Arrange
            int productId = 1;
            int photoId = 2;
            int differentPhotoIdThenThePassedOne = 3;

            _mockedUnitOfWork.Setup(s => s.Product.GetWithPhotosOnly(productId))
                                .ReturnsAsync(new Product() {Photos = new List<Photo>()
                                {
                                    new Photo(){Id = differentPhotoIdThenThePassedOne}
                                }});

            //Act
            var result = _cut.DeletePhoto(productId, photoId).Result;

            //Assert
            result.Should().BeOfType<UnauthorizedResult>();
            
            _mockedUnitOfWork.Verify(v => v.Product.GetWithPhotosOnly(productId), Times.Once);

        }

        [Fact]
        public void Given_ProductIdAndPhotoId_When_DeletePhoto_CaseWhenPhotoForDeleteIsFromCloud_VerifyIfPhotoDeleteMethodWasntCalled_BecauseResultFromCloudinaryIsNotOk()
        {
            //Arrange
            int productId = 1;
            int photoId = 2;

            var photoFromProductThatIsSameAsPassedOne = new Photo(){Id = photoId, PublicId="Placeholder PublicId"};

            _mockedUnitOfWork.Setup(s => s.Product.GetWithPhotosOnly(productId))
                                .ReturnsAsync(new Product() {Photos = new List<Photo>()
                                {
                                    photoFromProductThatIsSameAsPassedOne
                                }});

            _mockedUnitOfWork.Setup(s => s.Photo.GetAsync(photoId)).ReturnsAsync(new Photo(){Id = photoId, PublicId="Placeholder PublicId"});
            
            _mockedUnitOfWork.Setup(s => s.Photo.Delete(It.IsAny<Photo>()));

            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(false);


            //Act
            var result = _cut.DeletePhoto(productId, photoId).Result;

            //Assert

            _mockedUnitOfWork.Verify(v => v.Photo.Delete(It.IsAny<Photo>()), Times.Never);
        }

        [Fact]
        public void Given_ProductIdAndPhotoId_When_DeletePhoto_CaseWhenPhotoForDeleteIsFromCloud_ThenRetun_BadRequestWithMessage_BecauseErrorOccuredDuringSaving()
        {
            //Arrange
            int productId = 1;
            int photoId = 2;

            var photoFromProductThatIsSameAsPassedOne = new Photo(){Id = photoId, PublicId="Placeholder PublicId"};

            _mockedUnitOfWork.Setup(s => s.Product.GetWithPhotosOnly(productId))
                                .ReturnsAsync(new Product() {Photos = new List<Photo>()
                                {
                                    photoFromProductThatIsSameAsPassedOne
                                }});

            _mockedUnitOfWork.Setup(s => s.Photo.GetAsync(photoId)).ReturnsAsync(new Photo(){Id = photoId, PublicId="Placeholder PublicId"});
            
            _mockedUnitOfWork.Setup(s => s.Photo.Delete(It.IsAny<Photo>()));

            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(false);


            //Act
            var result = _cut.DeletePhoto(productId, photoId).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Failed to delete the photo");

            _mockedUnitOfWork.Verify(v => v.Photo.Delete(It.IsAny<Photo>()), Times.Never);
        }
    }
}