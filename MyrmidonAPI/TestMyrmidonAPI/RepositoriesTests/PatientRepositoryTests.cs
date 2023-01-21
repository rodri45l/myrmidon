namespace TestMyrmidonAPI;

public class PatientRepositoryTests
{
    private readonly MyrmidonContext _myrmidonContext;
    private readonly MyrmidonContext _myrmidonContext2;
    private readonly Patient _patient;
    private readonly PatientRepository _patientRepository;
    private readonly PatientRepository _patientRepository2;
    private readonly User _testUser;
    private readonly User _user;

    public PatientRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDbPatient")
            .Options;
        var options2 = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDbPatient2")
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
        _patient = new Patient
        {
            PatientId = new Guid(),
            Id = _user.Id,
            User = _user
        };
        _myrmidonContext = new MyrmidonContext(options);
        _myrmidonContext2 = new MyrmidonContext(options2);
        _patientRepository2 = new PatientRepository(_myrmidonContext2);
        _patientRepository = new PatientRepository(_myrmidonContext);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnSuccess()
    {
        await _myrmidonContext.Users.AddAsync(_user);
        await _myrmidonContext.Patients.AddAsync(_patient);
        var result = await _patientRepository.GetByIdAsync(_patient.PatientId);
        Assert.True(result.Success);
        _myrmidonContext.Patients.Remove(_patient);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnFail()
    {
        var result = await _patientRepository.GetByIdAsync(new Guid());
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldAddUser()
    {
        await _myrmidonContext.Patients.AddAsync(_patient);
        var result = await _patientRepository.GetByIdAsync(_patient.PatientId);

        Assert.True(result.Data.PatientId == _patient.PatientId);
    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnSuccess()
    {
        var result = await _patientRepository.AddAsync(_patient);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnFail()
    {
        await _patientRepository.AddAsync(_patient);
        var result = await _patientRepository.AddAsync(new Patient { PatientId = _patient.PatientId });
        await _patientRepository.DeleteAsync(_patient);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestUpdateAsync_ShouldReturnSuccess()
    {
        var result = await _patientRepository.UpdateAsync(_patient);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateAsync_ShouldReturnFail()
    {
        await _patientRepository.AddAsync(_patient);
        var result = await _patientRepository.UpdateAsync(new Patient { PatientId = _patient.PatientId });
        await _patientRepository.DeleteAsync(_patient);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestDeleteAsync_ShouldReturnSuccess()
    {
        await _patientRepository.AddAsync(_patient);
        var result = await _patientRepository.DeleteAsync(_patient);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestDeleteAsync_ShouldReturnFail()
    {
        var result = await _patientRepository.DeleteAsync(new Patient { PatientId = _patient.PatientId });
        Assert.False(result.Success);
    }
}