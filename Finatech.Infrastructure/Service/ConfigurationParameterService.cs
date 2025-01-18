using Finatech.Infrastructure.Attributes;
using Finatech.Infrastructure.Cache;

namespace Finatech.Infrastructure.Service;

using System.Collections.Concurrent;
using Finatech.Infrastructure.Model;


public class ConfigurationParameterService : IConfigurationParameterService
{
    private static readonly ConcurrentDictionary<string, ConfigurationParameter> _configurationParameterDictionary = new ConcurrentDictionary<string, ConfigurationParameter>
    {
        ["Param1"] = new ConfigurationParameter
        {
            Key = "Param1",
            Value = "Value1",
            Description = "Description for Param1",
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        },
        ["Param2"] = new ConfigurationParameter
        {
            Key = "Param2",
            Value = "Value2",
            Description = "Description for Param2",
            CreatedDate = DateTime.UtcNow,
            ModifiedDate = DateTime.UtcNow
        }
    };

    public ConfigurationParameter? GetConfigurationParameter(string key)
    {
        _configurationParameterDictionary.TryGetValue(key, out var configurationParameter);
        return configurationParameter;
    }

    public bool AddOrUpdateConfigurationParameter(ConfigurationParameter configurationParameter)
    {
        _configurationParameterDictionary.AddOrUpdate(configurationParameter.Key, configurationParameter, (key, existingVal) => configurationParameter);
        return true;
    }

    public bool RemoveConfigurationParameter(string key)
    {
        return _configurationParameterDictionary.TryRemove(key, out _);
    }
    
    [Cacheable("", 300, CacheType.Memory)]
    public IEnumerable<ConfigurationParameter> GetAllConfigurationParameters()
    {
        return _configurationParameterDictionary.Values;
    }
}
