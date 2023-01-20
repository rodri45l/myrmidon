using MyrmidonAPI.Dtos;
using MyrmidonAPI.Dtos.Appointment;
using MyrmidonAPI.Dtos.Fact;
using MyrmidonAPI.Dtos.JournalEntry;
using MyrmidonAPI.Dtos.Mood;
using MyrmidonAPI.Dtos.Patient;

namespace MyrmidonAPI;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, GetUserDto>();
        CreateMap<AddUserDto, User>().ForSourceMember(x => x.Password,
            opt => opt.DoNotValidate());
        CreateMap<UpdateUserDto, User>();
        CreateMap<GetUserDto, UpdateUserDto?>();
        CreateMap<AddFactDto, Fact>();
        CreateMap<AddMoodDto, Mood>();
        CreateMap<AddJournalEntryDto, JournalEntry>();
        CreateMap<AddPatientDto, Patient>();
        CreateMap<AddAppointmentDto, Appointment>();
    }
}