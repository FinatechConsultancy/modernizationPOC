using Finatech.Infrastructure.Cache;
using Finatech.AccountManagement.Business;
using Finatech.AccountManagement.Model;
using Finatech.Infrastructure.Attributes;
using Finatech.Infrastructure.Model;
using Finatech.Infrastructure.Service;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Finatech.Infrastructure.Service
{

    public class AccountService : IAccountService
    {
        private readonly ILogger<ServiceContext> _logger;
        private readonly ServiceContext _serviceContext;
        private readonly AccountBusiness _accountBusiness;
        private readonly IDistributedCache _distributedCache;
        private readonly IMemoryCache _memoryCache;
        private readonly LegacyAccountService _legacyAccountService;
        

        public AccountService(
            ILogger<ServiceContext> logger,  
            ServiceContext serviceContext, 
            AccountBusiness accountBusiness, 
            IDistributedCache distributedCache,
            IMemoryCache memoryCache)
        {
            ArgumentNullException.ThrowIfNull(distributedCache, nameof(distributedCache));
            ArgumentNullException.ThrowIfNull(memoryCache, nameof(memoryCache));

            _logger = logger;
            _serviceContext = serviceContext;
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
            _logger.LogInformation("AccountService - GetAccountInformation called by {UserName}", _serviceContext.UserInfo?.UserName);
            return _accountBusiness.GetAccount(accountId);
        }

        public bool UpdateAccountHolderName(string accountId, string newAccountHolderName)
        {
            return _accountBusiness.UpdateAccountHolderName(accountId, newAccountHolderName);
        }
    }
}