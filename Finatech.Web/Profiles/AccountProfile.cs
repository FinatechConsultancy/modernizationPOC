using AutoMapper;
using Finatech.AccountManagement.Model;
using Finatech.Web.Dtos;
using Finatech.CreditManagement.Model;


namespace Finatech.Web.Profiles;

public class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<Account, AccountDto>().ReverseMap();
    }
}