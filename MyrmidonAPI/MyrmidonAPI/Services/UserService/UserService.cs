using Microsoft.AspNetCore.Http.Extensions;
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

    public async Task<ServiceResponse<Tuple<Uri, GetUserDto>>> AddUser(AddUserDto addUserDto, HttpRequest request)
    {
        var serviceResponse = new ServiceResponse<Tuple<Uri, GetUserDto>>();

        var user = _mapper.Map<User>(addUserDto);
        /*var user = new User
        {
            UserId = Guid.NewGuid(),
            Name = addUserDto.Name,
            Surname = addUserDto.Surname,
            BirthDate = addUserDto.BirthDate,
            PostalCode = addUserDto.PostalCode,
            Email = addUserDto.Email, //to do : validate email input
            Address = addUserDto.Address,
            Phone = addUserDto.Phone,
            Sex = addUserDto.Sex, // True female, False for male
            Gender = addUserDto.Gender, // to do : enum for gender options? or set it on the gui.
            Password = BCrypt.Net.BCrypt.HashPassword(addUserDto.Password)
        };*/
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        var location = new Uri(request.GetEncodedUrl() + "/" + user.UserId);

        var getUserDto = _mapper.Map<GetUserDto>(user);

        serviceResponse.Data = Tuple.Create(location, getUserDto);


        return serviceResponse;
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
}