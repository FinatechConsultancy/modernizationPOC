using Etx.Infrastructure.Service;
using Etx.Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Etx.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController(ReflectionFactory reflectionFactory) : ControllerBase
{
    //Create Service instance with reflection for the sake of demonstration but also add it to the DI container
    //AccountInfoService accountInfoService = new AccountInfoService();
    private readonly AccountInfoService? _accountInfoService = reflectionFactory.CreateInstance(typeof(AccountInfoService)) as AccountInfoService;


    [HttpGet("{id}")]
    public IActionResult GetAccountInfo(string id)
    {
        
        var accountInfo = _accountInfoService!.GetAccountInfo(id);
        
        if (accountInfo == null)
        {
            return NotFound(new { Message = "Account not found" });
        }

        return Ok(accountInfo);
    }
    
    
    [HttpPost]
    public IActionResult CreateAccountInfo([FromBody] AccountInfo accountInfo)
    {
        AccountInfoService accountInfoService = new AccountInfoService();
        var result = accountInfoService.CreateAccount(accountInfo.AccountId, accountInfo.AccountHolderName);

        if (!result)
        {
            return BadRequest(new { Message = "Account already exists" });
        }

        return Ok(new { Message = "Account created successfully" });
    }
}