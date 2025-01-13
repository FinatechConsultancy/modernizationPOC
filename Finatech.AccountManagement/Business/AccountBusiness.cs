using Finatech.AccountManagement.Model;

namespace Finatech.AccountManagement.Business;

public class AccountBusiness
{
    private static readonly Dictionary<string, Account> _accounts = new();
    
    public bool CreateAccount(string accountId, string accountHolderName)
    {
            
        if (_accounts.ContainsKey(accountId))
        {
            return false; // Account already exists
        }

        _accounts[accountId] = new Account
        {
            AccountId = accountId,
            AccountHolderName = accountHolderName,
            Balance = 0
        };
        return true;
    }

    public Account? GetAccount(string accountId)
    {
        if (_accounts.TryGetValue(accountId, out var accountInfo))
        {
            return accountInfo;
        }

        return null; // Account not found
    }

    public bool UpdateAccountHolderName(string accountId, string newAccountHolderName)
    {
        if (_accounts.TryGetValue(accountId, out var accountInfo))
        {
            accountInfo.AccountHolderName = newAccountHolderName;
            return true;
        }

        return false; // Account not found
    }
}