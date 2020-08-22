using System;
using System.IO;
using AutoMapper;
using GameShop.Application.Helpers;
using GameShop.Application.Mappings;
using GameShop.Domain.Model;
using GameShop.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace TestsLib
{
    public abstract class TestBase : IDisposable
    {
        protected readonly Mock<IConfiguration> _mockedConfig;
        protected readonly Mock<UserManager<User>> _mockedUserManager;
        protected readonly Mock<SignInManager<User>> _mockedSignInManager;
        protected readonly Mock<RoleManager<Role>> _mockedRoleManager;


        protected readonly ApplicationDbContext _context;
        protected readonly IMapper _mapper;
        protected readonly UnitOfWork _unitOfWork;
        protected readonly UserManager<User> _userManager;
        protected readonly SignInManager<User> _signInManager;
        protected readonly RoleManager<Role> _roleManager;
        protected readonly IOptions<CloudinarySettings> _cloudinaryConfig;

        public TestBase()
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


            _unitOfWork = new UnitOfWork(_context);

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