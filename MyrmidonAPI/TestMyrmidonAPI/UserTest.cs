using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using MyrmidonAPI;
using MyrmidonAPI.Controllers;
using MyrmidonAPI.Dtos.User;
using MyrmidonAPI.Models;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Repositories;
using MyrmidonAPI.Repositories.Interfaces;
using MyrmidonAPI.Services;
using MyrmidonAPI.Services.SessionTokenService;
using MyrmidonAPI.Services.UserService;
using Xunit.Sdk;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace TestMyrmidonAPI;

public class UserTest 
{
    private readonly UserController _controller;
    private readonly IMapper _mapper;
    private readonly MyrmidonContext _myrmidonContext;
    private readonly string _password = "password";
    private readonly IUserAuthenticationService _authenticationService;
    private readonly UserManager<User> _userManager;

    private readonly List<User> _users;
    private readonly SessionTokenService _sessionTokenService;
    private readonly IMemoryCache _memoryCache;
    private readonly SessionTokenRepository _sessionTokenRepository;

    //  Constructor
    public UserTest()
    {
        _users = new List<User>
        {
            new()
            {
                Id = new Guid(),
                Email = "test@test.com",
                UserName = "test@test.com"
            },
            new()
            {
                Id = new Guid(),
                Email = "test2@test.com",
                UserName = "test2@test.com"
            }
        };

        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });
        _mapper = mockMapper.CreateMapper();

        
        // _myrmidonContext = new MyrmidonContext();
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDb")
            .Options;
       // var userManagerMock = new Mock<UserManager<User>>();
       var userManagerMock = new Mock<UserManager<User>>(
           /* IUserStore<TUser> store */Mock.Of<IUserStore<User>>(),
           /* IOptions<IdentityOptions> optionsAccessor */null,
           /* IPasswordHasher<TUser> passwordHasher */null,
           /* IEnumerable<IUserValidator<TUser>> userValidators */null,
           /* IEnumerable<IPasswordValidator<TUser>> passwordValidators */null,
           /* ILookupNormalizer keyNormalizer */null,
           /* IdentityErrorDescriber errors */null,
           /* IServiceProvider services */null,
           /* ILogger<UserManager<TUser>> logger */null);
       /*userManagerMock.Setup(sim => sim.FindByNameAsync(It.IsAny<string>()))
           .ReturnsAsync((string name) => new User{UserName=name, Id=new Guid()});*/
      
          var userManager = IdentityMockHelper.MockUserManager(_users);
          userManager.Setup(um => um.FindByNameAsync(It.IsAny<string>()))
              .ReturnsAsync((string name) => new User { UserName = name, Id = new Guid() });

          _userManager = userManager.Object;
       
       var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

       var signInManagerMock = new Mock<SignInManager<User>>(
           userManagerMock.Object,
           httpContextAccessorMock.Object,
           /* IUserClaimsPrincipalFactory<TUser> claimsFactory */Mock.Of<IUserClaimsPrincipalFactory<User>>(),
           /* IOptions<IdentityOptions> optionsAccessor */null,
           /* ILogger<SignInManager<TUser>> logger */null,
           /* IAuthenticationSchemeProvider schemes */null,
           /* IUserConfirmation<TUser> confirmation */null);
       signInManagerMock.Setup(sim => sim.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
           .ReturnsAsync((string username, string password, bool lockoutOnFailure, bool succeeded) => {
               if (string.IsNullOrEmpty(password))
               {
                   return SignInResult.Failed;
               }
               else if (password == "LockedOut")
               {
                   return SignInResult.LockedOut;
               }
               else if (password == "NotAllowed")
               {
                   return SignInResult.NotAllowed;
               }
               else if (password == "Two")
               {
                   return SignInResult.TwoFactorRequired;
               }

               return SignInResult.Success;
           });
       // Create a mock IMemoryCache object
       var memoryCacheMock = new Mock<IMemoryCache>();

// Create a mock ISessionTokenRepository object and pass the mock IMemoryCache object to its constructor
       var sessionTokenRepositoryMock = new Mock<ISessionTokenRepository>();
       /*sessionTokenRepositoryMock.Setup(x => x.StoreSessionToken(It.IsAny<SessionToken>()))
           .ReturnsAsync(new Result() { Success = true });*/
       var sessionTokenServiceMock = new Mock<SessionTokenService>(sessionTokenRepositoryMock.Object);
       sessionTokenServiceMock.Setup(sim =>sim.CreateSessionToken(It.IsAny<User>()))
           .ReturnsAsync((User user) =>
               {
                   if (user.UserName == "returnError")
                   {
                       return new ServiceResponse<SessionToken>()
                       {
                           Success = false
                       };
                   }
                   else
                   {
                       return new ServiceResponse<SessionToken>()
                       {
                           Success = false,
                           Data = new SessionToken()
                           {
                               ExpirationTime = DateTime.Now,
                               TokenId = new Guid(),
                           }
                       }; 
                   }

               }
               );

       _sessionTokenService = sessionTokenServiceMock.Object;

        _myrmidonContext = new MyrmidonContext(options);
        _authenticationService = new UserAuthenticationService(_userManager, _mapper, _myrmidonContext, signInManagerMock.Object, _sessionTokenService);
        _controller = new UserController(_authenticationService);
    }

    [Fact]
    public async Task TestCreateAUser_ShouldReturnCreated()
    {
        var newUser = new AddUserDto
        {
            Name = "rodrigo",

            Surname = "Lara",
            BirthDate = DateTime.Now,
            PostalCode = "29139",
            Email = "rodri45esp@gmail.com",
            Address = "address st",
            PhoneNumber = "+460793570919",
            Sex = false,
            Gender = "Male",
            UserName = "rodri45z",
            Password = "SecurePassword123@"
        };

        var result = await _controller.RegisterUser(newUser);
        Assert.IsType<CreatedResult>(result);
        Assert.Equal(3, _users.Count);
    }
    
    [Fact]
    public async Task TestCreateAUser_ShouldReturnBadRequest()
    {
        var newUser = new AddUserDto
        {
            Name = "rodrigo",

            Surname = "Lara",
            BirthDate = DateTime.Now,
            PostalCode = "29139",
            Email = "rodri45esp@gmail.com",
            Address = "address st",
            PhoneNumber = "+460793570919",
            Sex = false,
            Gender = "Male",
            UserName = "rodri45z",
            Password = null
        };

        var result = await _controller.RegisterUser(newUser);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(2, _users.Count);
    }
    
    
    [Fact]
    public async Task TestCreateAUser_ShouldReturnBadRequestMessage()
    {
        var newUser = new AddUserDto
        {
            Name = "rodrigo",

            Surname = "Lara",
            BirthDate = DateTime.Now,
            PostalCode = "29139",
            Email = "rodri45esp@gmail.com",
            Address = "address st",
            PhoneNumber = "+460793570919",
            Sex = false,
            Gender = "Male",
            UserName = "rodri45z",
            Password = string.Empty
        };

        var result = await _controller.RegisterUser(newUser);
        Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(2, _users.Count);
    }
    
    [Fact]
    public async Task TestLoginUser_ShouldReturnOk()
    {
        var loginUser = new UserLoginDto()
        {
            UserName = "rodri45z",
            Password = "Password123"
        };
        
        var newUser = new User()
        {
            Name = "rodrigo",

            Surname = "Lara",
            BirthDate = DateTime.Now,
            PostalCode = "29139",
            Email = "rodri45esp@gmail.com",
            Address = "address st",
            PhoneNumber = "+460793570919",
            Sex = false,
            Gender = "Male",
            UserName = "rodri45z",
            
        };
        await _userManager.CreateAsync(newUser, "Password123");
        var newUserResult = await _userManager.FindByNameAsync("rodrigo");
        var result = await _authenticationService.Login(loginUser);
        Assert.True(result.Success);
        Assert.IsType<OkObjectResult>(result.Data);
        

    }
    
    [Fact]
    public async Task TestLoginUser_ShouldReturnBadRequest()
    {
        var loginUser = new UserLoginDto()
        {
            UserName = "rodri45esp@gmail.com",
            Password = string.Empty
        };

        var result = await _authenticationService.Login(loginUser);
        Assert.False(result.Success);
        Assert.IsType<ObjectResult>(result.Data);
        
    }
    
    [Fact]
    public async Task TestLoginUser_ShouldReturnBadRequest2()
    {
        var loginUser = new UserLoginDto()
        {
            UserName = "Pete",
            Password = "NotAllowed"
        };

        var result = await _authenticationService.Login(loginUser);
        Assert.False(result.Success);
        Assert.IsType<ObjectResult>(result.Data);
        
    }

    [Fact]
    public async Task? TestLoginUser_ShouldReturnBadRequest3()
    {
        
        var loginUser = new UserLoginDto()
        {
            UserName = "Pete",
            Password = "LockedOut"
        };

        var result = await _authenticationService.Login(loginUser);
        Assert.False(result.Success);
        Assert.IsType<ObjectResult>(result.Data);
        
    }
    
    [Fact]
    public async Task? TestLoginUser_ShouldReturnBadRequest4()
    {
        
        var loginUser = new UserLoginDto()
        {
            UserName = "Pete",
            Password = "Two"
        };

        var result = await _authenticationService.Login(loginUser);
        Assert.False(result.Success);
        Assert.IsType<ObjectResult>(result.Data);
        
    }
    
    
}
    
  
    
    
