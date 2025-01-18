using Finatech.CreditManagement.Business;
using Finatech.CreditManagement.Model;
using Finatech.Infrastructure.Attributes;
using Finatech.Infrastructure.Cache;

namespace Finatech.CreditManagement.Service;


public class CreditService:ICreditService
{
    private readonly CreditBusiness _creditBusiness;

    public CreditService(CreditBusiness creditBusiness)
    {
        _creditBusiness = creditBusiness;
    }

    //First InvalidateCacheableAttribute is redundant, as it is new record,
    //it is not in cache but just to show how to use multiple InvalidateCacheableAttribute
    [InvalidateCacheableAttribute("creditProduct.creditId", cacheKeyPrefix:"CreditService.GetCreditProduct")]
    [InvalidateCacheableAttribute("", cacheKeyPrefix:"CreditService.GetAllCreditProducts")]
    public void AddCreditProduct(CreditProduct creditProduct)
    {
        _creditBusiness.AddCreditProduct(creditProduct);
    }

    
    [Cacheable("creditId", 300, CacheType.Memory)]
    public CreditProduct? GetCreditProduct(string creditId)
    {
        return _creditBusiness.GetCreditProduct(creditId);
    }
    
    [InvalidateCacheableAttribute("creditId", CacheType.Memory, cacheKeyPrefix:"CreditService.GetCreditProduct")]
    public bool MakePayment(string creditId, decimal amount)
    {
        return _creditBusiness.MakePayment(creditId, amount);
    }

    [InvalidateCacheableAttribute("creditId", CacheType.Memory, cacheKeyPrefix:"CreditService.GetCreditProduct")]
    public void ApplyInterest(string creditId)
    {
        _creditBusiness.ApplyInterest(creditId);
    }
    
    [Cacheable("", 300, CacheType.Memory)]
    public IEnumerable<CreditProduct> GetAllCreditProducts()
    {
        return _creditBusiness.GetAllCreditProducts();
    }
}