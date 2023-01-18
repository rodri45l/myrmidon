using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Services.UserService;

public interface IUserAuthenticationService
{
    Task<ServiceResponse<IActionResult>> RegisterUser(AddUserDto addUserDto);
    Task<ServiceResponse<IActionResult>> Login(UserLoginDto loginDto);
}