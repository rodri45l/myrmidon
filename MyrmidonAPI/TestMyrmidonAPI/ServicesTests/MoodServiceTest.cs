using MyrmidonAPI.Dtos.Mood;
using MyrmidonAPI.Services.MoodService;

namespace TestMyrmidonAPI;

public class MoodServiceTest
{
    private readonly IMapper _mapper;
    private readonly Mood _mood;
    private readonly MoodRepository _moodRepository;
    private readonly MoodService _moodService;
    private readonly MyrmidonContext _myrmidonContext;
    private readonly Mood _testMood;

    public MoodServiceTest()
    {
        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });
        _mapper = mockMapper.CreateMapper();
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDbService")
            .Options;
        _myrmidonContext = new MyrmidonContext(options);
        _moodRepository = new MoodRepository(_myrmidonContext);
        _moodService = new MoodService(_mapper, _moodRepository);
        _testMood = new Mood
        {
            MoodId = 3,
            Id = new Guid(),
            Date = DateTime.Today,
            Rating = 2,
            User = new User()
        };
        _mood = new Mood();
    }

    [Fact]
    public async Task TestGetByMood_ShouldReturnSuccess()
    {
        await _moodRepository.AddAsync(_testMood);

        var result = await _moodService.GetMood(_testMood.MoodId);
        Assert.True(result.Success);
        _myrmidonContext.Moods.Remove(_testMood);
    }

    [Fact]
    public async Task TestGetByMood_ShouldReturnOk()
    {
        await _moodRepository.AddAsync(_testMood);

        var result = await _moodService.GetMood(_testMood.MoodId);
        Assert.IsType<OkObjectResult>(result.Data);
        _myrmidonContext.Moods.Remove(_testMood);
    }

    [Fact]
    public async Task TestGetByMood_ShouldReturnFail()
    {
        await _moodRepository.AddAsync(_testMood);

        var result = await _moodService.GetMood(_testMood.MoodId + 1);
        Assert.False(result.Success);
        _myrmidonContext.Moods.Remove(_testMood);
    }

    [Fact]
    public async Task TestGetByMood_ShouldReturnNotFound()
    {
        var result = await _moodService.GetMood(_testMood.MoodId + 5);
        Assert.IsType<NotFoundResult>(result.Data);
    }

    [Fact]
    public async Task TestAddMood_ShouldReturnOkObjectResult()
    {
        var mood = new AddMoodDto
        {
            Rating = 3,
            Date = DateTime.Now,
            UserId = Guid.NewGuid()
        };
        var result = await _moodService.AddMood(mood);
        Assert.IsType<OkObjectResult>(result.Data);
    }

    [Fact]
    public async Task TestAddMood_ShouldReturnSuccess()
    {
        var mood = new AddMoodDto
        {
            Rating = 3,
            Date = DateTime.Now,
            UserId = Guid.NewGuid()
        };
        var result = await _moodService.AddMood(mood);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateMood_ShouldReturnSuccess()
    {
        var mood = new Mood
        {
            Rating = 3,
            Date = DateTime.Now,
            Id = new Guid(),
            MoodId = 32
        };
        await _moodRepository.AddAsync(mood);

        var result = await _moodService.UpdateMood(mood);
        await _moodRepository.DeleteAsync(mood);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateMood_ShouldReturnFail()
    {
        var mood = new Mood
        {
            Rating = 3,
            Date = DateTime.Now,
            Id = new Guid(),
            MoodId = 33
        };

        var result = await _moodService.UpdateMood(mood);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestRemoveMood_ShouldReturnFail()
    {
        var result = await _moodService.RemoveMood(33);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestRemoveMood_ShouldReturnSuccess()
    {
        var mood = new Mood
        {
            Rating = 3,
            Date = DateTime.Now,
            Id = new Guid(),
            MoodId = 33
        };
        await _moodRepository.AddAsync(mood);
        var result = await _moodService.RemoveMood(mood.MoodId);
        Assert.True(result.Success);
    }
}