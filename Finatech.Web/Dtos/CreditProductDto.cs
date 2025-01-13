namespace Finatech.Web.Dtos;

public class CreditProductDto
{
    public required string AccountId { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal InterestRate { get; set; }
    public decimal Balance { get; set; }
}