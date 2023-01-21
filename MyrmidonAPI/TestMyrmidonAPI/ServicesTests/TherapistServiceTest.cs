using MyrmidonAPI.Dtos.Therapist;
using MyrmidonAPI.Services.TherapistService;

namespace TestMyrmidonAPI;

public class TherapistServiceTest
{
    
    private readonly IMapper _mapper;
    private readonly Therapist _therapist;
    private readonly TherapistRepository _therapistRepository;
    private readonly TherapistService _therapistService;
    private readonly MyrmidonContext _myrmidonContext;
    private readonly User _user;
    private readonly Therapist _testTherapist;


    public TherapistServiceTest()
    {

        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });
        _mapper = mockMapper.CreateMapper();
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDbService")
            .Options;
        _myrmidonContext = new MyrmidonContext(options);
        _therapistRepository = new TherapistRepository(_myrmidonContext);
        _therapistService = new TherapistService(_mapper, _therapistRepository);
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
        _testTherapist = new Therapist
        {
            TherapistId = new Guid(),
            Id = _user.Id,
            User = _user
        };
        _therapist = new Therapist();
    }

    [Fact]
    public async Task TestGetByTherapist_ShouldReturnSuccess()
    {
        await _therapistRepository.AddAsync(_testTherapist);

        var result = await _therapistService.GetTherapist(_testTherapist.TherapistId);
        Assert.True(result.Success);
        _myrmidonContext.Therapists.Remove(_testTherapist);
    }

    [Fact]
    public async Task TestGetByTherapist_ShouldReturnOk()
    {
        await _therapistRepository.AddAsync(_testTherapist);

        var result = await _therapistService.GetTherapist(_testTherapist.TherapistId);
        Assert.IsType<OkObjectResult>(result.Data);
        _myrmidonContext.Therapists.Remove(_testTherapist);
    }

    [Fact]
    public async Task TestGetByTherapist_ShouldReturnFail()
    {
        await _therapistRepository.AddAsync(_testTherapist);

        var result = await _therapistService.GetTherapist(Guid.NewGuid());
        Assert.False(result.Success);
        _myrmidonContext.Therapists.Remove(_testTherapist);
    }

    [Fact]
    public async Task TestGetByTherapist_ShouldReturnNotFound()
    {
        var result = await _therapistService.GetTherapist(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(result.Data);
    }

    [Fact]
    public async Task TestAddTherapist_ShouldReturnOkObjectResult()
    {
        var therapist = new AddTherapistDto
        {
            Id = Guid.NewGuid()
        };
        var result = await _therapistService.AddTherapist(therapist);
        Assert.IsType<OkObjectResult>(result.Data);
    }

    [Fact]
    public async Task TestAddTherapist_ShouldReturnSuccess()
    {
        var therapist = new AddTherapistDto
        {
            Id = Guid.NewGuid()
        };
        var result = await _therapistService.AddTherapist(therapist);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateTherapist_ShouldReturnSuccess()
    {
        var therapist = _testTherapist;
        await _therapistRepository.AddAsync(therapist);

        var result = await _therapistService.UpdateTherapist(therapist);
        await _therapistRepository.DeleteAsync(therapist);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateTherapist_ShouldReturnFail()
    {
        await _therapistRepository.AddAsync(_therapist);
        var result = await _therapistService.UpdateTherapist(new Therapist { TherapistId = _therapist.TherapistId });
        await _therapistRepository.DeleteAsync(_therapist);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestRemoveTherapist_ShouldReturnFail()
    {
        var result = await _therapistService.RemoveTherapist(Guid.NewGuid());
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestRemoveTherapist_ShouldReturnSuccess()
    {
        var therapist = _testTherapist;
        await _therapistRepository.AddAsync(therapist);
        var result = await _therapistService.RemoveTherapist(therapist.TherapistId);
        Assert.True(result.Success);
    }
    
}