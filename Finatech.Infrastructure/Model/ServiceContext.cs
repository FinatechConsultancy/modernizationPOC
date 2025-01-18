namespace Finatech.Infrastructure.Model;

public class ServiceContext
{
    public UserInfo UserInfo { get; set; }
    public IEnumerable<ConfigurationParameter> ConfigurationParameters { get; set; }
}

public class UserInfo {
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
} 

