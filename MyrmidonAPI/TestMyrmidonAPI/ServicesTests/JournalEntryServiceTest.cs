using MyrmidonAPI.Dtos.JournalEntry;
using MyrmidonAPI.Services.JournalEntryService;

namespace TestMyrmidonAPI;

public class JournalEntryServiceTest
{
    private readonly IMapper _mapper;
    private readonly JournalEntry _journalEntry;
    private readonly JournalEntryRepository _journalEntryRepository;
    private readonly JournalEntryService _journalEntryService;
    private readonly MyrmidonContext _myrmidonContext;
    private readonly JournalEntry _testJournalEntry;

    public JournalEntryServiceTest()
    {
        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });
        _mapper = mockMapper.CreateMapper();
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDbService")
            .Options;
        _myrmidonContext = new MyrmidonContext(options);
        _journalEntryRepository = new JournalEntryRepository(_myrmidonContext);
        _journalEntryService = new JournalEntryService(_mapper, _journalEntryRepository);
        _testJournalEntry = new JournalEntry
        {
            JournalEntryId = 3,
            Id = new Guid(),
            Date = DateTime.Today,
            JournalEntry1 = "Test entry",
            User = new User()
        };
        _journalEntry = new JournalEntry();
    }

    [Fact]
    public async Task TestGetByJournalEntry_ShouldReturnSuccess()
    {
        await _journalEntryRepository.AddAsync(_testJournalEntry);

        var result = await _journalEntryService.GetJournalEntry(_testJournalEntry.JournalEntryId);
        Assert.True(result.Success);
        _myrmidonContext.JournalEntries.Remove(_testJournalEntry);
    }

    [Fact]
    public async Task TestGetByJournalEntry_ShouldReturnOk()
    {
        await _journalEntryRepository.AddAsync(_testJournalEntry);

        var result = await _journalEntryService.GetJournalEntry(_testJournalEntry.JournalEntryId);
        Assert.IsType<OkObjectResult>(result.Data);
        _myrmidonContext.JournalEntries.Remove(_testJournalEntry);
    }

    [Fact]
    public async Task TestGetByJournalEntry_ShouldReturnFail()
    {
        await _journalEntryRepository.AddAsync(_testJournalEntry);

        var result = await _journalEntryService.GetJournalEntry(_testJournalEntry.JournalEntryId + 1);
        Assert.False(result.Success);
        _myrmidonContext.JournalEntries.Remove(_testJournalEntry);
    }

    [Fact]
    public async Task TestGetByJournalEntry_ShouldReturnNotFound()
    {
        var result = await _journalEntryService.GetJournalEntry(_testJournalEntry.JournalEntryId + 5);
        Assert.IsType<NotFoundResult>(result.Data);
    }

    [Fact]
    public async Task TestAddJournalEntry_ShouldReturnOkObjectResult()
    {
        var journalEntry = new AddJournalEntryDto
        {
            JournalEntry1 = "Test",
            Date = DateTime.Now,
            Id = Guid.NewGuid()
        };
        var result = await _journalEntryService.AddJournalEntry(journalEntry);
        Assert.IsType<OkObjectResult>(result.Data);
    }

    [Fact]
    public async Task TestAddJournalEntry_ShouldReturnSuccess()
    {
        var journalEntry = new AddJournalEntryDto
        {
            JournalEntry1 = "Test",
            Date = DateTime.Now,
            Id = Guid.NewGuid()
        };
        var result = await _journalEntryService.AddJournalEntry(journalEntry);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateJournalEntry_ShouldReturnSuccess()
    {
        var journalEntry = new JournalEntry
        {
            JournalEntryId = 36,
            JournalEntry1 = "asdfasdf",
            Date = DateTime.Now,
            Id = Guid.NewGuid(),
        };
        await _journalEntryRepository.AddAsync(journalEntry);

        var result = await _journalEntryService.UpdateJournalEntry(journalEntry);
        await _journalEntryRepository.DeleteAsync(journalEntry);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateJournalEntry_ShouldReturnFail()
    {
        var journalEntry =  _testJournalEntry;

        var result = await _journalEntryService.UpdateJournalEntry(journalEntry);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestRemoveJournalEntry_ShouldReturnFail()
    {
        var result = await _journalEntryService.RemoveJournalEntry(33);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestRemoveJournalEntry_ShouldReturnSuccess()
    {
        var journalEntry = new JournalEntry
        {
            JournalEntryId = 32,
            JournalEntry1 = "asdfasdf",
            Date = DateTime.Now,
            Id = Guid.NewGuid(),
        };
        await _journalEntryRepository.AddAsync(journalEntry);
        var result = await _journalEntryService.RemoveJournalEntry(journalEntry.JournalEntryId);
        Assert.True(result.Success);
    }
}