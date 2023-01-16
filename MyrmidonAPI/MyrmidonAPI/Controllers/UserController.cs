using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Services.UserService;

namespace MyrmidonAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    // private readonly MyrmidonContext _dbContext;
    private readonly IUserService _userService;
    

    public UserController(   IUserService userService)
    {
        _userService = userService;
        
    }

    /*
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var serviceResponse = await _userService.GetUser(userId);

        if (!serviceResponse.Success) return NotFound();

        return Ok(serviceResponse.Data);
    }*/
    
    
    /*{
        "name": "rodrigo",
        "surname": "Lara",
        "birthDate": "1999-10-02T18:20:57.964Z",
        "postalCode": "29139",
        "email": "rodri45esp@gmail.com",
        "address": "address st",
        "phoneNumber": "+460793570919",
        "sex": false,
        "gender": "Male",
        "userName": "rodri45z",
        "password": "SecurePassword123@"
    }*/

    // Post
    [HttpPost]
    public async Task<IActionResult> RegisterUser(AddUserDto addUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _userService.RegisterUser(addUserDto);
        if (result.Data == null) return BadRequest("Something went wrong");

        return result.Data;


        /*var serviceResponse = await _userService.AddUser(addUserDto);
        if (serviceResponse.Success == false || serviceResponse.Data == null)
            return BadRequest(serviceResponse.Message);
        var (location, newUser) = serviceResponse.Data;
        return Created(location, newUser);*/
    }

    /*[HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var serviceResponse = await _userService.DeleteUser(userId);

        if (!serviceResponse.Success) return NotFound();

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
    {
        var response = await _userService.UpdateUser(updateUserDto);
        return response.Data!;
    }*/
}