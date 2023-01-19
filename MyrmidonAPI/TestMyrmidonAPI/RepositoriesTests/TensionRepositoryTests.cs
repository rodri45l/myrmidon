namespace TestMyrmidonAPI;

public class TensionRepositoryTests
{
    private readonly Tension _tension;
    private readonly TensionRepository _tensionRepository;
    private readonly TensionRepository _tensionRepository2;

    private readonly MyrmidonContext _myrmidonContext;
    private readonly MyrmidonContext _myrmidonContext2;
    private readonly Tension _testTension;


    public TensionRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDb")
            .Options;
        var options2 = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDb2")
            .Options;


        _testTension = new Tension
        {
            TensionId = 3,
            Id = new Guid(),
            Date = DateTime.Today,
            Rating = 2,
            User = new User()
        };

        _tension = new Tension();
        _myrmidonContext = new MyrmidonContext(options);
        _myrmidonContext2 = new MyrmidonContext(options2);
        _tensionRepository2 = new TensionRepository(_myrmidonContext2);
        _tensionRepository = new TensionRepository(_myrmidonContext);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnSuccess()
    {
        await _tensionRepository.AddAsync(_testTension);

        var result = await _tensionRepository.GetByIdAsync(_testTension.TensionId);
        Assert.True(result.Success);
        _myrmidonContext.Tensions.Remove(_testTension);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnTension()
    {
        await _tensionRepository.AddAsync(_testTension);

        var result = await _tensionRepository.GetByIdAsync(_testTension.TensionId);
        Assert.IsType<Tension>(result.Data);
        _myrmidonContext.Tensions.Remove(_testTension);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnFail()
    {
        await _tensionRepository.AddAsync(_testTension);

        var result = await _tensionRepository.GetByIdAsync(23);
        Assert.False(result.Success);
        _myrmidonContext.Tensions.Remove(_testTension);
    }

    [Fact]
    public async Task TestGetAllByUserIdAsync_ShouldReturnSuccess()
    {
        await _tensionRepository.AddAsync(_tension);

        var result = await _tensionRepository.GetAllByUserIdAsync(_tension.Id);
        Assert.True(result.Success);
        _myrmidonContext.Tensions.Remove(_tension);
    }

    [Fact]
    public async Task TestGetAllByUserIdAsync_ShouldReturnFail()
    {
        var result = await _tensionRepository2.GetAllByUserIdAsync(new Guid());

        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnSuccess()
    {
        var result = await _tensionRepository.AddAsync(_tension);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnFail()
    {
        var result = await _tensionRepository.AddAsync(_testTension);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestDeleteAsync_ShouldReturnSuccess()
    {
        await _tensionRepository.AddAsync(_tension);
        var result = await _tensionRepository.DeleteAsync(_tension);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestDeleteAsync_ShouldReturnFail()
    {
        var result = await _tensionRepository.DeleteAsync(new Tension());
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestUpdateAsync_ShouldReturnSuccess()
    {
        await _tensionRepository.AddAsync(_tension);
        var result = await _tensionRepository.UpdateAsync(_tension);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateAsync_ShouldReturnFail()
    {
        await _tensionRepository.AddAsync(_tension);

        var result = await _tensionRepository.UpdateAsync(_testTension);
        Assert.False(result.Success);
    }
}