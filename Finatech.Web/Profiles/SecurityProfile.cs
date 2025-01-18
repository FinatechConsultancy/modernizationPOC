
using AutoMapper;
using Finatech.Infrastructure.Model;
using Finatech.Security.Model;
using Finatech.Web.Dtos;

namespace Finatech.Web.Profiles;

public class SecurityProfile: Profile
{
    public SecurityProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserInfo>().ReverseMap();
    }
}