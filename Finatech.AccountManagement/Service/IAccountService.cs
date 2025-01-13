using Finatech.AccountManagement.Model;

namespace Etx.Infrastructure.Service;

public interface IAccountService
{
    bool CreateAccount(string accountId, string accountHolderName);
    Account? GetAccount(string accountId);
    bool UpdateAccountHolderName(string accountId, string newAccountHolderName);
}