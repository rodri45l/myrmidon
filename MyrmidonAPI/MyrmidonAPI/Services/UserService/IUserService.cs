namespace MyrmidonAPI.Services.UserService;

public interface IUserService
{
    Task<ServiceResponse<Tuple<Uri, GetUserDto>>> AddUser(AddUserDto addUserDto, HttpRequest request);
    Task<ServiceResponse<GetUserDto>> GetUser(Guid userId);
}