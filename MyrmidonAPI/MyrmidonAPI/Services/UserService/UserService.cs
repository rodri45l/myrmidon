using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyrmidonAPI.Models.OtherModels;

namespace MyrmidonAPI.Services.UserService;

public class UserService : IUserService
{
    // Database context
    private readonly MyrmidonContext _dbContext;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;


    // Constructor
    public UserService(UserManager<User> userManager, IMapper mapper, MyrmidonContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<ServiceResponse<IActionResult>> RegisterUser(AddUserDto addUserDto)
    {
        var user = _mapper.Map<User>(addUserDto);
        var serviceResponse = new ServiceResponse<IActionResult>();

        if (addUserDto.Password == null)
        {
            serviceResponse.Data = new BadRequestObjectResult("Null password");
            serviceResponse.Success = false;
            return serviceResponse;
        }


        var result = await _userManager.CreateAsync(user
            ,
            addUserDto.Password
        );
        if (!result.Succeeded)
        {
            serviceResponse.Data = new BadRequestObjectResult(result.Errors);
            serviceResponse.Success = false;
            return serviceResponse;
        }

        addUserDto.Password = null;
        serviceResponse.Data = new CreatedResult("", addUserDto);

        return serviceResponse;
    }
}