namespace TestMyrmidonAPI;

public class UserRepositoryTest
{
    private readonly MyrmidonContext _myrmidonContext;
    private readonly MyrmidonContext _myrmidonContext2;
    private readonly User _testUser;
    private readonly User _user;
    private readonly UserRepository _userRepository;
    private readonly UserRepository _userRepository2;


    public UserRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDb")
            .Options;
        var options2 = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDb2")
            .Options;

        _testUser = new User
        {
            UserName = "testUser",
            Id = new Guid()
        };

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
        _myrmidonContext = new MyrmidonContext(options);
        _myrmidonContext2 = new MyrmidonContext(options2);
        _userRepository2 = new UserRepository(_myrmidonContext2);
        _userRepository = new UserRepository(_myrmidonContext);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnSuccess()
    {
        await _userRepository.AddAsync(_testUser);

        var result = await _userRepository.GetByIdAsync(_testUser.Id);
        Assert.True(result.Success);
        _myrmidonContext.Users.Remove(_testUser);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnUser()
    {
        await _userRepository.AddAsync(_testUser);

        var result = await _userRepository.GetByIdAsync(_testUser.Id);
        Assert.IsType<User>(result.Data);
        _myrmidonContext.Users.Remove(_testUser);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnFail()
    {
        await _userRepository.AddAsync(_testUser);

        var result = await _userRepository.GetByIdAsync(new Guid());
        Assert.False(result.Success);
        _myrmidonContext.Users.Remove(_testUser);
    }

    [Fact]
    public async Task TestGetAllAsync_ShouldReturnSuccess()
    {
        await _userRepository.AddAsync(_user);

        var result = await _userRepository.GetAllAsync();
        Assert.True(result.Success);
        _myrmidonContext.Users.Remove(_user);
    }

    [Fact]
    public async Task TestGetAllAsync_ShouldReturnFail()
    {
        var result = await _userRepository2.GetAllAsync();

        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnSuccess()
    {
        var result = await _userRepository.AddAsync(_user);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnFail()
    {
        var result = await _userRepository.AddAsync(_testUser);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestDeleteAsync_ShouldReturnSuccess()
    {
        await _userRepository.AddAsync(_user);
        var result = await _userRepository.DeleteAsync(_user);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestDeleteAsync_ShouldReturnFail()
    {
        var result = await _userRepository.DeleteAsync(new User());
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestUpdateAsync_ShouldReturnSuccess()
    {
        await _userRepository.AddAsync(_user);
        var result = await _userRepository.UpdateAsync(_user);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateAsync_ShouldReturnFail()
    {
        var result = await _userRepository.UpdateAsync(new User());
        Assert.False(result.Success);
    }
}