using Microsoft.AspNetCore.Mvc;

namespace MyrmidonAPI.Services.UserService;

public interface IUserService
{
    Task<ServiceResponse<Tuple<Uri, GetUserDto>>> AddUser(AddUserDto addUserDto);
    Task<ServiceResponse<GetUserDto>> GetUser(Guid userId);
    
    Task<ServiceResponse<IActionResult>> DeleteUser(Guid userId);
    
    
}