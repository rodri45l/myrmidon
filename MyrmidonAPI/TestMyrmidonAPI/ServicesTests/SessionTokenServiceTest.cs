namespace TestMyrmidonAPI;

public class SessionTokenServiceTest
{
    private readonly MyrmidonContext _myrmidonContext;
    private readonly SessionTokenRepository _sessionTokenRepository;
    private readonly User _user;
    private readonly SessionTokenService _sessionTokenService;


    public SessionTokenServiceTest()
    {
        var cache = new MemoryCache(new MemoryCacheOptions());
        var cache2 = new MemoryCache(new MemoryCacheOptions());
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDbSessServ")
            .Options;
       
        _myrmidonContext = new MyrmidonContext(options);
       
        _sessionTokenRepository = new SessionTokenRepository(_myrmidonContext, cache2);
        _sessionTokenService = new SessionTokenService(_sessionTokenRepository);
        _user = new User
        {
            Name = "Rodrigo",
            Surname = "Lara",
            UserName = "Rodri45z",

            BirthDate = DateTime.Now,
            PostalCode = "29139",
            Email = "rodri45esp@gmail.com",
            Address = "asdfadfasdf",
            PhoneNumber = "+460793570919",
            Sex = false,
            Gender = "Male",
            PasswordHash = "SecurePassword123@"
        };
        
        
    }
    
     [Fact]
    public async Task TestStoreSessionToken_ShouldReturnSuccess()
    {
        await _myrmidonContext.Users.AddAsync(_user);
        var result = await _sessionTokenService.CreateSessionToken(_user);
        Assert.True(result.Success);
    }

   

    [Fact]
    public async Task TestCheckSessionToken_ShouldReturnSuccess()
    {
        await _myrmidonContext.Users.AddAsync(_user);
        var sessionToken = new SessionToken
        {
            TokenId = Guid.NewGuid(),
            UserId = _user.Id,
            ExpirationTime = default,
            User = _user
        };
        await _sessionTokenRepository.StoreSessionToken(sessionToken);
        var result = await _sessionTokenService.CheckSessionToken(sessionToken.TokenId.ToString());
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestCheckSessionToken_ShouldReturnFail()
    {
        var sessionToken = new SessionToken
        {
            TokenId = Guid.NewGuid(),
            UserId = _user.Id,
            ExpirationTime = default,
            User = _user
        };

        var result = await _sessionTokenService.CheckSessionToken(sessionToken.TokenId.ToString());
        Assert.False(result.Success);
    }
    
    

    [Fact]
    public async Task TestDeleteSessionToken_ShouldReturnSuccess()
    {
        await _myrmidonContext.Users.AddAsync(_user);
        var sessionToken = new SessionToken
        {
            TokenId = Guid.NewGuid(),
            UserId = _user.Id,
            ExpirationTime = default,
            User = _user
        };
        await _sessionTokenRepository.StoreSessionToken(sessionToken);
        var result = await _sessionTokenService.DeleteSessionToken(sessionToken.TokenId.ToString());
        Assert.True(result.Success);
    }

    
    [Fact]
    public async Task TestDeleteSessionToken_ShouldReturnFail()
    {
        var result = await _sessionTokenService.DeleteSessionToken(Guid.NewGuid().ToString());
        Assert.False(result.Success);
    }
    
    

    
    
    

}