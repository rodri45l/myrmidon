using AutoMapper;
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


    //  Constructor
    public UserTest()
    {
        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });
        var mapper = mockMapper.CreateMapper();
        _myrmidonContext = new MyrmidonContext();
        _service = new UserService(mapper, _myrmidonContext);
        _controller = new UserController(_service);
    }


    [Fact]
    public async Task GetUserOk_ShouldReturnOk()
    {
        var guid = Guid.Parse("08daf638-c1f8-414e-8925-c49b44db73f6");
        var result = await _controller.GetUser(guid);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetUser_ShouldReturnUser()
    {
        var guid = Guid.Parse("08daf638-c1f8-414e-8925-c49b44db73f6");
        var result = await _controller.GetUser(guid);
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.IsType<GetUserDto>(okResult.Value);
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
            var userId = createdUser.UserId;
            var user = await _myrmidonContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
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
            var createdUser = (GetUserDto)createdResult.Value;
            //var userId = createdUser.UserId;
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
        // Create a new user to test
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
        var createdResult = (CreatedResult)result;


        if (createdResult.Value != null)
        {
            // We remove the user created, not sure if this is the proper way to handle stuff in my database.
            var createdUser = (GetUserDto)createdResult.Value;
            var userId = createdUser.UserId;
            var deleteUser = await _controller.DeleteUser(userId);
            Assert.IsType<OkResult>(deleteUser);
            var user = await _myrmidonContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            _myrmidonContext.Users.Remove(user);
            await _myrmidonContext.SaveChangesAsync();
        }
    }

    [Fact]
    public async Task DeleteUser_ShouldReturnNotFound()
    {
        var userId = new Guid();
        var deleteUser = await _controller.DeleteUser(userId);
        Assert.IsType<NotFoundResult>(deleteUser);
    }
}