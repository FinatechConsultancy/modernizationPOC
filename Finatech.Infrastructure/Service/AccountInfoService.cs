using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Etx.Infrastructure.Service
{
    public class AccountInfoService
    {
        private static readonly Dictionary<string, AccountInfo> _accounts = new();
        
        private readonly IDistributedCache _distributedCache;
        private readonly IMemoryCache _memoryCache;

        public AccountInfoService(IDistributedCache distributedCache, IMemoryCache memoryCache)
        {
            ArgumentNullException.ThrowIfNull(distributedCache, nameof(distributedCache));
            ArgumentNullException.ThrowIfNull(memoryCache, nameof(memoryCache));

            _distributedCache = distributedCache;
            _memoryCache = memoryCache;
        }
        
        public bool CreateAccount(string accountId, string accountHolderName)
        {
            
            if (_accounts.ContainsKey(accountId))
            {
                return false; // Account already exists
            }

            _accounts[accountId] = new AccountInfo
            {
                AccountId = accountId,
                AccountHolderName = accountHolderName,
                Balance = 0
            };
            return true;
        }

        public AccountInfo? GetAccountInfo(string accountId)
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

    public class AccountInfo
    {
        public string AccountId { get; set; }
        public string AccountHolderName { get; set; }
        public decimal Balance { get; set; }
    }
}