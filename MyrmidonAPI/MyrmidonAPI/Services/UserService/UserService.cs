using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyrmidonAPI.Services.UserService;

public class UserService : IUserService
{
    // Database context
    private readonly MyrmidonContext _dbContext;
    private readonly IMapper _mapper;

    // Constructor
    public UserService(IMapper mapper, MyrmidonContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<Tuple<Uri, GetUserDto>>> AddUser(AddUserDto addUserDto)
    {
        var serviceResponse = new ServiceResponse<Tuple<Uri, GetUserDto>>();

        var user = _mapper.Map<User>(addUserDto);
        
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        var uriString =  "https://example.com/users/{user.UserId}";
        try{
            Uri uri = new Uri(uriString);
            var getUserDto = _mapper.Map<GetUserDto>(user);
            serviceResponse.Data = Tuple.Create(uri, getUserDto);
            return serviceResponse;
            
        }
        catch(UriFormatException e)
        {
            serviceResponse.Success = false;
            return serviceResponse;
        }

            

        


        
    }


    public async Task<ServiceResponse<GetUserDto>> GetUser(Guid userId)
    {
        var serviceResponse = new ServiceResponse<GetUserDto>();
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null)
        {
            serviceResponse.Data = new GetUserDto();
            serviceResponse.Success = false;
            serviceResponse.Message = "User Not Found";
            return serviceResponse;
        }

        serviceResponse.Data = _mapper.Map<GetUserDto>(user);

        return serviceResponse;
    }

    public async Task<ServiceResponse<IActionResult>> DeleteUser(Guid userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null)
        {
            return new ServiceResponse<IActionResult>
            {
                Success = false

            };
        }

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return new ServiceResponse<IActionResult>
        {
            Success = true,
            Message = "User deleted."
            
        };
    }
}