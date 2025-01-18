using Finatech.Security.Business;
using Finatech.Security.Model;

namespace Finatech.Security.Service;

public class UserService : IUserService
{
    private readonly UserBusiness _userBusiness;

    public UserService(UserBusiness userBusiness)
    {
        _userBusiness = userBusiness;
    }

    public void AddUser(User user)
    {
        _userBusiness.AddUser(user);
    }
    
    public User? GetUser(string userId)
    {
        return _userBusiness.GetUser(userId);
    }
    
    public void UpdateUser(User user)
    {
        _userBusiness.UpdateUser(user);
    }

    public void DeleteUser(string userId)
    {
        _userBusiness.DeleteUser(userId);
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userBusiness.GetAllUsers();
    }
}