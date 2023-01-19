using System.Runtime.InteropServices.JavaScript;

namespace TestMyrmidonAPI;

public class AppointmentRepositoryTests
{
    
     private readonly Appointment _appointment;
    private readonly AppointmentRepository _appointmentRepository;
    private readonly AppointmentRepository _appointmentRepository2;

    private readonly MyrmidonContext _myrmidonContext;
    private readonly MyrmidonContext _myrmidonContext2;
    private readonly Appointment _testAppointment;
    private readonly User _user;


    public AppointmentRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDbApp")
            .Options;
        var options2 = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDbApp2")
            .Options;
        


        _testAppointment = new Appointment
        {
            AppointmentId = 3,
            Date = DateTime.Today,
            Notes = "asdfasdfasdfasfdas"
        };

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
        _appointment = new Appointment()
        {
            AppointmentId = 3,
        };
        _myrmidonContext = new MyrmidonContext(options);
        _myrmidonContext2 = new MyrmidonContext(options2);
        _appointmentRepository2 = new AppointmentRepository(_myrmidonContext2);
        _appointmentRepository = new AppointmentRepository(_myrmidonContext);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnSuccess()
    {
        await _appointmentRepository.AddAsync(_testAppointment);

        var result = await _appointmentRepository.GetByIdAsync(_testAppointment.AppointmentId);
        Assert.True(result.Success);
        _myrmidonContext.Appointments.Remove(_testAppointment);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnAppointment()
    {
        await _appointmentRepository.AddAsync(_testAppointment);

        var result = await _appointmentRepository.GetByIdAsync(_testAppointment.AppointmentId);
        Assert.IsType<Appointment>(result.Data);
        _myrmidonContext.Appointments.Remove(_testAppointment);
    }

    [Fact]
    public async Task TestGetByIdAsync_ShouldReturnFail()
    {
        await _appointmentRepository.AddAsync(_testAppointment);

        var result = await _appointmentRepository.GetByIdAsync(23);
        Assert.False(result.Success);
        _myrmidonContext.Appointments.Remove(_testAppointment);
    }

    [Fact]
    public async Task TestGetAllByUserIdAsync_ShouldReturnSuccess()
    {
        
        await _myrmidonContext2.Users.AddAsync(_user);
        _testAppointment.Users.Add(_user);
        await _appointmentRepository2.AddAsync(_testAppointment);

        var result = _appointmentRepository2.GetAllByUserIdAsync(_user.Id);
        Assert.True(result.Success);
        
    }

    [Fact]
    public async Task TestGetAllByUserIdAsync_ShouldReturnFail()
    {
        var result = _appointmentRepository2.GetAllByUserIdAsync(_user.Id);

        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnSuccess()
    {
        var result = await _appointmentRepository.AddAsync(new Appointment
        {
            AppointmentId = 5,
            Date = DateTime.Now,
            Notes = "adfasdfasdf"
        });
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestAddAsync_ShouldReturnFail()
    {
        await _appointmentRepository.AddAsync(_testAppointment);
        var result = await _appointmentRepository.AddAsync(_testAppointment);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestDeleteAsync_ShouldReturnSuccess()
    {
        await _appointmentRepository.AddAsync(_appointment);
        var result = await _appointmentRepository.DeleteAsync(_appointment);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestDeleteAsync_ShouldReturnFail()
    {
        var result = await _appointmentRepository.DeleteAsync(new Appointment());
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestUpdateAsync_ShouldReturnSuccess()
    {
        await _appointmentRepository.AddAsync(_appointment);
        var result = await _appointmentRepository.UpdateAsync(_appointment);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateAsync_ShouldReturnFail()
    {
        await _appointmentRepository.AddAsync(_appointment);

        var result = await _appointmentRepository.UpdateAsync(_testAppointment);
        Assert.False(result.Success);
    }
}