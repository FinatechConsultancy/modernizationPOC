using Finatech.AccountManagement.Business;
using Finatech.AccountManagement.Model;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace Etx.Infrastructure.Service
{

    public class AccountService : IAccountService
    {
        private readonly AccountBusiness _accountBusiness;
        private readonly IDistributedCache _distributedCache;
        private readonly IMemoryCache _memoryCache;
        private readonly LegacyAccountService _legacyAccountService;

        public AccountService(AccountBusiness accountBusiness, IDistributedCache distributedCache,
            IMemoryCache memoryCache)
        {
            ArgumentNullException.ThrowIfNull(distributedCache, nameof(distributedCache));
            ArgumentNullException.ThrowIfNull(memoryCache, nameof(memoryCache));

            _accountBusiness = accountBusiness;
            _distributedCache = distributedCache;
            _memoryCache = memoryCache;
            _legacyAccountService = new LegacyAccountService();
        }
        
        public bool CreateAccount(string accountId, string accountHolderName)
        {
            return _accountBusiness.CreateAccount(accountId, accountHolderName);
        }

        public Account? GetAccount(string accountId)
        {
            return _accountBusiness.GetAccount(accountId);
        }

        public bool UpdateAccountHolderName(string accountId, string newAccountHolderName)
        {
            return _accountBusiness.UpdateAccountHolderName(accountId, newAccountHolderName);
        }
    }
}