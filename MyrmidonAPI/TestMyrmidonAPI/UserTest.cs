using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyrmidonAPI;
using MyrmidonAPI.Controllers;
using MyrmidonAPI.Dtos.User;
using MyrmidonAPI.Models;
using MyrmidonAPI.Services.UserService;

namespace TestMyrmidonAPI;

public class UserTest
{
    private readonly UserController _controller;
    private readonly IMapper _mapper;
    private readonly MyrmidonContext _myrmidonContext;
    private readonly string _password = "password";
    private readonly IUserService _service;
    private readonly UserManager<User> _userManager;

    private readonly List<User> _users;


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

        _userManager = IdentityMockHelper.MockUserManager(_users).Object;
        // _myrmidonContext = new MyrmidonContext();
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDb")
            .Options;

        _myrmidonContext = new MyrmidonContext(options);
        _service = new UserService(_userManager, _mapper, _myrmidonContext);
        _controller = new UserController(_service);
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
    
    
}