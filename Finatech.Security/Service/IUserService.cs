using Finatech.Security.Model;

namespace Finatech.Security.Service;

public interface IUserService
{
    void AddUser(User user);
    User? GetUser(string userId);
    void UpdateUser(User user);
    void DeleteUser(string userId);
    IEnumerable<User> GetAllUsers();
}