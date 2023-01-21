using MyrmidonAPI.Dtos.Fact;
using MyrmidonAPI.Services.FactService;

namespace TestMyrmidonAPI;

public class FactServiceTest
{
    private readonly IMapper _mapper;
    private readonly MyrmidonContext _myrmidonContext;
    private readonly FactRepository _factRepository;
    private readonly FactService _factService;
    private readonly Fact _testFact;
    private readonly Fact _fact;

    public FactServiceTest()
    {
        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });
        _mapper = mockMapper.CreateMapper();
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDbSerFact")
            .Options;
        _myrmidonContext = new MyrmidonContext(options);
        _factRepository = new FactRepository(_myrmidonContext);
        _factService = new FactService( _factRepository, _mapper);
        _testFact = new Fact
        {
            FactId = 3,
            Fact1 = "",
            LastShown = DateTime.Now
        };
        _fact = new Fact();
    }
    
    
    [Fact]
    public async Task TestGetByFact_ShouldReturnSuccess()
    {
        await _factRepository.AddAsync(_testFact);

        var result = await _factService.GetFact(_testFact.FactId);
        Assert.True(result.Success);
        _myrmidonContext.Facts.Remove(_testFact);
    }

    [Fact]
    public async Task TestGetByFact_ShouldReturnOk()
    {
        await _factRepository.AddAsync(_testFact);

        var result = await _factService.GetFact(_testFact.FactId);
        Assert.IsType<OkObjectResult>(result.Data);
        _myrmidonContext.Facts.Remove(_testFact);
    }

    [Fact]
    public async Task TestGetByFact_ShouldReturnFail()
    {
        await _factRepository.AddAsync(_testFact);

        var result = await _factService.GetFact(_testFact.FactId + 1);
        Assert.False(result.Success);
        _myrmidonContext.Facts.Remove(_testFact);
    }

    [Fact]
    public async Task TestGetByFact_ShouldReturnNotFound()
    {
        var result = await _factService.GetFact(_testFact.FactId + 5);
        Assert.IsType<NotFoundResult>(result.Data);
    }

    [Fact]
    public async Task TestAddFact_ShouldReturnOkObjectResult()
    {
        var fact = new AddFactDto
        {
            Fact1 = "adfasdfasdf",
            LastShown = DateTime.Now,
            
        };
        var result = await _factService.AddFact(fact);
        Assert.IsType<OkObjectResult>(result.Data);
    }

    [Fact]
    public async Task TestAddFact_ShouldReturnSuccess()
    {
        var fact = new AddFactDto
        {
            Fact1 = "adfasdfasdf",
            LastShown = DateTime.Now,
            
        };
        var result = await _factService.AddFact(fact);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateFact_ShouldReturnSuccess()
    {
        var fact = _testFact;
        await _factRepository.AddAsync(fact);

        var result = await _factService.UpdateFact(fact);
        await _factRepository.DeleteAsync(fact);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateFact_ShouldReturnFail()
    {
        var fact = _testFact;

        var result = await _factService.UpdateFact(fact);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestRemoveFact_ShouldReturnFail()
    {
        var result = await _factService.RemoveFact(33);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestRemoveFact_ShouldReturnSuccess()
    {
        var fact = _testFact;
        await _factRepository.AddAsync(fact);
        var result = await _factService.RemoveFact(fact.FactId);
        Assert.True(result.Success);
    }
    
    
}