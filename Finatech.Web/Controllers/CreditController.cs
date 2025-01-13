using AutoMapper;
using Finatech.CreditManagement.Model;
using Finatech.CreditManagement.Service;
using Finatech.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Etx.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CreditController : ControllerBase
{
    private readonly ICreditService _creditService;
    private readonly IMapper _mapper;

    public CreditController(ICreditService creditService, IMapper mapper)
    {
        _creditService = creditService;
        _mapper = mapper;
    }

    [HttpPost("add")]
    public IActionResult AddCreditProduct([FromBody] CreditProductDto creditProductDto)
    {
        var creditProduct = _mapper.Map<CreditProduct>(creditProductDto);
        _creditService.AddCreditProduct(creditProduct);
        return Ok(new { Message = "Credit created successfully" });
    }
    

    [HttpGet("get/{accountId}")]
    public IActionResult GetCreditProduct(string accountId)
    {
        var creditProduct = _creditService.GetCreditProduct(accountId);
        if (creditProduct == null)
        {
            return NotFound();
        }
        var creditProductDto = _mapper.Map<CreditProductDto>(creditProduct);
        return Ok(creditProductDto);
    }

    [HttpPost("make-payment")]
    public IActionResult MakePayment(string accountId, decimal amount)
    {
        var success = _creditService.MakePayment(accountId, amount);
        if (!success)
        {
            return BadRequest("Payment failed.");
        }
        return Ok();
    }

    [HttpPost("apply-interest")]
    public IActionResult ApplyInterest(string accountId)
    {
        _creditService.ApplyInterest(accountId);
        return Ok();
    }
}