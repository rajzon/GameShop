using System;
using System.IO;
using System.IO.Abstractions;
using AutoMapper;
using CloudinaryDotNet;
using GameShop.Application.Helpers;
using GameShop.Application.Interfaces;
using GameShop.Application.Mappings;
using GameShop.Domain.Model;
using GameShop.Infrastructure;
using GameShop.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace UnitTests
{
    public abstract class UnitTestsBase : IDisposable
    {
        protected readonly Mock<IConfiguration> _mockedConfig;
        protected readonly Mock<UserManager<User>> _mockedUserManager;
        protected readonly Mock<SignInManager<User>> _mockedSignInManager;
        protected readonly Mock<RoleManager<Role>> _mockedRoleManager;


        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly Mock<IJwtTokenHelper> _mockedJwtTokenHelper;
        protected readonly UnitOfWork _unitOfWork;
        protected readonly Mock<IUnitOfWork> _mockedUnitOfWork;
        protected readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        protected readonly Mock<IFormFile> _mockedFormFile;

        public UnitTestsBase()
        {
            // Db Configuration
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            var appsettingsPath = Path.GetFullPath("../../../../GameShop.UI/appsettings.json");
            configurationBuilder.AddJsonFile(appsettingsPath);

             // c:\\Users\\Dawid\\Documents\\GameShop\\TestsLib\\bin\\Debug\\netcoreapp3.1"
            IConfiguration config = configurationBuilder.Build();


            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("shopappDbTesting")
                .Options;

            _context = new ApplicationDbContext(options,config);

            _context.Database.EnsureCreated();

            // AutoMapper Configuration
            var mapperProfile = new AutoMapperProfiles();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            _mapper = new Mapper(mapperConfig);

            //Jwt Token Helper mocking 
            _mockedJwtTokenHelper = new Mock<IJwtTokenHelper>();

            // ASP Identity Configuration
            _mockedConfig = new Mock<IConfiguration>();
            _mockedUserManager = new Mock<UserManager<User>>(
                    new Mock<IUserStore<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<IPasswordHasher<User>>().Object,
                    new IUserValidator<User>[0],
                    new IPasswordValidator<User>[0],
                    new Mock<ILookupNormalizer>().Object,
                    new Mock<IdentityErrorDescriber>().Object,
                    new Mock<IServiceProvider>().Object,
                    new Mock<ILogger<UserManager<User>>>().Object);
            
                    
            _mockedSignInManager = new Mock<SignInManager<User>>(
                    _mockedUserManager.Object,
                    new Mock<IHttpContextAccessor>().Object,
                    new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                    new Mock<IOptions<IdentityOptions>>().Object,
                    new Mock<ILogger<SignInManager<User>>>().Object,
                    new Mock<IAuthenticationSchemeProvider>().Object,
                    new Mock<IUserConfirmation<User>>().Object   
            );

            //UnitOfWork mocking
            _unitOfWork = new UnitOfWork(_context);
            _mockedUnitOfWork = new Mock<IUnitOfWork>();

            //FormFile mocking
            _mockedFormFile = new Mock<IFormFile>();

            //Cloudinary configuration
            CloudinarySettings cloudinarySettings = new CloudinarySettings()
            {
                CloudName = "dbriyaupr",
                ApiKey = "554769833952559",
                ApiSecret = "r67SKLpl2ky4SoEr7BsHVRwVYQI"
            };
            _cloudinaryConfig = Options.Create<CloudinarySettings>(cloudinarySettings);

            
            // TO DO , when I want to test Asp Identity
            var mockRoleStore = new Mock<IRoleStore<Role>>();
            _mockedRoleManager = new Mock<RoleManager<Role>>(
                 mockRoleStore.Object, null, null, null, null);

            // Seed.SeedUsers(_userManager.Object, _roleManager.Object, config);     
            Seed.SeedProductsFKs(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}