using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FluentAssertions;
using GameShop.Domain.Model;
using GameShop.Infrastructure.Repositories;
using TestsLib.DataForTests;
using Xunit;

namespace TestsLib
{

    // TO DO: 
    //1.Add more photos on SeedData for concrete product to be able to test funcionality of getting multiple photos for that product 
    public class PhotoRepositoryTest : TestBase
    {
        private readonly PhotoRepository _sut;

        public PhotoRepositoryTest()
        {
            _sut = new PhotoRepository(_context);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(7)]
        public void IntegrationTest_Given_ProductId_When_GetPhotosForProduct_ThenReturn_OneElementPhotoList(int productId)
        {
            //Arrange
            var expected = Data.Photo().Where(p => p.ProductId == productId).ToList();
            

            //Act
            var result = _sut.GetPhotosForProduct(productId).Result;

            //Assert
            result.Should().NotBeEmpty();
            result.Should().BeOfType<List<Photo>>();
            result.Should().BeEquivalentTo(expected);
            result.Count().Should().Be(1);
            

            
        }


        [Theory]
        [InlineData(10)]
        [InlineData(23)]
        [InlineData(50)]
        public void IntegrationTest_Given_ProductIdThatDoesntExistInDb_When_GetPhotosForProduct_ThenReturn_Null(int productId)
        {
            //Arrange

            //Act
            var result = _sut.GetPhotosForProduct(productId).Result;

            //Assert
            result.Should().BeEmpty();
            result.Should().BeOfType<List<Photo>>();

            

            
        }
    }
}