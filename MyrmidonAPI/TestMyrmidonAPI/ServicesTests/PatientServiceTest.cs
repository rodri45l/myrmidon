using MyrmidonAPI.Dtos.Patient;
using MyrmidonAPI.Services.PatientService;

namespace TestMyrmidonAPI;

public class PatientServiceTest
{
        
    private readonly IMapper _mapper;
    private readonly Patient _patient;
    private readonly PatientRepository _patientRepository;
    private readonly PatientService _patientService;
    private readonly MyrmidonContext _myrmidonContext;
    private readonly User _user;
    private readonly Patient _testPatient;


    public PatientServiceTest()
    {

        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });
        _mapper = mockMapper.CreateMapper();
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDbService")
            .Options;
        _myrmidonContext = new MyrmidonContext(options);
        _patientRepository = new PatientRepository(_myrmidonContext);
        _patientService = new PatientService(_mapper, _patientRepository);
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
        _testPatient = new Patient
        {
            PatientId = new Guid(),
            Id = _user.Id,
            User = _user
        };
        _patient = new Patient();
    }

    [Fact]
    public async Task TestGetByPatient_ShouldReturnSuccess()
    {
        await _patientRepository.AddAsync(_testPatient);

        var result = await _patientService.GetPatient(_testPatient.PatientId);
        Assert.True(result.Success);
        _myrmidonContext.Patients.Remove(_testPatient);
    }

    [Fact]
    public async Task TestGetByPatient_ShouldReturnOk()
    {
        await _patientRepository.AddAsync(_testPatient);

        var result = await _patientService.GetPatient(_testPatient.PatientId);
        Assert.IsType<OkObjectResult>(result.Data);
        _myrmidonContext.Patients.Remove(_testPatient);
    }

    [Fact]
    public async Task TestGetByPatient_ShouldReturnFail()
    {
        await _patientRepository.AddAsync(_testPatient);

        var result = await _patientService.GetPatient(Guid.NewGuid());
        Assert.False(result.Success);
        _myrmidonContext.Patients.Remove(_testPatient);
    }

    [Fact]
    public async Task TestGetByPatient_ShouldReturnNotFound()
    {
        var result = await _patientService.GetPatient(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(result.Data);
    }

    [Fact]
    public async Task TestAddPatient_ShouldReturnOkObjectResult()
    {
        var patient = new AddPatientDto
        {
            Id = Guid.NewGuid()
        };
        var result = await _patientService.AddPatient(patient);
        Assert.IsType<OkObjectResult>(result.Data);
    }

    [Fact]
    public async Task TestAddPatient_ShouldReturnSuccess()
    {
        var patient = new AddPatientDto
        {
            Id = Guid.NewGuid()
        };
        var result = await _patientService.AddPatient(patient);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdatePatient_ShouldReturnSuccess()
    {
        var patient = _testPatient;
        await _patientRepository.AddAsync(patient);

        var result = await _patientService.UpdatePatient(patient);
        await _patientRepository.DeleteAsync(patient);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdatePatient_ShouldReturnFail()
    {
        await _patientRepository.AddAsync(_patient);
        var result = await _patientService.UpdatePatient(new Patient { PatientId = _patient.PatientId });
        await _patientRepository.DeleteAsync(_patient);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestRemovePatient_ShouldReturnFail()
    {
        var result = await _patientService.RemovePatient(Guid.NewGuid());
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestRemovePatient_ShouldReturnSuccess()
    {
        var patient = _testPatient;
        await _patientRepository.AddAsync(patient);
        var result = await _patientService.RemovePatient(patient.PatientId);
        Assert.True(result.Success);
    }
    
}