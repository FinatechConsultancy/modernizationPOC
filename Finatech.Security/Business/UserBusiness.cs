using Finatech.Security.Model;

namespace Finatech.Security.Business;

public class UserBusiness
{
    private static readonly Dictionary<string, User> _userStore = new()
    {
        {
            "1", new User() 
            {
                UserId = "1",
                UserName = "demoUser",
                Email = "demouser@finatech.com.tr"
            }
        }
    };

    public void AddUser(User user)
    {
        if (_userStore.ContainsKey(user.UserId))
        {
            throw new InvalidOperationException("User already exists.");
        }
        _userStore[user.UserId] = user;
    }

    public User? GetUser(string userId)
    {
        _userStore.TryGetValue(userId, out var user);
        return user;
    }

    public void UpdateUser(User user)
    {
        if (!_userStore.ContainsKey(user.UserId))
        {
            throw new KeyNotFoundException("User not found.");
        }
        _userStore[user.UserId] = user;
    }

    public void DeleteUser(string userId)
    {
        if (!_userStore.Remove(userId))
        {
            throw new KeyNotFoundException("User not found.");
        }
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _userStore.Values;
    }
}