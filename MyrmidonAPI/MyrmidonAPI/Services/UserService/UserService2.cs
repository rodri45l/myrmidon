using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Services.UserService;

public class UserService2 : IUserService
{
    // Database context
    private readonly MyrmidonContext _dbContext;
    private readonly IMapper _mapper;

    // Constructor
    public UserService2(IMapper mapper, MyrmidonContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<Tuple<Uri, GetUserDto>>> AddUser(AddUserDto addUserDto)
    {
        var serviceResponse = new ServiceResponse<Tuple<Uri, GetUserDto>>();

       
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);


        var uriString = "https://example.com/users/{user.Id}";
        try
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            var uri = new Uri(uriString);
            var getUserDto = _mapper.Map<GetUserDto>(user);
            serviceResponse.Data = Tuple.Create(uri, getUserDto);
            return serviceResponse;
        }
        catch (DbUpdateException ex)
        {
            serviceResponse.Success = false;
            if (ex.InnerException != null && ex.InnerException.Message.Contains("Duplicate entry"))
                serviceResponse.Message = "Email already exists";
            else
                serviceResponse.Message = "An error occurred while saving the entity changes.";


            return serviceResponse;
        }
    }


    public async Task<ServiceResponse<GetUserDto>> GetUser(Guid Id)
    {
        var serviceResponse = new ServiceResponse<GetUserDto>();
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == Id);
        if (user == null || user.Deleted)
        {
            serviceResponse.Data = new GetUserDto();
            serviceResponse.Success = false;
            serviceResponse.Message = "User Not Found";
            return serviceResponse;
        }

        serviceResponse.Data = _mapper.Map<GetUserDto>(user);

        return serviceResponse;
    }

    public async Task<ServiceResponse<IActionResult>> DeleteUser(Guid Id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == Id);
        if (user == null || user.Deleted)
            return new ServiceResponse<IActionResult>
            {
                Success = false
            };

        user.Deleted = true;

        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();

        return new ServiceResponse<IActionResult>
        {
            Success = true,
            Message = "User deleted."
        };
    }

    public async Task<ServiceResponse<IActionResult>> UpdateUser(UpdateUserDto updateUserDto)
    {
        var result = new ServiceResponse<IActionResult>();
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == updateUserDto.Id);
        if (user == null)
        {
            result.Success = false;
            result.Message = "User doesn't exist";
            result.Data = new NotFoundResult();
            return result;
        }

        if (await _dbContext.Users.AnyAsync(u => u.Email == updateUserDto.Email && u.Id != user.Id))
        {
            result.Message = "Email already exists";
            result.Success = false;
            result.Data = new BadRequestObjectResult("Email already exists");
            return result;
        }
        // set Password to null if user don't want to change it.

        updateUserDto.Password = updateUserDto.Password != null
            ? BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password)
            : user.Password;

        _mapper.Map(updateUserDto, user);

        await _dbContext.SaveChangesAsync();
        result.Data = new OkResult();
        return result;
    }
}