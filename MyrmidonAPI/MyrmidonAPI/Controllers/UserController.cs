using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Services.UserService;

namespace MyrmidonAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    // private readonly MyrmidonContext _dbContext;
    private readonly IUserAuthenticationService _userAuthenticationService;


    public UserController(IUserAuthenticationService userAuthenticationService)
    {
        _userAuthenticationService = userAuthenticationService;
    }
    

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
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _userAuthenticationService.RegisterUser(addUserDto);
        if (result.Data == null) return BadRequest("Something went wrong");

        return result.Data;
        
    }

    [HttpPost]
    public async Task<IActionResult> LoginUser(UserLoginDto user)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _userAuthenticationService.Login(user);
        return result.Data ?? BadRequest("Something Went Wrong");
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