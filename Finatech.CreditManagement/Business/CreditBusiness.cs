using Finatech.CreditManagement.Model;

namespace Finatech.CreditManagement.Business;

public class CreditBusiness
{
    private static readonly Dictionary<string, CreditProduct> _creditProducts = new();

    public void AddCreditProduct(CreditProduct creditProduct)
    {
        if (!_creditProducts.ContainsKey(creditProduct.AccountId))
        {
            _creditProducts[creditProduct.AccountId] = creditProduct;
        }
    }
    

    public CreditProduct? GetCreditProduct(string accountId)
    {
        if (_creditProducts.TryGetValue(accountId, out var creditProduct))
        {
            return creditProduct;
        }
        return null;
    }

    public bool MakePayment(string accountId, decimal amount)
    {
        if (_creditProducts.TryGetValue(accountId, out var creditProduct))
        {
            if (amount <= creditProduct.Balance)
            {
                creditProduct.Balance -= amount;
                return true;
            }
        }
        return false;
    }

    public void ApplyInterest(string accountId)
    {
        if (_creditProducts.TryGetValue(accountId, out var creditProduct))
        {
            creditProduct.Balance += creditProduct.Balance * creditProduct.InterestRate;
        }
    }
}