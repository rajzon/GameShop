using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using FluentAssertions;
using GameShop.Application.Helpers;
using GameShop.Domain.Dtos;
using GameShop.Domain.Dtos.ProductDtos;
using GameShop.Domain.Model;
using GameShop.UI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using UnitTests.DataForTests;
using Xunit;

namespace UnitTests.Controllers
{
    //Info: unable to test DeleteProduct() - case when photo have PublicId from cloudinary
    //Add integration test for GetUsersWithRoles() method , for case when any roles are passed , user do not have any roles, user that have less roles then roles that was passed to method etc.
    // GetProductForEdit() - 1 cases to delete: photosFromRepo == null
    // Rename ProductRepository.CreateAsync() To ScaffoldProductForCreation
    // Rename ProductRepository.EditAsync() To ScaffoldProductForEdit

    //Check If statements for IEnumerable<T> in Controllers/Repository and add checking for empty list
    public class AdminControllerTest: UnitTestsBase , IDisposable
    {
        private readonly AdminController _cut;
        public AdminControllerTest()
        {
            var controllerContext = new ControllerContext() {
                HttpContext = new DefaultHttpContext()
            };

            _cut = new AdminController(_mockedUserManager.Object, _mapper, 
                            _cloudinaryConfig, _mockedUnitOfWork.Object)
            {
                ControllerContext = controllerContext
            }; 
        }


        [Fact]
        public void Given_None_When_GetUsersWithRoles_ThenReturn_OkStatusWithUsersList()
        {
            //Arrange
            _mockedUnitOfWork.Setup(s => s.User.GetAllWithRolesAsync())
                        .Returns(Task.FromResult<IEnumerable<UserForListDto>>(new List<UserForListDto>()));

            //Act
            var result = _cut.GetUsersWithRoles().Result;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull();

            _mockedUnitOfWork.Verify(v => v.User.GetAllWithRolesAsync(), Times.Once);
        }

        [Fact]
        public void Given_None_When_GetUsersWithRoles_ThenReturn_NotFound_BecauseUsersNotExistInDb()
        {
            //Arrange
            _mockedUnitOfWork.Setup(s => s.User.GetAllWithRolesAsync())
                        .Returns(Task.FromResult<IEnumerable<UserForListDto>>(null));

            //Act
            var result = _cut.GetUsersWithRoles().Result;

            //Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockedUnitOfWork.Verify(v => v.User.GetAllWithRolesAsync(), Times.Once);
        }


        [Fact]
        public void Given_UserNameThatDoNotHaveAnyRole_And_RoleEditDtoForUser_When_EditRoles_ThenReturn_OkStatusWithUserRoles()
        {
            //Arrange
            var userName = "TestUser";
            var roleEditDto = new RoleEditDto() 
            {
                RoleNames = new string[] {"Customer", "Moderator"}
            };

            var emptyRoleList = new List<string>();

            var expectedRoles = new List<string>();
            expectedRoles.AddRange(roleEditDto.RoleNames);

            var listOfRolesToAdd = roleEditDto.RoleNames.Except(emptyRoleList);

            _mockedUserManager.Setup(s => s.FindByNameAsync(userName))
                        .Returns(Task.FromResult(new User(){ UserName = userName}));

            _mockedUserManager.SetupSequence(s => s.GetRolesAsync(It.IsAny<User>()))
                        .Returns(Task.FromResult<IList<string>>(emptyRoleList))
                        .Returns(Task.FromResult<IList<string>>(expectedRoles));

            _mockedUserManager.Setup(s => s.AddToRolesAsync(It.IsAny<User>(), listOfRolesToAdd))
                        .Returns(Task.FromResult(IdentityResult.Success));

            _mockedUserManager.Setup(s => s.RemoveFromRolesAsync(It.IsAny<User>(), emptyRoleList))
                        .Returns(Task.FromResult(IdentityResult.Success));

            //Act
            var result = _cut.EditRoles(userName, roleEditDto).Result;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expectedRoles);

            _mockedUserManager.Verify(v => v.FindByNameAsync(userName), Times.Once);

            _mockedUserManager.Verify(v => v.GetRolesAsync(It.IsAny<User>()), Times.Exactly(2));

            _mockedUserManager.Verify(v => v.AddToRolesAsync(It.IsAny<User>(), listOfRolesToAdd), Times.Once);

            _mockedUserManager.Verify(v => v.RemoveFromRolesAsync(It.IsAny<User>(), emptyRoleList), Times.Once);
        }

        [Fact]
        public void Given_UserNameThatAlreadyHaveRolesThatWasSelectedByClientAndRoleEditDtoForUser_When_EditRoles_ThenReturn_OkStatusWithUsersRoles_UserRolesDidntChangedBecauseClientPassedSameRolesAsUserAlreadyHave()
        {
            //Arrange
            var userName = "TestUser";
            var roleEditDto = new RoleEditDto() 
            {
                RoleNames = new string[] {"Customer", "Moderator"}
            };
            var emptyRoleList = new List<string>();

            var roleListThatUserAlreadyHave = new List<string>();
            roleListThatUserAlreadyHave.AddRange(new string[] {"Customer", "Moderator"});

            var expectedRoles = new List<string>();
            expectedRoles.AddRange(roleEditDto.RoleNames);

            var listOfRolesToAdd = roleEditDto.RoleNames.Except(roleListThatUserAlreadyHave);

            _mockedUserManager.Setup(s => s.FindByNameAsync(userName))
                        .Returns(Task.FromResult(new User(){ UserName = userName}));

            _mockedUserManager.SetupSequence(s => s.GetRolesAsync(It.IsAny<User>()))
                        .Returns(Task.FromResult<IList<string>>(roleListThatUserAlreadyHave))
                        .Returns(Task.FromResult<IList<string>>(expectedRoles));

            _mockedUserManager.Setup(s => s.AddToRolesAsync(It.IsAny<User>(), listOfRolesToAdd))
                        .Returns(Task.FromResult(IdentityResult.Success));

            _mockedUserManager.Setup(s => s.RemoveFromRolesAsync(It.IsAny<User>(), emptyRoleList))
                        .Returns(Task.FromResult(IdentityResult.Success));

            //Act
            var result = _cut.EditRoles(userName, roleEditDto).Result;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expectedRoles);

            _mockedUserManager.Verify(v => v.FindByNameAsync(userName), Times.Once);

            _mockedUserManager.Verify(v => v.GetRolesAsync(It.IsAny<User>()), Times.Exactly(2));

            _mockedUserManager.Verify(v => v.AddToRolesAsync(It.IsAny<User>(), listOfRolesToAdd), Times.Once);

            _mockedUserManager.Verify(v => v.RemoveFromRolesAsync(It.IsAny<User>(), emptyRoleList), Times.Once);
        }

        [Fact]
        public void Given_UserNameAndRoleEditDtoForUserWithRoleThatUserDoNotHave_When_EditRoles_ThenReturn_OkStatusWithUsersRoles_Added1MoreRoleForUserAndKeptRolesThatUserAlreadyHave()
        {
            //Arrange
            var userName = "TestUser";
            var roleEditDto = new RoleEditDto() 
            {
                RoleNames = new string[] {"Customer", "Moderator", "Admin"}
            };
            var emptyRoleList = new List<string>();

            var roleListThatUserAlreadyHave = new List<string>();
            roleListThatUserAlreadyHave.AddRange(new string[] {"Customer", "Moderator"});

            var expectedRoles = new List<string>();
            expectedRoles.AddRange(roleEditDto.RoleNames);

            var listOfRolesToAdd = roleEditDto.RoleNames.Except(roleListThatUserAlreadyHave);

            _mockedUserManager.Setup(s => s.FindByNameAsync(userName))
                        .Returns(Task.FromResult(new User(){ UserName = userName}));

            _mockedUserManager.SetupSequence(s => s.GetRolesAsync(It.IsAny<User>()))
                        .Returns(Task.FromResult<IList<string>>(roleListThatUserAlreadyHave))
                        .Returns(Task.FromResult<IList<string>>(expectedRoles));

            _mockedUserManager.Setup(s => s.AddToRolesAsync(It.IsAny<User>(), listOfRolesToAdd))
                        .Returns(Task.FromResult(IdentityResult.Success));

            _mockedUserManager.Setup(s => s.RemoveFromRolesAsync(It.IsAny<User>(), emptyRoleList))
                        .Returns(Task.FromResult(IdentityResult.Success));

            //Act
            var result = _cut.EditRoles(userName, roleEditDto).Result;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(expectedRoles);

            _mockedUserManager.Verify(v => v.FindByNameAsync(userName), Times.Once);

            _mockedUserManager.Verify(v => v.GetRolesAsync(It.IsAny<User>()), Times.Exactly(2));

            _mockedUserManager.Verify(v => v.AddToRolesAsync(It.IsAny<User>(), listOfRolesToAdd), Times.Once);

            _mockedUserManager.Verify(v => v.RemoveFromRolesAsync(It.IsAny<User>(), emptyRoleList), Times.Once);
        }

        [Fact]
        public void Given_UserNameThatNotExistInDbAndRoleEditDtoForUser_When_EditRoles_ThenReturn_BadRequestWithMessage()
        {
            //Arrange
            var userName = "NotExistUser";
            var roleEditDto = new RoleEditDto() 
            {
                RoleNames = new string[] {"Customer", "Moderator"}
            };

            _mockedUserManager.Setup(s => s.FindByNameAsync(userName))
                        .Returns(Task.FromResult<User>(null));

            //Act
            var result = _cut.EditRoles(userName, roleEditDto).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("User not found");

            _mockedUserManager.Verify(v => v.FindByNameAsync(userName), Times.Once);

        }

        [Fact]
        public void Given_UserNameAndRoleEditDtoForUser_When_EditRoles_ThenReturn_BadRequestWithMessage_BecauseErrorOccuredDuringAddingRole()
        {
            //Arrange
            var userName = "TestUser";
            var roleEditDto = new RoleEditDto() 
            {
                RoleNames = new string[] {"Customer", "Moderator"}
            };

            _mockedUserManager.Setup(s => s.FindByNameAsync(userName))
                        .Returns(Task.FromResult<User>(new User() {UserName = userName}));

            _mockedUserManager.Setup(s => s.GetRolesAsync(It.IsAny<User>()))
                            .Returns(Task.FromResult<IList<string>>(new List<string>()));

            _mockedUserManager.Setup(s => s.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()))
                        .Returns(Task.FromResult(IdentityResult.Failed()));

            //Act
            var result = _cut.EditRoles(userName, roleEditDto).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Fail during adding roles to specified user");

            _mockedUserManager.Verify(v => v.FindByNameAsync(userName), Times.Once);
            _mockedUserManager.Verify(v => v.GetRolesAsync(It.IsAny<User>()), Times.Once);
            _mockedUserManager.Verify(v => v.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()), Times.Once);
            _mockedUserManager.Verify(v => v.RemoveFromRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()), Times.Never);
            
        }

        [Fact]
        public void Given_UserNameAndRoleEditDtoForUser_When_EditRoles_ThenReturn_BadRequestWithMessage_BecauseErrorOccuredDuringRemovingRole()
        {
            //Arrange
            var userName = "TestUser";
            var roleEditDto = new RoleEditDto() 
            {
                RoleNames = new string[] {"Customer", "Moderator"}
            };

            _mockedUserManager.Setup(s => s.FindByNameAsync(userName))
                        .Returns(Task.FromResult<User>(new User() {UserName = userName}));

            _mockedUserManager.Setup(s => s.GetRolesAsync(It.IsAny<User>()))
                            .Returns(Task.FromResult<IList<string>>(new List<string>()));

            _mockedUserManager.Setup(s => s.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()))
                        .Returns(Task.FromResult(IdentityResult.Success));

            _mockedUserManager.Setup(s => s.RemoveFromRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()))
                        .Returns(Task.FromResult(IdentityResult.Failed()));

            //Act
            var result = _cut.EditRoles(userName, roleEditDto).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Fail during removing roles to specified user");

            _mockedUserManager.Verify(v => v.FindByNameAsync(userName), Times.Once);
            _mockedUserManager.Verify(v => v.GetRolesAsync(It.IsAny<User>()), Times.Once);
            _mockedUserManager.Verify(v => v.AddToRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()), Times.Once);
            _mockedUserManager.Verify(v => v.RemoveFromRolesAsync(It.IsAny<User>(), It.IsAny<IEnumerable<string>>()), Times.Once);
            
        }


        [Fact]
        public void Given_ProductParamsFromQuery_When_GetProductsForModeration_ThenReturn_OkWithListOfProductForModerationDto()
        {
            //Arrange
            var productParams = new ProductParams();

            int pageNumber = productParams.PageNumber;
            int pageSize = productParams.PageSize;

            var productForModerationDto = Data.ProductForModerationDto(); 

            var expectedPaginationHeader = JsonConvert.SerializeObject(new {currentPage = pageNumber, itemsPerPage = pageSize, totalItems = productForModerationDto.Count, totalPages = 1});

            _mockedUnitOfWork.Setup(s => s.Product.GetProductsForModerationAsync(productParams))
                            .Returns(Task.FromResult(new PagedList<ProductForModerationDto>(productForModerationDto, productForModerationDto.Count, 
                                    pageNumber, pageSize)));
                                
           
            //Act
            var result = _cut.GetProductsForModeration(productParams).Result;

            //Assert
            result.Should().BeOfType<OkObjectResult>();

            _cut.HttpContext.Response.Headers["Pagination"].Should().BeEquivalentTo(expectedPaginationHeader);

            result.As<OkObjectResult>().Value.Should().NotBeNull();

            result.As<OkObjectResult>().Value.Should().BeOfType<List<ProductForModerationDto>>();

            _mockedUnitOfWork.Verify(v => v.Product.GetProductsForModerationAsync(productParams), Times.Once);
            
            
        }

        [Theory]
        [InlineData(0,0)]
        [InlineData(0,-1)]
        [InlineData(-1,0)]
        [InlineData(-1,-1)]
        public void Given_ProductParamsFromQueryThatAreLessThen1_When_GetProductsForModeration_ThenReturn_BadRequestWithMessage(int pageNumber, int pageSize)
        {
            //Arrange
            var productParams = new ProductParams()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };               
           
            //Act
            var result = _cut.GetProductsForModeration(productParams).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("PageNumber or PageSize is less then 1");
            
            _mockedUnitOfWork.Verify(v => v.Product.GetProductsForModerationAsync(productParams), Times.Never);
        }

        [Fact]
        public void Given_ProductParamsFromQuery_When_GetProductsForModeration_ThenReturn_NotFound()
        {
            //Arrange
            var productParams = new ProductParams();

            _mockedUnitOfWork.Setup(s => s.Product.GetProductsForModerationAsync(productParams))
                            .ReturnsAsync((PagedList<ProductForModerationDto>)(null));
           
            //Act
            var result = _cut.GetProductsForModeration(productParams).Result;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
            
            _mockedUnitOfWork.Verify(v => v.Product.GetProductsForModerationAsync(productParams), Times.Once);
        }

        [Fact]
        public void Given_ProductParamsFromQuery_When_GetProductsForModeration_ThenReturn_BadRequestWithMessage_BecausePaginationParametersNotSetProperly()
        {
            //Arrange
            var productParams = new ProductParams();
            int pageNumber = productParams.PageNumber;
            int pageSize = productParams.PageSize;

            var productForModerationDto = Data.ProductForModerationDto();

            var pagedListForMock = new PagedList<ProductForModerationDto>(productForModerationDto, 
                    productForModerationDto.Count, pageNumber, pageSize);

            _mockedUnitOfWork.Setup(s => s.Product.GetProductsForModerationAsync(productParams))
                            .Returns(Task.FromResult(pagedListForMock))
                            .Callback(() => 
                            {
                                pagedListForMock.PageSize = 0;
                                pagedListForMock.TotalCount = -1;
                                pagedListForMock.TotalPages = 0;
                                pagedListForMock.CurrentPage = 0;
                            });
           
            //Act
            var result = _cut.GetProductsForModeration(productParams).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Pagination parameters wasn't set properly");
            
            _mockedUnitOfWork.Verify(v => v.Product.GetProductsForModerationAsync(productParams), Times.Once);
        }


        [Fact]
        public void Given_ProductId_When_GetProductForEdit_ThenReturn_OkWithProductToEditDto()
        {
            //Arrange
            int productId = 1;

            _mockedUnitOfWork.Setup(s => s.Requirements.GetRequirementsForProductAsync(productId))
                            .ReturnsAsync(new RequirementsForEditDto());

            _mockedUnitOfWork.Setup(s => s.Photo.GetPhotosForProduct(productId))
                            .ReturnsAsync(new List<Photo>());

            _mockedUnitOfWork.Setup(s => s.Product.GetProductToEditAsync(It.IsAny<RequirementsForEditDto>(),
                                    It.IsAny<IEnumerable<Photo>>(),productId))
                            .ReturnsAsync(new ProductToEditDto());

           
            //Act
            var result = _cut.GetProductForEdit(productId).Result;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeOfType<ProductToEditDto>();
            result.As<OkObjectResult>().Value.Should().NotBeNull();

            _mockedUnitOfWork.Verify(s => s.Requirements.GetRequirementsForProductAsync(productId), Times.Once);

            _mockedUnitOfWork.Verify(s => s.Photo.GetPhotosForProduct(productId), Times.Once);

            _mockedUnitOfWork.Verify(s => s.Product.GetProductToEditAsync(It.IsAny<RequirementsForEditDto>(),
                                    It.IsAny<IEnumerable<Photo>>(),productId), Times.Once);
        }

        [Fact]
        public void Given_ProductId_When_GetProductForEdit_ThenReturn_BadRequestWithMessage_BecauseProductDoNotHaveRequirements()
        {
            //Arrange
            int productId = 1;

            _mockedUnitOfWork.Setup(s => s.Requirements.GetRequirementsForProductAsync(productId))
                            .ReturnsAsync((RequirementsForEditDto)(null));

           
            //Act
            var result = _cut.GetProductForEdit(productId).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Error occured during retrieving requirements for selected product");

            _mockedUnitOfWork.Verify(s => s.Requirements.GetRequirementsForProductAsync(productId), Times.Once);


        }

        [Fact]
        public void Given_ProductId_When_GetProductForEdit_ThenReturn_BadRequestWithMessage_BecauseProductToEditNotReturned()
        {
            //Arrange
            int productId = 1;

            _mockedUnitOfWork.Setup(s => s.Requirements.GetRequirementsForProductAsync(productId))
                            .ReturnsAsync(new RequirementsForEditDto());

            _mockedUnitOfWork.Setup(s => s.Photo.GetPhotosForProduct(productId))
                            .ReturnsAsync(new List<Photo>());

            _mockedUnitOfWork.Setup(s => s.Product.GetProductToEditAsync(It.IsAny<RequirementsForEditDto>(),
                                    It.IsAny<IEnumerable<Photo>>(),productId))
                            .ReturnsAsync((ProductToEditDto)(null));

           
            //Act
            var result = _cut.GetProductForEdit(productId).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Error occured during retrieving product");

            _mockedUnitOfWork.Verify(s => s.Requirements.GetRequirementsForProductAsync(productId), Times.Once);
            
            _mockedUnitOfWork.Verify(s => s.Photo.GetPhotosForProduct(productId), Times.Once);

            _mockedUnitOfWork.Verify(s => s.Product.GetProductToEditAsync(It.IsAny<RequirementsForEditDto>(),
                                    It.IsAny<IEnumerable<Photo>>(),productId), Times.Once);


        }


        [Fact]
        public void Given_ProductForCreationDto_When_CreateProduct_ThenReturn_CreatedAtRoute()
        {
            //Arrange
            var productForCreationDto = new ProductForCreationDto();

            _mockedUnitOfWork.Setup(s => s.Category.GetAsync(It.IsAny<int>()))
                                .ReturnsAsync(new Category());

            _mockedUnitOfWork.Setup(s => s.Product.CreateAsync(productForCreationDto, It.IsAny<Requirements>(), It.IsAny<Category>()))
                                .ReturnsAsync(new Product());

            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(true);


            //Act
            var result = _cut.CreateProduct(productForCreationDto).Result;

            //Assert
            result.Should().BeOfType<CreatedAtRouteResult>();
            result.As<CreatedAtRouteResult>().Value.Should().NotBeNull();

            _mockedUnitOfWork.Verify(v => v.Category.GetAsync(It.IsAny<int>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Product.CreateAsync(productForCreationDto, It.IsAny<Requirements>(), It.IsAny<Category>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Product.Add(It.IsAny<Product>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.SaveAsync(), Times.Once);


        }

        [Fact]
        public void Given_ProductForCreationDtoThatIsNull_When_CreateProduct_ThenReturn_BadRequestWithMessage()
        {
            //Arrange

            //Act
            var result = _cut.CreateProduct(null).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Sended null product");

        }

        [Fact]
        public void Given_ProductForCreationDto_When_CreateProduct_ThenReturn_BadRequestWithMessage_BecauseProductWasntScaffoldedProperly()
        {
            //Arrange
            var productForCreationDto = new ProductForCreationDto();

            _mockedUnitOfWork.Setup(s => s.Category.GetAsync(It.IsAny<int>()))
                                .ReturnsAsync(new Category());

            _mockedUnitOfWork.Setup(s => s.Product.CreateAsync(productForCreationDto, It.IsAny<Requirements>(), It.IsAny<Category>()))
                                .ReturnsAsync((Product)null);


            //Act
            var result = _cut.CreateProduct(productForCreationDto).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Something went wrong during creating Product");

            _mockedUnitOfWork.Verify(v => v.Category.GetAsync(It.IsAny<int>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Product.CreateAsync(productForCreationDto, It.IsAny<Requirements>(), It.IsAny<Category>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Product.Add(It.IsAny<Product>()), Times.Never);

            _mockedUnitOfWork.Verify(v => v.SaveAsync(), Times.Never);


        }

        [Fact]
        public void Given_ProductForCreationDto_When_CreateProduct_ThenReturn_BadRequestWithMessage_BecauseProductWasntSavedInDb()
        {
            //Arrange
            var productForCreationDto = new ProductForCreationDto();

            _mockedUnitOfWork.Setup(s => s.Category.GetAsync(It.IsAny<int>()))
                                .ReturnsAsync(new Category());

            _mockedUnitOfWork.Setup(s => s.Product.CreateAsync(productForCreationDto, It.IsAny<Requirements>(), It.IsAny<Category>()))
                                .ReturnsAsync(new Product());

            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(false);


            //Act
            var result = _cut.CreateProduct(productForCreationDto).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Error occured during Saving Product");

            _mockedUnitOfWork.Verify(v => v.Category.GetAsync(It.IsAny<int>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Product.CreateAsync(productForCreationDto, It.IsAny<Requirements>(), It.IsAny<Category>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Product.Add(It.IsAny<Product>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.SaveAsync(), Times.Once);


        }

        [Fact]
        public void Given_ProductIdAndProductEditDto_When_EditProduct_ThenReturn_CreatedAtRoute()
        {
            //Arrange
            int productId = 1;
            var productEditDto = new ProductEditDto();

            _mockedUnitOfWork.Setup(s => s.Product.GetAsync(productId))
                        .ReturnsAsync(new Product());

            _mockedUnitOfWork.Setup(s => s.Category.FindAsync(It.IsAny<Expression<Func<Category,bool>>>()))
                        .ReturnsAsync(new Category());

            _mockedUnitOfWork.Setup(s => s.Product.EditAsync(productId, productEditDto,It.IsAny<Requirements>(),
                             It.IsAny<Category>(), It.IsAny<Product>()))
                        .ReturnsAsync(new Product());

            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(true);

            //Act
            var result = _cut.EditProduct(productId, productEditDto).Result;

            //Assert
            result.Should().BeOfType<CreatedAtRouteResult>();

            _mockedUnitOfWork.Verify(v => v.Product.GetAsync(productId), Times.Exactly(2));

            _mockedUnitOfWork.Verify(v => v.Category.FindAsync(It.IsAny<Expression<Func<Category,bool>>>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Product.EditAsync(productId, productEditDto,It.IsAny<Requirements>(),
                             It.IsAny<Category>(), It.IsAny<Product>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.SaveAsync(), Times.Once);

        }

        [Fact]
        public void Given_ProductIdAndProductEditDtoThatIsNull_When_EditProduct_ThenReturn_BadRequestWithMessage()
        {
            //Arrange
            int productId = 1;

            //Act
            var result = _cut.EditProduct(productId, null).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Sended null product");

        }

        [Fact]
        public void Given_ProductIdThatNotExistInDbAndProductEditDto_When_EditProduct_ThenReturn_BadRequestWithMessage()
        {
            //Arrange
            int productId = 1;
            var productEditDto = new ProductEditDto();

            _mockedUnitOfWork.Setup(s => s.Product.GetAsync(productId)).ReturnsAsync((Product)null);

            //Act
            var result = _cut.EditProduct(productId, productEditDto).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be($"Product with Id:{productId} not found");

        }

        [Fact]
        public void Given_ProductIdAndProductEditDto_When_EditProduct_ThenReturn_BadRequestWithMessage_BecauseProductWasntSavedInDb()
        {
            //Arrange
            int productId = 1;
            var productEditDto = new ProductEditDto();

            _mockedUnitOfWork.Setup(s => s.Product.GetAsync(productId))
                        .ReturnsAsync(new Product());

            _mockedUnitOfWork.Setup(s => s.Category.FindAsync(It.IsAny<Expression<Func<Category,bool>>>()))
                        .ReturnsAsync(new Category());

            _mockedUnitOfWork.Setup(s => s.Product.EditAsync(productId, productEditDto,It.IsAny<Requirements>(),
                             It.IsAny<Category>(), It.IsAny<Product>()))
                        .ReturnsAsync(new Product());

            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(false);

            //Act
            var result = _cut.EditProduct(productId, productEditDto).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Fail occured during editing Product");

            _mockedUnitOfWork.Verify(v => v.Product.GetAsync(productId), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Category.FindAsync(It.IsAny<Expression<Func<Category,bool>>>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Product.EditAsync(productId, productEditDto,It.IsAny<Requirements>(),
                             It.IsAny<Category>(), It.IsAny<Product>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.SaveAsync(), Times.Once);

        }

        [Fact]
        public void Given_ProductId_When_DeleteProduct_ThenRetrun_NoContentStatus()
        {
            //Arrange
            int productId = 1;

            _mockedUnitOfWork.Setup(s => s.Product.GetAsync(productId))
            .ReturnsAsync(new Product()
            {
                Photos = new List<Photo>()
                {
                    new Photo()
                    {
                        Id = 1,
                    },
                    new Photo()
                    {
                        Id = 2,
                    },
                    new Photo()
                    {
                        Id = 3,
                    }
                }
            });

            _mockedUnitOfWork.Setup(s => s.Photo.Delete(It.IsAny<Photo>()));
            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(true);

            //Act
            var result = _cut.DeleteProduct(productId).Result;

            //Assert
            result.Should().BeOfType<NoContentResult>();


            _mockedUnitOfWork.Verify(v => v.Product.GetAsync(productId), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Photo.Delete(It.IsAny<Photo>()), Times.Exactly(3));

            _mockedUnitOfWork.Verify(v => v.Product.Delete(It.IsAny<Product>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.SaveAsync(), Times.Once);
        }

        [Fact]
        public void Given_ProductIdThatNotExistInDb_When_DeleteProduct_ThenRetrun_BadRequestWithMessage()
        {
            //Arrange
            int productId = 1;

            _mockedUnitOfWork.Setup(s => s.Product.GetAsync(productId))
                .ReturnsAsync((Product)null);

            //Act
            var result = _cut.DeleteProduct(productId).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Product for that Id doesn't exist");

            _mockedUnitOfWork.Verify(v => v.Product.GetAsync(productId), Times.Once);

        }

        [Fact]
        public void Given_ProductId_When_DeleteProduct_ThenRetrun_BadRequestWithMessage_BecauseErrorOccuredDuringSavingInDbThatProductWasDeleted()
        {
            //Arrange
            int productId = 1;

            _mockedUnitOfWork.Setup(s => s.Product.GetAsync(productId))
                .ReturnsAsync(new Product());

            _mockedUnitOfWork.Setup(s => s.Photo.Delete(It.IsAny<Photo>()));
            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(false);


            //Act
            var result = _cut.DeleteProduct(productId).Result;

            //Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            result.As<BadRequestObjectResult>().Value.Should().Be("Something went wrong during deleting product");

            _mockedUnitOfWork.Verify(v => v.Product.GetAsync(productId), Times.Once);

            _mockedUnitOfWork.Verify(v => v.Product.Delete(It.IsAny<Product>()), Times.Once);

            _mockedUnitOfWork.Verify(v => v.SaveAsync(), Times.Once);


        }

        [Fact]
        public void Given_ProductId_When_DeleteProduct_CheckingIfProductThatDoNotHavePhotos_NotThrowException()
        {
            //Arrange
            int productId = 1;

            _mockedUnitOfWork.Setup(s => s.Product.GetAsync(productId))
                .ReturnsAsync(new Product());

            _mockedUnitOfWork.Setup(s => s.Photo.Delete(It.IsAny<Photo>()));
            _mockedUnitOfWork.Setup(s => s.SaveAsync()).ReturnsAsync(true);


            //Act
            _cut.Invoking(a => a.DeleteProduct(productId)).Should().NotThrow();



        }

        [Fact]
        public void Given_None_When_GetCategories_ThenRetrun_OkStatusWithCategoriesToReturnDto()
        {
            //Arrange           
            _mockedUnitOfWork.Setup(s => s.Category.GetAllOrderedByAsync(It.IsAny<Expression<Func<Category,int>>>()))
                            .ReturnsAsync(new List<Category>() {new Category()});
            //Act
            var result = _cut.GetCategories().Result;


            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull();
            result.As<OkObjectResult>().Value.Should().BeOfType<List<CategoryToReturnDto>>();

            _mockedUnitOfWork.Verify(v => v.Category.GetAllOrderedByAsync(It.IsAny<Expression<Func<Category,int>>>()), Times.Once);



        }

        [Fact]
        public void Given_None_When_GetCategories_ThenRetrun_NotFound_Because_CategoriesFromDbIsNull()
        {
            //Arrange           
            _mockedUnitOfWork.Setup(s => s.Category.GetAllOrderedByAsync(It.IsAny<Expression<Func<Category,int>>>()))
                            .ReturnsAsync((IEnumerable<Category>)null);
            //Act
            var result = _cut.GetCategories().Result;


            //Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockedUnitOfWork.Verify(v => v.Category.GetAllOrderedByAsync(It.IsAny<Expression<Func<Category,int>>>()), Times.Once);

        }


        [Fact]
        public void Given_None_When_GetCategories_ThenRetrun_NotFound_Because_CategoriesFromDbAreEmpty()
        {
            //Arrange 
            var emptyCategoriesList = new List<Category>();

            _mockedUnitOfWork.Setup(s => s.Category.GetAllOrderedByAsync(It.IsAny<Expression<Func<Category,int>>>()))
                            .ReturnsAsync(emptyCategoriesList);
            //Act
            var result = _cut.GetCategories().Result;


            //Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockedUnitOfWork.Verify(v => v.Category.GetAllOrderedByAsync(It.IsAny<Expression<Func<Category,int>>>()), Times.Once);

        }

        [Fact]
        public void Given_None_When_GetSubCategories_ThenRetrun_OkStatusWithSubCategoriesToReturnDto()
        {
            //Arrange           
            _mockedUnitOfWork.Setup(s => s.SubCategory.GetAllAsync())
                            .ReturnsAsync(new List<SubCategory>() {new SubCategory()});
            //Act
            var result = _cut.GetSubCategories().Result;


            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull();
            result.As<OkObjectResult>().Value.Should().BeOfType<List<SubCategoryToReturnDto>>();

            _mockedUnitOfWork.Verify(v => v.SubCategory.GetAllAsync(), Times.Once);

        }

        [Fact]
        public void Given_None_When_GetSubCategories_ThenRetrun_NotFound_Because_SubCategoriesFromDbIsNull()
        {
            //Arrange           
            _mockedUnitOfWork.Setup(s => s.SubCategory.GetAllAsync())
                            .ReturnsAsync((IEnumerable<SubCategory>)null);
            //Act
            var result = _cut.GetSubCategories().Result;


            //Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockedUnitOfWork.Verify(v => v.SubCategory.GetAllAsync(), Times.Once);

        }

        [Fact]
        public void Given_None_When_GetSubCategories_ThenRetrun_NotFound_Because_SubCategoriesFromDbAreEmpty()
        {
            //Arrange
            var emptySubCategoriesList = new List<SubCategory>();

            _mockedUnitOfWork.Setup(s => s.SubCategory.GetAllAsync())
                            .ReturnsAsync(emptySubCategoriesList);
            //Act
            var result = _cut.GetSubCategories().Result;


            //Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockedUnitOfWork.Verify(v => v.SubCategory.GetAllAsync(), Times.Once);

        }

        [Fact]
        public void Given_None_When_GetLanguages_ThenRetrun_OkStatusWithLanguagesToReturnDto()
        {
            //Arrange           
            _mockedUnitOfWork.Setup(s => s.Language.GetAllOrderedByAsync(It.IsAny<Expression<Func<Language,int>>>()))
                            .ReturnsAsync(new List<Language>() {new Language()});
            //Act
            var result = _cut.GetLanguages().Result;


            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull();
            result.As<OkObjectResult>().Value.Should().BeOfType<List<LanguageToReturnDto>>();


            _mockedUnitOfWork.Verify(v => v.Language.GetAllOrderedByAsync(It.IsAny<Expression<Func<Language,int>>>()), Times.Once);

        }

        [Fact]
        public void Given_None_When_GetLanguages_ThenRetrun_NotFound_Because_LanguagesFromDbIsNull()
        {
            //Arrange           
            _mockedUnitOfWork.Setup(s => s.Language.GetAllOrderedByAsync(It.IsAny<Expression<Func<Language,int>>>()))
                            .ReturnsAsync((IEnumerable<Language>)null);
            //Act
            var result = _cut.GetLanguages().Result;


            //Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockedUnitOfWork.Verify(v => v.Language.GetAllOrderedByAsync(It.IsAny<Expression<Func<Language,int>>>()), Times.Once);

        }

        [Fact]
        public void Given_None_When_GetLanguages_ThenRetrun_NotFound_Because_LanguagesFromDbAreEmpty()
        {
            //Arrange
            var emptyLanguagesList = new List<Language>();

            _mockedUnitOfWork.Setup(s => s.Language.GetAllOrderedByAsync(It.IsAny<Expression<Func<Language,int>>>()))
                            .ReturnsAsync(emptyLanguagesList);
            //Act
            var result = _cut.GetLanguages().Result;


            //Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockedUnitOfWork.Verify(v => v.Language.GetAllOrderedByAsync(It.IsAny<Expression<Func<Language,int>>>()), Times.Once);

        }

        [Fact]
        public void Given_CategoryId_When_GetCategory_ThenReturn_OkStatusWithCategoryToReturnDto()
        {
            //Arrange
            var categoryId = 1;

            _mockedUnitOfWork.Setup(s => s.Category.GetAsync(categoryId)).ReturnsAsync(new Category());

            //Act
            var result = _cut.GetCategory(categoryId).Result;

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull();
            result.As<OkObjectResult>().Value.Should().BeOfType<CategoryToReturnDto>();


            _mockedUnitOfWork.Verify(v => v.Category.GetAsync(categoryId), Times.Once);
        }

        [Fact]
        public void Given_CategoryId_When_GetCategory_ThenReturn_NotFound()
        {
            //Arrange
            var categoryId = 1;

            _mockedUnitOfWork.Setup(s => s.Category.GetAsync(categoryId)).ReturnsAsync((Category)null);

            //Act
            var result = _cut.GetCategory(categoryId).Result;

            //Assert
            result.Should().BeOfType<NotFoundResult>();

            _mockedUnitOfWork.Verify(v => v.Category.GetAsync(categoryId), Times.Once);
        }
    }
}