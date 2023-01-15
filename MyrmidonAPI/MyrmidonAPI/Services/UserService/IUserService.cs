using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Services.UserService;

public interface IUserService
{
    Task<ServiceResponse<Tuple<Uri, GetUserDto>>> AddUser(AddUserDto addUserDto);
    Task<ServiceResponse<GetUserDto>> GetUser(Guid userId);

    Task<ServiceResponse<IActionResult>> DeleteUser(Guid userId);

    Task<ServiceResponse<IActionResult>> UpdateUser(UpdateUserDto updateUserDto);
}