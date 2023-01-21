namespace TestMyrmidonAPI.RepositoriesTests;

public class JournalRepositoryTests
{
    private readonly JournalEntry _journal;
    private readonly JournalEntryRepository _journalRepository;
    private readonly JournalEntryRepository _journalRepository2;

    private readonly MyrmidonContext _myrmidonContext;
    private readonly MyrmidonContext _myrmidonContext2;
    private readonly JournalEntry _testJournal;


    public JournalRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDb32")
            .Options;
        var options2 = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDb33")
            .Options;


        _testJournal = new JournalEntry
        {
            JournalEntryId = 1,
            JournalEntry1 = "asdfasdfasdfasfd",
            Date = DateTime.Today,
            Id = new Guid(),
            User = new User()
        };

        _journal = new JournalEntry
        {
            JournalEntryId = 0
        };
        _myrmidonContext = new MyrmidonContext(options);
        _myrmidonContext2 = new MyrmidonContext(options2);
        _journalRepository2 = new JournalEntryRepository(_myrmidonContext2);
        _journalRepository = new JournalEntryRepository(_myrmidonContext);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnSuccess()
    {
        await _journalRepository.AddAsync(_testJournal);

        var result = await _journalRepository.GetByIdAsync(_testJournal.JournalEntryId);

        Assert.True(result.Success);
        _myrmidonContext.JournalEntries.Remove(_testJournal);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnJournal()
    {
        await _journalRepository.AddAsync(_testJournal);

        var result = await _journalRepository.GetByIdAsync(_testJournal.JournalEntryId);
        Assert.IsType<JournalEntry>(result.Data);
        _myrmidonContext.JournalEntries.Remove(_testJournal);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnFail()
    {
        await _journalRepository.AddAsync(_testJournal);

        var result = await _journalRepository.GetByIdAsync(23);
        Assert.False(result.Success);
        _myrmidonContext.JournalEntries.Remove(_testJournal);
    }

    [Fact]
    public async Task TestGetAllByUserIdAsync_ShouldReturnSuccess()
    {
        var user = new User
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
        _testJournal.Id = user.Id;
        _testJournal.User = user;
        var resultdeb = await _journalRepository.UpdateAsync(_testJournal);
        var resutlErr = resultdeb.Error;

        var result = await _journalRepository.GetAllByUserIdAsync(_testJournal.Id);
        Assert.True(result.Success);
        _myrmidonContext.JournalEntries.Remove(_testJournal);
    }

    [Fact]
    public async Task TestGetAllByUserIdAsync_ShouldReturnFail()
    {
        var result = await _journalRepository2.GetAllByUserIdAsync(new Guid());

        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnSuccess()
    {
        var user = new User
        {
            Name = "Rodrigo",
            Surname = "Lara Rodriguez",
            UserName = "Rodri45z",

            BirthDate = DateTime.Now,
            PostalCode = "29139",
            Email = "rodri45esp@gmail.com",
            Address = "asdfadfasdf asdf",
            PhoneNumber = "+460793570919",
            Sex = false,
            Gender = "Male",
            PasswordHash = "SecurePassword123@"
        };
        _testJournal.Id = user.Id;
        _testJournal.User = user;

        await _myrmidonContext.Users.AddAsync(user);
        await _journalRepository.DeleteAsync(_testJournal);
        var result = await _journalRepository.AddAsync(_testJournal);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnFail()
    {
        var result = await _journalRepository.AddAsync(_testJournal);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestDeleteAsync_ShouldReturnSuccess()
    {
        await _journalRepository.AddAsync(_journal);
        var result = await _journalRepository.DeleteAsync(_journal);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestDeleteAsync_ShouldReturnFail()
    {
        var result = await _journalRepository.DeleteAsync(new JournalEntry());
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestUpdateAsync_ShouldReturnSuccess()
    {
        var user = new User
        {
            UserName = "testUser",
            Email = "test@test.com",
            PhoneNumber = "null",
            Id = new Guid(),
            Name = "test",
            Surname = "test",
            BirthDate = DateTime.Now,
            PostalCode = "null",
            Address = "null",
            Sex = false,
            Gender = "null"
        };
        await _myrmidonContext2.Users.AddAsync(user);
        _testJournal.User = user;
        await _journalRepository2.AddAsync(_testJournal);
        var result = await _journalRepository2.UpdateAsync(_testJournal);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateAsync_ShouldReturnFail()
    {
        await _journalRepository.AddAsync(_journal);

        var result = await _journalRepository.UpdateAsync(_testJournal);
        Assert.False(result.Success);
    }
}