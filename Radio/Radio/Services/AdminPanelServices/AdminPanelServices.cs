using System.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Radio.Data.Repository;
using Radio.Model.User;

namespace Radio.Services.AdminPanelServices;

public interface IAdminPanelServices
{
    public Task<List<User>> GetLimitUser(int limit);
    public List<User> GetIdUser(int id);
    public List<User> GetName(string name);
}

public class AdminPanelServices : IAdminPanelServices
{
    private List<User> _users;
    private IUserRepository _userRepository;
    public async Task<List<User>> GetLimitUser(int limit)
    {
        _users = new List<User>();
        DbDataReader reader = await _userRepository.GetLimit("User", limit);

        if (reader.HasRows)
        {
            while (await reader.ReadAsync())
            {
                
            }
        }
        
        return _users;
    }

    public List<User> GetIdUser(int id)
    {
        _users = new List<User>();
        
        return _users;
    }

    public List<User> GetName(string name)
    {
        _users = new List<User>();

        return _users;
    }
}