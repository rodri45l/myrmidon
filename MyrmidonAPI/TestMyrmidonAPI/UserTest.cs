using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
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
    private readonly MyrmidonContext _myrmidonContext;
    private readonly IUserService _service;
    private readonly IMapper _mapper;
    private readonly string _password = "password";


    //  Constructor
    public UserTest()
    {
        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });
        _mapper = mockMapper.CreateMapper();
        _myrmidonContext = new MyrmidonContext();
        _service = new UserService(_mapper, _myrmidonContext);
        _controller = new UserController(_service);
    }

    private async Task<GetUserDto> AddTestUser()
    {
        var newUser = new AddUserDto
        {
            Name = "John",
            Surname = "Doe",
            BirthDate = new DateTime(1980, 1, 1),
            PostalCode = "12345",
            Email = "johndoe@gmai43l.com",
            Address = "123 Main St",
            Phone = "555-555-5555",
            Sex = true,
            Gender = "non-binary",
            Password = _password
        };
        var addResult = (CreatedResult)await _controller.RegisterUser(newUser);
        var getUser = (GetUserDto)addResult.Value!;
        return getUser;
    }

    private async Task DeleteTestUser(Guid userId)
    {
        var user = await _myrmidonContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        _myrmidonContext.Users.Remove(user!);
        await _myrmidonContext.SaveChangesAsync();
    }


    [Fact]
    public async Task GetUserOk_ShouldReturnOk()
    {
        var getUser = await AddTestUser();
        var result = await _controller.GetUser(getUser.UserId);
        Assert.IsType<OkObjectResult>(result);
        await DeleteTestUser(getUser.UserId);
    }

    [Fact]
    public async Task GetUser_ShouldReturnUser()
    {
        var getUser = await AddTestUser();

        var result = await _controller.GetUser(getUser.UserId);
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.IsType<GetUserDto>(okResult?.Value);

        await DeleteTestUser(getUser.UserId);
    }

    [Fact]
    public async Task GetUser_ShouldReturnNotFound()
    {
        var guid = Guid.Parse("d532b1ab-921b-11ed-8886-c5d1c66cc8a9");
        var result = await _controller.GetUser(guid);
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnCreated()
    {
        var newUser = new AddUserDto
        {
            Name = "John",
            Surname = "Doe",
            BirthDate = new DateTime(1980, 1, 1),
            PostalCode = "12345",
            Email = "johndoe@gmai43l.com",
            Address = "123 Main St",
            Phone = "555-555-5555",
            Sex = true,
            Gender = "non-binary",
            Password = "password"
        };


        var result = await _controller.RegisterUser(newUser);
        Assert.IsType<CreatedResult>(result);
        var createdResult = (CreatedResult)result;
        if (createdResult.Value != null)
        {
            // We remove the user created, not sure if this is the proper way to handle stuff in my database.
            var createdUser = (GetUserDto)createdResult.Value;
            await DeleteTestUser(createdUser.UserId);
        }
    }

    [Fact]
    public async Task RegisterUser_CheckPasswordHashing()
    {
        var createdUser = await AddTestUser();
        var userId = createdUser.UserId;

        var user = await _myrmidonContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user != null)
        {
            var assertion = BCrypt.Net.BCrypt.Verify(_password, user.Password);
            Assert.True(assertion);
            _myrmidonContext.Users.Remove(user);
            await _myrmidonContext.SaveChangesAsync();
        }
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnBadRequest()
    {
        var newUser = new AddUserDto
        {
            Name = "John",
            Surname = "Doe",
            BirthDate = new DateTime(1980, 1, 1),
            PostalCode = "12345",
            Email = "johndoe@gmai43l.com",
            Address = "123 Main St",
            Phone = "555-555-5555",
            Sex = true,
            Gender = "non-binary",
            Password = "password"
        };

        var result = await _controller.RegisterUser(newUser);
        var result2 = await _controller.RegisterUser(newUser);

        Assert.IsType<BadRequestObjectResult>(result2);
        const string message = "Email already exists";
        var resultMessage = (BadRequestObjectResult)result2;
        Assert.True(message == (string)resultMessage.Value!);
        var createdResult = (CreatedResult)result;
        if (createdResult.Value != null)
        {
            // We remove the user created, not sure if this is the proper way to handle stuff in my database.
            
            var users = await _myrmidonContext.Users.Where(u => u.Email == newUser.Email).ToListAsync();
            _myrmidonContext.Users.RemoveRange(users);
            await _myrmidonContext.SaveChangesAsync();

            var users2 = await _myrmidonContext.Users.Where(u => u.Email == newUser.Email).ToListAsync();
            _myrmidonContext.Users.RemoveRange(users2);
            await _myrmidonContext.SaveChangesAsync();
        }
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnOk()
    {
        var createdUser = await AddTestUser();


        // We remove the user created, not sure if this is the proper way to handle stuff in my database.

        var userId = createdUser.UserId;
        var deleteUser = await _controller.DeleteUser(userId);
        Assert.IsType<OkResult>(deleteUser);
        await DeleteTestUser(userId);
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnNotFound()
    {
        var userId = new Guid();
        var deleteUser = await _controller.DeleteUser(userId);
        Assert.IsType<NotFoundResult>(deleteUser);
    }

    [Fact]
    public async Task UpdateUser_ShouldReturnOk()
    {
        var user = await AddTestUser();
        var updateUser = new UpdateUserDto();
        
        _mapper.Map(user, updateUser);
        updateUser.Password = _password;
         var result = await _controller.UpdateUser(updateUser);
         Assert.IsType<OkResult>(result);
         await DeleteTestUser(user.UserId);
         //Assert.True(true);
    }
    
    [Fact]
    public async Task UpdateUser_ShouldReturnNotFound()
    {

        var testUser = await AddTestUser();
        var updateUser = new UpdateUserDto();
        _mapper.Map(testUser, updateUser);
        updateUser.Password = _password;
        updateUser.UserId = new Guid();
        var result = await _controller.UpdateUser(updateUser);
        Assert.IsType<NotFoundResult>(result);
        await DeleteTestUser(testUser.UserId);
        
    }
    
    [Fact]
    public async Task UpdateUser_ShouldReturnBadRequest()
    {
        // Create first user
        var testUser = await AddTestUser();
        //save its UserId and change email.
        
        var updateUser = new UpdateUserDto();
        _mapper.Map(testUser, updateUser);
        updateUser.Password = _password;
        updateUser.Email = "testEmail";
        await _controller.UpdateUser(updateUser);
        
        var testUser2 = await AddTestUser();
        var updateUser2 = new UpdateUserDto();
        _mapper.Map(testUser2, updateUser2);
        updateUser2.Password = _password;
        updateUser2.Email = "testEmail";
        var result = await _controller.UpdateUser(updateUser2);
        Assert.IsType<BadRequestObjectResult>(result);
        await DeleteTestUser(testUser.UserId);
        await DeleteTestUser(testUser2.UserId);

    }
}