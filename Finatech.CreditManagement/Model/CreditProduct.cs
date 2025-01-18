namespace Finatech.CreditManagement.Model;

public class CreditProduct
{
    public string CreditId { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal InterestRate { get; set; }
    public decimal Balance { get; set; }

    public CreditProduct(string creditId, decimal creditLimit, decimal interestRate, decimal balance=0)
    {
        CreditId = creditId;
        CreditLimit = creditLimit;
        InterestRate = interestRate;
        Balance = balance;
    }
}