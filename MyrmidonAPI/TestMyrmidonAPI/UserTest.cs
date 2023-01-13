using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MyrmidonAPI;
using MyrmidonAPI.Controllers;
using MyrmidonAPI.Dtos.User;
using MyrmidonAPI.Models;
using MyrmidonAPI.Services.UserService;

namespace TestMyrmidonAPI;

public class UserTest 
{
    private readonly UserController _controller;
    private readonly IUserService _service;
    
    
    
    public UserTest()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        });
        var mapper = mockMapper.CreateMapper();
        var dbContext = new MyrmidonContext();
        _service = new UserService(mapper,dbContext );
        _controller = new UserController(_service);
    }
    [Fact]
    public async Task GetUserOk_ShouldReturnOk()
    {
        var guid = Guid.Parse("d532b1ab-921b-11ed-8886-c5d1c66cc8a8");
        var result = await _controller.GetUser(guid);
        Assert.IsType<OkObjectResult>(result);

    }
    [Fact]
    public async Task GetUser_ShouldReturnUser()
    {
        var guid = Guid.Parse("d532b1ab-921b-11ed-8886-c5d1c66cc8a8");
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
    public async Task AddUser_ShouldReturnCreated()
    {
        var newUser = new AddUserDto(){
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
            var createdUser = (GetUserDto)createdResult.Value;
            var userId = createdUser.UserId;
            await _controller.DeleteUser(userId);
        }
        
        
        
    }
    
    
    
    
    
}
    
