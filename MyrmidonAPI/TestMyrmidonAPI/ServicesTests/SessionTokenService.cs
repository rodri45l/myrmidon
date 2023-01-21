namespace TestMyrmidonAPI;

public class SessionTokenService
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
}