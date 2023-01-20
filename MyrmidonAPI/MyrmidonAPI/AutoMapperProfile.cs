using MyrmidonAPI.Dtos;
using MyrmidonAPI.Dtos.Fact;
using MyrmidonAPI.Dtos.Mood;

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
        CreateMap<Fact, AddFactDto>();
        CreateMap<Mood, AddMoodDto>();
        
    }
}