using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyrmidonAPI.Models.OtherModels;
using MyrmidonAPI.Services.SessionTokenService;

namespace MyrmidonAPI.Services.UserService;

public sealed class UserAuthenticationService : IUserAuthenticationService
{
    // Database context
    private readonly MyrmidonContext _dbContext;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    private readonly ISessionTokenService _sessionTokenService;
    // private readonly IJwtService _jwtService;


    // Constructor
    public UserAuthenticationService(UserManager<User> userManager, IMapper mapper, MyrmidonContext dbContext, SignInManager<User> signInManager, ISessionTokenService sessionTokenService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _sessionTokenService = sessionTokenService;

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
    

    public async Task<ServiceResponse<IActionResult>> Login(UserLoginDto loginDto)
    {
        var serviceResponse = new ServiceResponse<IActionResult>();

        var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            var tokenResult = await _sessionTokenService.CreateSessionToken(user);
            var token = tokenResult.Data;
            
            
            
            serviceResponse.Data = new OkObjectResult(new {token = token.TokenId });
            serviceResponse.Success = true;
        }
        else if (result.IsLockedOut)
        {
            serviceResponse.Data = new ObjectResult("User account locked out.") { StatusCode = StatusCodes.Status429TooManyRequests };
            serviceResponse.Success = false;
        }
        else if (result.IsNotAllowed)
        {
            serviceResponse.Data = new ObjectResult("User not allowed to log in.") { StatusCode = StatusCodes.Status403Forbidden };
            serviceResponse.Success = false;
        }
        else
        {
            serviceResponse.Data = new ObjectResult("Invalid login attempt.") { StatusCode = StatusCodes.Status401Unauthorized };
            serviceResponse.Success = false;
        }

        return serviceResponse;
    }
    
    
    /*private SigningCredentials GetSigningCredentials()
    {
        var jwtConfig = _configuration.GetSection("jwtConfig");
        var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserName)
        };
        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtConfig");
        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["validIssuer"],
            audience: jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expiresIn"])),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }*/
}