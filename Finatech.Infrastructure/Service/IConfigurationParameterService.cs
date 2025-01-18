using Finatech.Infrastructure.Model;

namespace Finatech.Infrastructure.Service;

public interface IConfigurationParameterService
{
    ConfigurationParameter? GetConfigurationParameter(string key);
    bool AddOrUpdateConfigurationParameter(ConfigurationParameter configurationParameter);
    bool RemoveConfigurationParameter(string key);
    IEnumerable<ConfigurationParameter> GetAllConfigurationParameters();
}