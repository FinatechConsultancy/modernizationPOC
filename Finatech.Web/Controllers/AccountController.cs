using AutoMapper;
using Etx.Infrastructure.Factories;
using Etx.Infrastructure.Service;
using Finatech.AccountManagement.Model;
using Finatech.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Etx.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(IDependencyReflectorFactory reflectionFactory, IMapper mapper) : ControllerBase
{
    //Create Service instance with reflection for the sake of demonstration but also add it to the DI container
    //AccountInfoService accountInfoService = new AccountInfoService();
    private readonly AccountService? _accountService = reflectionFactory.GetReflectedType<AccountService>(typeof(AccountService), null);
    private IMapper _mapper = mapper;
    
    
    [HttpPost]
    public IActionResult CreateAccount([FromBody] AccountDto accountDto)
    {
        var account = _mapper.Map<Account>(accountDto);
        var result = _accountService.CreateAccount(account.AccountId, account.AccountHolderName);

        if (!result)
        {
            return BadRequest(new { Message = "Account already exists" });
        }

        return Ok(new { Message = "Account created successfully" });
    }
    
    [HttpGet("{id}")]
    public IActionResult GetAccount(string id)
    {
        
        var account = _accountService!.GetAccount(id);
        
        if (account == null)
        {
            return NotFound(new { Message = "Account not found" });
        }

        var accountDto = _mapper.Map<AccountDto>(account);
        return Ok(account);
    }

}