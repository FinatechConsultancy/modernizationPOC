using Finatech.AccountManagement.Model;

namespace Finatech.Infrastructure.Service;

public interface IAccountService
{
    bool CreateAccount(string accountId, string accountHolderName);
    Account? GetAccount(string accountId);
    bool UpdateAccountHolderName(string accountId, string newAccountHolderName);
}