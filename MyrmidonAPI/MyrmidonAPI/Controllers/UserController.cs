using Microsoft.AspNetCore.Mvc;

namespace MyrmidonAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UserController : Controller
{
    // private readonly MyrmidonContext _dbContext;
    private readonly IUserService _userService;

    public UserController( /*MyrmidonContext dbContext,*/ IUserService userService)
    {
        _userService = userService;
        // _dbContext = dbContext;
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var serviceResponse = await _userService.GetUser(userId);

        if (!serviceResponse.Success) return NotFound();

        return Ok(serviceResponse.Data);
    }

    // Post
    [HttpPost]
    public async Task<IActionResult> RegisterUser(AddUserDto addUserDto)
    {
        var serviceResponse = await _userService.AddUser(addUserDto);
        if (serviceResponse.Success == false || serviceResponse.Data == null)
            return BadRequest(serviceResponse.Message);
        var (location, newUser) = serviceResponse.Data;
        return Created(location, newUser);
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var serviceResponse = await _userService.DeleteUser(userId);

        if (!serviceResponse.Success) return NotFound();

        return Ok();
    }
}