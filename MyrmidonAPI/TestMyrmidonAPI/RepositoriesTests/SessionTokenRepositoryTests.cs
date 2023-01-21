namespace TestMyrmidonAPI;

public class SessionTokenRepositoryTests
{
    private readonly MyrmidonContext _myrmidonContext;
    private readonly SessionTokenRepository _sessionTokenRepository;
    private readonly SessionTokenRepository _sessionTokenRepository2;
    private readonly User _user;


    public SessionTokenRepositoryTests()
    {
        var cache = new MemoryCache(new MemoryCacheOptions());
        var cache2 = new MemoryCache(new MemoryCacheOptions());
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDbSess")
            .Options;
        var options2 = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemorySessDb2")
            .Options;
        _myrmidonContext = new MyrmidonContext(options);
        var myrmidonContext2 = new MyrmidonContext(options2);
        _sessionTokenRepository2 = new SessionTokenRepository(myrmidonContext2, cache);
        _sessionTokenRepository = new SessionTokenRepository(_myrmidonContext, cache2);
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
        var sessionToken = new SessionToken
        {
            TokenId = Guid.NewGuid(),
            UserId = _user.Id,
            ExpirationTime = default,
            User = _user
        };
        var result = await _sessionTokenRepository.StoreSessionToken(sessionToken);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestStoreSessionToken_ShouldReturnFail()
    {
        var sessionToken = new SessionToken
        {
            TokenId = Guid.NewGuid(),
            UserId = _user.Id,
            ExpirationTime = default,
            User = _user
        };
        await _sessionTokenRepository.StoreSessionToken(sessionToken);
        var result = await _sessionTokenRepository.StoreSessionToken(sessionToken);
        Assert.False(result.Success);
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
        var result = await _sessionTokenRepository.CheckSessionToken(sessionToken.TokenId.ToString());
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

        var result = await _sessionTokenRepository.CheckSessionToken(sessionToken.TokenId.ToString());
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
        var result = await _sessionTokenRepository.DeleteSessionToken(sessionToken.TokenId.ToString());
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestDeleteSessionToken_ShouldReturnFail()
    {
        var result = await _sessionTokenRepository.DeleteSessionToken(Guid.NewGuid().ToString());
        Assert.False(result.Success);
    }

    [Fact]
    public void TestCreateSessionCacheAsync_ShouldReturnSessionId()
    {
        var guid = Guid.NewGuid();
        var result = _sessionTokenRepository.CreateSessionCacheAsync(guid.ToString());
        var checkResult = _sessionTokenRepository.GetUserIdBySessionCacheAsync(result);

        Assert.True(checkResult == guid.ToString());
    }

    [Fact]
    public async Task TestGetUserIdBySessionCacheAsync_ShouldSuccess()
    {
        await _myrmidonContext.Users.AddAsync(_user);
        var sessionId = _sessionTokenRepository.CreateSessionCacheAsync(_user.Id.ToString());
        var result = _sessionTokenRepository.GetUserIdBySessionCacheAsync(sessionId);
        Assert.True(result == _user.Id.ToString());
    }

    [Fact]
    public void TestGetUserIdBySessionCacheAsync_ShouldFail()
    {
        var result = _sessionTokenRepository.GetUserIdBySessionCacheAsync("sessionId");
        Assert.True(result == null);
    }


    [Fact]
    public async Task TestRemoveCacheSessionId_ShouldSuccess()
    {
        var guid = Guid.NewGuid();
        var sessionId = _sessionTokenRepository.CreateSessionCacheAsync(guid.ToString());
        _sessionTokenRepository.RemoveCacheSessionId(sessionId);
        var result = await _sessionTokenRepository.CheckSessionToken(sessionId);
        Assert.False(result.Success);
    }
}