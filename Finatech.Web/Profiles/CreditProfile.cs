using AutoMapper;
using Finatech.Web.Dtos;
using Finatech.CreditManagement.Model;


namespace Finatech.Web.Profiles;

public class CreditProfile : Profile
{
    public CreditProfile()
    {
        CreateMap<CreditProduct, CreditProductDto>().ReverseMap();
    }
}