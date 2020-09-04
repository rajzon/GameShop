using System.Linq;
using FluentAssertions;
using GameShop.Domain.Dtos;
using GameShop.Infrastructure.Repositories;
using TestsLib.DataForTests;
using Xunit;

namespace TestsLib
{
    public class RequirementsRepositoryTest : TestBase
    {
        public class GetRequirementsForProductAsync : TestBase
        {
            [Theory]
            [InlineData(1)]
            [InlineData(5)]
            [InlineData(7)]
            public void IntegrationTest_Given_ProductId_When_GetRequirementsForProductAsync_ThenReturn_RequirementsForEditDto(int productId)
            {
                //Arrange
                var expected = Data.RequirementsForEditDto().Skip(productId - 1).FirstOrDefault();

                var sut = new RequirementsRepository(_context);

                //Act
                var result = sut.GetRequirementsForProductAsync(productId).Result;

                //Assert
                result.Should().BeOfType<RequirementsForEditDto>();
                result.Should().BeEquivalentTo(expected);



            }

            [Theory]
            [InlineData(0)]
            [InlineData(10)]
            [InlineData(25)]
            [InlineData(53)]
            public void IntegrationTest_Given_ProductIdThatDoesntExistInDb_When_GetRequirementsForProductAsync_ThenReturn_Null(int productId)
            {
                //Arrange
                var sut = new RequirementsRepository(_context);

                //Act
                var result = sut.GetRequirementsForProductAsync(productId).Result;

                //Assert
                result.Should().BeNull();

            }  
        }
        
        
    }
}