using Finatech.CreditManagement.Model;
using Finatech.Infrastructure.Attributes;
using Finatech.Infrastructure.Cache;

namespace Finatech.CreditManagement.Service;

public interface ICreditService
{
    void AddCreditProduct(CreditProduct creditProduct);
    
    CreditProduct? GetCreditProduct(string creditId);
    bool MakePayment(string creditId, decimal amount);
    void ApplyInterest(string creditId);

    IEnumerable<CreditProduct> GetAllCreditProducts();
}