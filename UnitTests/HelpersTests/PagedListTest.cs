using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using GameShop.Application.Helpers;
using GameShop.Domain.Model;
using Microsoft.EntityFrameworkCore;
using UnitTests.DataForTests;
using Xunit;

namespace UnitTests.HelpersTests
{

    //Info: Not tested!!
    public class PagedListTest : UnitTestsBase, IDisposable
    {


        public Task<PagedList<T>> PagedListHelperTest<T>(IQueryable<T> source,int pageNumber, int pageSize)
        {
            return PagedList<T>.CreateAsync(source, pageNumber, pageSize);
        }


        // TO DO: change source of expected from dbContext to concrete Data
        [Theory]
        [InlineData(1,1)]
        public void Given_IQueryableDataPageNumberPageSize_When_CreateAsync_ThenReturn_PagedList(int pageNumber, int pageSize)
        {
            //Arrange
            var products = _context.Products.Include(l => l.Languages)
                                    .ThenInclude(productLanguage => productLanguage.Language)
                                .Include(p => p.Photos)
                                .Include(p => p.Category)
                                .Include(p => p.SubCategories)
                                    .ThenInclude(productSubCategory => productSubCategory.SubCategory)
                                .Include(p => p.Requirements);

            var expected = products.Skip((pageNumber -1) * pageSize).Take(pageSize);
            //Act
            var result = this.PagedListHelperTest<Product>(products, pageNumber, pageSize).Result;

            //Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}