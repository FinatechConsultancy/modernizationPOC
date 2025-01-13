using Finatech.CreditManagement.Model;

namespace Finatech.CreditManagement.Service;

public interface ICreditService
{
    void AddCreditProduct(CreditProduct creditProduct);
    CreditProduct? GetCreditProduct(string accountId);
    bool MakePayment(string accountId, decimal amount);
    void ApplyInterest(string accountId);
}