namespace MyrmidonAPI;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, GetUserDto>();
        CreateMap<AddUserDto, User>();
        CreateMap<UpdateUserDto, User>();
        CreateMap<GetUserDto, UpdateUserDto?>();
    }
}