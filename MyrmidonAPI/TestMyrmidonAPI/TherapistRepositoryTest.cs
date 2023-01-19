namespace TestMyrmidonAPI;

public class TherapistRepositoryTest
{
    private readonly MyrmidonContext _myrmidonContext;
    private readonly MyrmidonContext _myrmidonContext2;
    private readonly User _testUser;
    private readonly Therapist _therapist;
    private readonly TherapistRepository _therapistRepository;
    private readonly TherapistRepository _therapistRepository2;
    private readonly User _user;

    public TherapistRepositoryTest()
    {
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDbThera")
            .Options;
        var options2 = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDbThera2")
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
        _therapist = new Therapist
        {
            TherapistId = new Guid(),
            Id = _user.Id,
            User = _user
        };
        _myrmidonContext = new MyrmidonContext(options);
        _myrmidonContext2 = new MyrmidonContext(options2);
        _therapistRepository2 = new TherapistRepository(_myrmidonContext2);
        _therapistRepository = new TherapistRepository(_myrmidonContext);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnSuccess()
    {
        await _myrmidonContext.Users.AddAsync(_user);
        await _myrmidonContext.Therapists.AddAsync(_therapist);
        var result = await _therapistRepository.GetByIdAsync(_therapist.TherapistId);
        Assert.True(result.Success);
        _myrmidonContext.Therapists.Remove(_therapist);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnFail()
    {
        var result = await _therapistRepository.GetByIdAsync(new Guid());
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldAddUser()
    {
        await _myrmidonContext.Therapists.AddAsync(_therapist);
        var result = await _therapistRepository.GetByIdAsync(_therapist.TherapistId);

        Assert.True(result.Data.TherapistId == _therapist.TherapistId);
    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnSuccess()
    {
        var result = await _therapistRepository.AddAsync(_therapist);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnFail()
    {
        await _therapistRepository.AddAsync(_therapist);
        var result = await _therapistRepository.AddAsync(new Therapist { TherapistId = _therapist.TherapistId });
        await _therapistRepository.DeleteAsync(_therapist);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestUpdateAsync_ShouldReturnSuccess()
    {
        var result = await _therapistRepository.UpdateAsync(_therapist);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateAsync_ShouldReturnFail()
    {
        await _therapistRepository.AddAsync(_therapist);
        var result = await _therapistRepository.UpdateAsync(new Therapist { TherapistId = _therapist.TherapistId });
        await _therapistRepository.DeleteAsync(_therapist);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestDeleteAsync_ShouldReturnSuccess()
    {
        await _therapistRepository.AddAsync(_therapist);
        var result = await _therapistRepository.DeleteAsync(_therapist);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestDeleteAsync_ShouldReturnFail()
    {
        var result = await _therapistRepository.DeleteAsync(new Therapist { TherapistId = _therapist.TherapistId });
        Assert.False(result.Success);
    }
}