using AutoMapper;

namespace LeafByte.Parking.API.Models;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}
