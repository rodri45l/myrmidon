namespace TestMyrmidonAPI.RepositoriesTests;

public class FactRepositoryTests
{
    private readonly Fact _fact;
    private readonly FactRepository _factRepository;
    private readonly FactRepository _factRepository2;

    private readonly MyrmidonContext _myrmidonContext;
    private readonly MyrmidonContext _myrmidonContext2;
    private readonly Fact _testFact;


    public FactRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryFactDb")
            .Options;
        var options2 = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryFactDb2")
            .Options;


        _testFact = new Fact
        {
            FactId = 3,
            Fact1 = string.Empty,
            LastShown = DateTime.Now
        };

        _fact = new Fact
        {
            FactId = 3
        };
        _myrmidonContext = new MyrmidonContext(options);
        _myrmidonContext2 = new MyrmidonContext(options2);
        _factRepository2 = new FactRepository(_myrmidonContext2);
        _factRepository = new FactRepository(_myrmidonContext);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnSuccess()
    {
        await _factRepository.AddAsync(_testFact);

        var result = await _factRepository.GetByIdAsync(_testFact.FactId);
        Assert.True(result.Success);
        _myrmidonContext.Facts.Remove(_testFact);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnFact()
    {
        await _factRepository.AddAsync(_testFact);

        var result = await _factRepository.GetByIdAsync(_testFact.FactId);
        Assert.IsType<Fact>(result.Data);
        _myrmidonContext.Facts.Remove(_testFact);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnFail()
    {
        await _factRepository.AddAsync(_testFact);

        var result = await _factRepository.GetByIdAsync(23);
        Assert.False(result.Success);
        _myrmidonContext.Facts.Remove(_testFact);
    }

    [Fact]
    public async Task TestGetAllAsync_ShouldReturnSuccess()
    {
        await _factRepository.AddAsync(_fact);

        var result = await _factRepository.GetAllAsync();
        Assert.True(result.Success);
        _myrmidonContext.Facts.Remove(_fact);
    }

    [Fact]
    public async Task TestGetAllByUserIdAsync_ShouldReturnFail()
    {
        var result = await _factRepository2.GetAllAsync();

        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnSuccess()
    {
        var result = await _factRepository2.AddAsync(_testFact);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnFail()
    {
        var result = await _factRepository.AddAsync(_fact);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestDeleteAsync_ShouldReturnSuccess()
    {
        await _factRepository.AddAsync(_fact);
        var result = await _factRepository.DeleteAsync(_fact);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestDeleteAsync_ShouldReturnFail()
    {
        var result = await _factRepository.DeleteAsync(new Fact());
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestUpdateAsync_ShouldReturnSuccess()
    {
        await _factRepository.AddAsync(_fact);
        var result = await _factRepository.UpdateAsync(_fact);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateAsync_ShouldReturnFail()
    {
        await _factRepository.AddAsync(_fact);

        var result = await _factRepository.UpdateAsync(_testFact);
        Assert.False(result.Success);
    }
}