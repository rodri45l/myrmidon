using MyrmidonAPI.Dtos.Tension;
using MyrmidonAPI.Services.TensionService;

namespace TestMyrmidonAPI;

public class TensionServiceTest
{
    private readonly IMapper _mapper;
    private readonly Tension _tension;
    private readonly TensionRepository _tensionRepository;
    private readonly TensionService _tensionService;
    private readonly MyrmidonContext _myrmidonContext;
    private readonly Tension _testTension;

    public TensionServiceTest()
    {
        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });
        _mapper = mockMapper.CreateMapper();
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryTension")
            .Options;
        _myrmidonContext = new MyrmidonContext(options);
        _tensionRepository = new TensionRepository(_myrmidonContext);
        _tensionService = new TensionService(_mapper, _tensionRepository);
        _testTension = new Tension
        {
            TensionId = 3,
            Id = new Guid(),
            Date = DateTime.Today,
            Rating = 2,
            User = new User()
        };
        _tension = new Tension();
    }

    [Fact]
    public async Task TestGetByTension_ShouldReturnSuccess()
    {
        await _tensionRepository.AddAsync(_testTension);

        var result = await _tensionService.GetTension(_testTension.TensionId);
        Assert.True(result.Success);
        _myrmidonContext.Tensions.Remove(_testTension);
    }

    [Fact]
    public async Task TestGetByTension_ShouldReturnOk()
    {
        await _tensionRepository.AddAsync(_testTension);

        var result = await _tensionService.GetTension(_testTension.TensionId);
        Assert.IsType<OkObjectResult>(result.Data);
        _myrmidonContext.Tensions.Remove(_testTension);
    }

    [Fact]
    public async Task TestGetByTension_ShouldReturnFail()
    {
        await _tensionRepository.AddAsync(_testTension);

        var result = await _tensionService.GetTension(_testTension.TensionId + 1);
        Assert.False(result.Success);
        _myrmidonContext.Tensions.Remove(_testTension);
    }

    [Fact]
    public async Task TestGetByTension_ShouldReturnNotFound()
    {
        var result = await _tensionService.GetTension(_testTension.TensionId + 5);
        Assert.IsType<NotFoundResult>(result.Data);
    }

    [Fact]
    public async Task TestAddTension_ShouldReturnOkObjectResult()
    {
        var tension = new AddTensionDto
        {
            Rating = 3,
            Date = DateTime.Now,
            UserId = Guid.NewGuid()
        };
        var result = await _tensionService.AddTension(tension);
        Assert.IsType<OkObjectResult>(result.Data);
    }

    [Fact]
    public async Task TestAddTension_ShouldReturnSuccess()
    {
        var tension = new AddTensionDto
        {
            Rating = 3,
            Date = DateTime.Now,
            UserId = Guid.NewGuid()
        };
        var result = await _tensionService.AddTension(tension);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateTension_ShouldReturnSuccess()
    {
        var tension = new Tension
        {
            Rating = 3,
            Date = DateTime.Now,
            Id = new Guid(),
            TensionId = 32
        };
        await _tensionRepository.AddAsync(tension);

        var result = await _tensionService.UpdateTension(tension);
        await _tensionRepository.DeleteAsync(tension);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateTension_ShouldReturnFail()
    {
        var tension = new Tension
        {
            Rating = 3,
            Date = DateTime.Now,
            Id = new Guid(),
            TensionId = 33
        };

        var result = await _tensionService.UpdateTension(tension);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestRemoveTension_ShouldReturnFail()
    {
        var result = await _tensionService.RemoveTension(33);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestRemoveTension_ShouldReturnSuccess()
    {
        var tension = new Tension
        {
            Rating = 3,
            Date = DateTime.Now,
            Id = new Guid(),
            TensionId = 33
        };
        await _tensionRepository.AddAsync(tension);
        var result = await _tensionService.RemoveTension(tension.TensionId);
        Assert.True(result.Success);
    }
    
}