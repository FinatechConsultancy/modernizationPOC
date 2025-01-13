using Finatech.CreditManagement.Business;
using Finatech.CreditManagement.Model;

namespace Finatech.CreditManagement.Service;


public class CreditService:ICreditService
{
    private readonly CreditBusiness _creditBusiness;

    public CreditService(CreditBusiness creditBusiness)
    {
        _creditBusiness = creditBusiness;
    }

    public void AddCreditProduct(CreditProduct creditProduct)
    {
        _creditBusiness.AddCreditProduct(creditProduct);
    }

    public CreditProduct? GetCreditProduct(string accountId)
    {
        return _creditBusiness.GetCreditProduct(accountId);
    }

    public bool MakePayment(string accountId, decimal amount)
    {
        return _creditBusiness.MakePayment(accountId, amount);
    }

    public void ApplyInterest(string accountId)
    {
        _creditBusiness.ApplyInterest(accountId);
    }
}