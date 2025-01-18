namespace Finatech.Security.Model;

public class User
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime LastLogin { get; set; }
}