namespace Finatech.CreditManagement.Model;

public class CreditProduct
{
    public string AccountId { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal InterestRate { get; set; }
    public decimal Balance { get; set; }

    public CreditProduct(string accountId, decimal creditLimit, decimal interestRate)
    {
        AccountId = accountId;
        CreditLimit = creditLimit;
        InterestRate = interestRate;
        Balance = 0;
    }
}