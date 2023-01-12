using BCrypt.Net;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using MyrmidonAPI.Data;
using MyrmidonAPI.Entities;

namespace MyrmidonAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly MyrmidonContext _dbContext;

    public UserController(MyrmidonContext dbContext)
    {
        this._dbContext = dbContext;
    }
    
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    
    // Post
    [HttpPost]
    public async Task<IActionResult> RegisterUser(AddLoginRequest addLoginRequest)
    {
        var user = new User()
        {
            UserId = Guid.NewGuid(),
            Name = addLoginRequest.Name,
            Surname = addLoginRequest.Surname,
            BirthDate = addLoginRequest.BirthDate,
            PostalCode = addLoginRequest.PostalCode,
            Email = addLoginRequest.Email, //to do : validate email input
            Address = addLoginRequest.Address,
            Phone = addLoginRequest.Phone,
            Sex = addLoginRequest.Sex, // True female, False for male
            Gender = addLoginRequest.Gender, // to do : enum for gender options? or set it on the gui.
            Password = BCrypt.Net.BCrypt.HashPassword(addLoginRequest.Password)
        };
        await _dbContext.Users.AddAsync(user);
        try
        {
            await _dbContext.SaveChangesAsync();
            var newUser = await _dbContext.Users.FindAsync(user.UserId);
            var location = new Uri(Request.GetEncodedUrl().ToString() + "/" + newUser.UserId);
            return Created(location, newUser);
        }
        catch (DbUpdateException ex )
        {
            if (ex.InnerException.Message.Contains("Duplicate entry"))
            {
                return Conflict("Email already exists");
            }

            return BadRequest("An error occurred while saving the entity changes.");
        }



    }
}