using Alachisoft.NCache.Common.Util;
using Finatech.CreditManagement.Model;

namespace Finatech.CreditManagement.Business;

public class CreditBusiness
{
    private static readonly Dictionary<string, CreditProduct> _creditProducts = new()
    {
        {"1", new CreditProduct("1", 50000m, 0.2m, 1000)},
        {"2", new CreditProduct("2", 10000m, 0.1m, 2000)},
    };

    public void AddCreditProduct(CreditProduct creditProduct)
    {
        if (!_creditProducts.ContainsKey(creditProduct.CreditId))
        {
            _creditProducts[creditProduct.CreditId] = creditProduct;
        }
    }
    

    public CreditProduct? GetCreditProduct(string creditId)
    {
        if (_creditProducts.TryGetValue(creditId, out var creditProduct))
        {
            return creditProduct;
        }
        return null;
    }

    
    public bool MakePayment(string creditId, decimal amount)
    {
        if (_creditProducts.TryGetValue(creditId, out var creditProduct))
        {
            if (amount <= creditProduct.Balance)
            {
                creditProduct.Balance -= amount;
                return true;
            }
        }
        return false;
    }

    public void ApplyInterest(string creditId)
    {
        if (_creditProducts.TryGetValue(creditId, out var creditProduct))
        {
            creditProduct.Balance += creditProduct.Balance * creditProduct.InterestRate;
        }
    }
    
    public IEnumerable<CreditProduct> GetAllCreditProducts()
    {
        return _creditProducts.Values;
    }
}