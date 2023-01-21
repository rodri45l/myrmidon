using System.Runtime.InteropServices.JavaScript;
using MyrmidonAPI.Dtos.Appointment;
using MyrmidonAPI.Services.AppointmentService;

namespace TestMyrmidonAPI;

public class AppointmentServiceTest
{
    private readonly Appointment _appointment;
    private readonly AppointmentRepository _appointmentRepository;
    private readonly AppointmentService _appointmentService;
    private readonly IMapper _mapper;
    private readonly MyrmidonContext _myrmidonContext;
    private readonly Appointment _testAppointment;

    public AppointmentServiceTest()
    {
        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfile()); });
        _mapper = mockMapper.CreateMapper();
        var options = new DbContextOptionsBuilder<MyrmidonContext>()
            .UseInMemoryDatabase("InMemoryDbSerAppointment")
            .Options;
        _myrmidonContext = new MyrmidonContext(options);
        _appointmentRepository = new AppointmentRepository(_myrmidonContext);
        _appointmentService = new AppointmentService(_appointmentRepository, _mapper);
        _testAppointment = new Appointment
        {
            AppointmentId = 0,
            Date = DateTime.Today,
            Notes = "asdfasdfasdf"
        };
        _appointment = new Appointment();
    }


    [Fact]
    public async Task TestGetByAppointment_ShouldReturnSuccess()
    {
        await _appointmentRepository.AddAsync(_testAppointment);

        var result = await _appointmentService.GetAppointment(_testAppointment.AppointmentId);
        Assert.True(result.Success);
        _myrmidonContext.Appointments.Remove(_testAppointment);
    }

    [Fact]
    public async Task TestGetByAppointment_ShouldReturnOk()
    {
        await _appointmentRepository.AddAsync(_testAppointment);

        var result = await _appointmentService.GetAppointment(_testAppointment.AppointmentId);
        Assert.IsType<OkObjectResult>(result.Data);
        _myrmidonContext.Appointments.Remove(_testAppointment);
    }

    [Fact]
    public async Task TestGetByAppointment_ShouldReturnFail()
    {
        await _appointmentRepository.AddAsync(_testAppointment);

        var result = await _appointmentService.GetAppointment(_testAppointment.AppointmentId + 1);
        Assert.False(result.Success);
        _myrmidonContext.Appointments.Remove(_testAppointment);
    }

    [Fact]
    public async Task TestGetByAppointment_ShouldReturnNotFound()
    {
        var result = await _appointmentService.GetAppointment(_testAppointment.AppointmentId + 5);
        Assert.IsType<NotFoundResult>(result.Data);
    }

    [Fact]
    public async Task TestAddAppointment_ShouldReturnOkObjectResult()
    {
        var appointment = new AddAppointmentDto
        {
            Date = DateTime.Now,
            Notes = "  "
        };
        var result = await _appointmentService.AddAppointment(appointment);
        Assert.IsType<OkObjectResult>(result.Data);
    }

    [Fact]
    public async Task TestAddAppointment_ShouldReturnSuccess()
    {
        var appointment = new AddAppointmentDto
        {
            Date = DateTime.Now,
            Notes = "  "
        };
        var result = await _appointmentService.AddAppointment(appointment);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateAppointment_ShouldReturnSuccess()
    {
        var appointment = _testAppointment;
        await _appointmentRepository.AddAsync(appointment);

        var result = await _appointmentService.UpdateAppointment(appointment);
        await _appointmentRepository.DeleteAsync(appointment);
        Assert.True(result.Success);
    }

    [Fact]
    public async Task TestUpdateAppointment_ShouldReturnFail()
    {
        var appointment = _testAppointment;
        await _appointmentRepository.AddAsync(_testAppointment);

        var result = await _appointmentService.UpdateAppointment(new Appointment(){AppointmentId = _testAppointment.AppointmentId});
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestRemoveAppointment_ShouldReturnFail()
    {
        var result = await _appointmentService.RemoveAppointment(33);
        Assert.False(result.Success);
    }

    [Fact]
    public async Task TestRemoveAppointment_ShouldReturnSuccess()
    {
        var appointment = _testAppointment;
        await _appointmentRepository.AddAsync(appointment);
        var result = await _appointmentService.RemoveAppointment(appointment.AppointmentId);
        Assert.True(result.Success);
    }
}