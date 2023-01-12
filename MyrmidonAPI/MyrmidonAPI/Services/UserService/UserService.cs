using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MyrmidonAPI.Services.UserService;

public class UserService : IUserService
{
    // Database context
    private readonly MyrmidonContext _dbContext;

    // Constructor
    public UserService(MyrmidonContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<Tuple<Uri, GetUserDto>>> AddUser(AddUserDto addUserDto, HttpRequest request)
    {
        var serviceResponse = new ServiceResponse<Tuple<Uri, GetUserDto>>();
        var user = new User
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
        };
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        var location = new Uri(request.GetEncodedUrl() + "/" + user.UserId);

        var getUserDto = new GetUserDto()
        {
            Name = user.Name,
            Surname = user.Surname,
            BirthDate = user.BirthDate,
            PostalCode = user.PostalCode,
            Email = user.Email, 
            Address = user.Address,
            Phone = user.Phone,
            Sex = user.Sex, 
            Gender = user.Gender, 
            UserType = user.UserType
            
            
        };

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

        /*var moods = _dbContext.Moods.Where(m => m.UserId == userId)
            .Select(m => new MoodDTO
            {
                MoodId = m.MoodId,
                Rating = m.Rating,
                Date = m.Date,
                UserId = m.UserId
            })
            .ToList();*/
        var userDto = new GetUserDto
        {
            Name = user.Name,
            Surname = user.Surname,
            BirthDate = user.BirthDate,
            PostalCode = user.PostalCode,
            Email = user.Email,
            Address = user.Address,
            Phone = user.Phone,
            Sex = user.Sex,
            Gender = user.Gender,
            UserType = user.UserType,
            
        };
        serviceResponse.Data = userDto;

        return serviceResponse;
    }
}