namespace TestMyrmidonAPI;

public class MoodRepositoryTest
{
    
    private  MyrmidonContext _myrmidonContext;
    private  MyrmidonContext _myrmidonContext2;
    private Mood _testMood;
    private readonly MoodRepository _moodRepository;
    private readonly MoodRepository _moodRepository2;
    private readonly Mood _mood;


    public  MoodRepositoryTest()
    {
        
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDb")
            .Options;
        var options2 = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDb2")
            .Options;
        

        _testMood = new Mood()
        {
            MoodId = 3,
            Id = new Guid(),
            Date = DateTime.Today,
            Rating = 2,
            User = new User()
            
            
        };
        
         _mood = new Mood()
        {

        };
        _myrmidonContext = new MyrmidonContext(options);
        _myrmidonContext2 = new MyrmidonContext(options2);
        _moodRepository2 = new MoodRepository(_myrmidonContext2);
        _moodRepository = new MoodRepository(_myrmidonContext);

    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnSuccess()
    {
        await _moodRepository.AddAsync(_testMood);

        var result = await _moodRepository.GetByIdAsync(_testMood.MoodId);
        Assert.True(result.Success); 
        _myrmidonContext.Moods.Remove(_testMood);


    }
    
    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnMood()
    {
        await _moodRepository.AddAsync(_testMood);

        var result = await _moodRepository.GetByIdAsync(_testMood.MoodId);
        Assert.IsType<Mood>(result.Data);
        _myrmidonContext.Moods.Remove(_testMood);
       
    }
    
    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnFail()
    {
        await _moodRepository.AddAsync(_testMood);

        var result = await _moodRepository.GetByIdAsync(23);
        Assert.False(result.Success);
        _myrmidonContext.Moods.Remove(_testMood);
    }
    
    [Fact]
    public async Task TestGetAllByUserIdAsync_ShouldReturnSuccess()
    {
        
        
        await _moodRepository.AddAsync(_mood);
        
        var result = await _moodRepository.GetAllByUserIdAsync(_mood.Id);
        Assert.True(result.Success);
        _myrmidonContext.Moods.Remove(_mood);

    }
    [Fact]
    public async Task TestGetAllByUserIdAsync_ShouldReturnFail()
    {

        var result = await _moodRepository2.GetAllByUserIdAsync(new Guid());
        
        Assert.False(result.Success);
        

    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnSuccess()
    {
        var result = await _moodRepository.AddAsync(_mood);
        Assert.True(result.Success);

    }
    [Fact]
    public async Task TestAddAsync_ShouldReturnFail()
    {
        var result = await _moodRepository.AddAsync(_testMood);
        Assert.False(result.Success);

    }
    
    [Fact]
    public async Task TestDeleteAsync_ShouldReturnSuccess()
    {
        await _moodRepository.AddAsync(_mood);
        var result = await _moodRepository.DeleteAsync(_mood);
        Assert.True(result.Success);

    }
    
    [Fact]
    public async Task TestDeleteAsync_ShouldReturnFail()
    {
        
        var result = await _moodRepository.DeleteAsync(new Mood());
        Assert.False(result.Success);

    }
    
    [Fact]
    public async Task TestUpdateAsync_ShouldReturnSuccess()
    {
        await _moodRepository.AddAsync(_mood);
        var result = await _moodRepository.UpdateAsync(_mood);
        Assert.True(result.Success);

    }
    
    [Fact]
    public async Task TestUpdateAsync_ShouldReturnFail()
    {
        await _moodRepository.AddAsync(_mood);

        var result = await _moodRepository.UpdateAsync(_testMood);
        Assert.False(result.Success);

    }

}