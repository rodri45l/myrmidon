namespace MyrmidonAPI;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, GetUserDto>();
        CreateMap<AddUserDto, User>().ForSourceMember(x =>x.Password,
            opt => opt.DoNotValidate());
        CreateMap<UpdateUserDto, User>();
        CreateMap<GetUserDto, UpdateUserDto?>();
    }
}