using System.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Radio.Data.Repository;
using Radio.Data.Repository.User;
using Radio.Model.User;

namespace Radio.Services.AdminPanelServices;

public interface IAdminPanelServices
{
    public Task<List<User>> GetLimitUser(int limit);
    public List<User> GetIdUser(int id);
    public List<User> GetName(string name);
}

public class UserServices : IAdminPanelServices
{
    private List<User> _users;
    private GetUser _user;
    private IUserRepository _userRepository;

    public async Task<List<User>> GetLimitUser(int limit)
    {
        _users = new List<User>();
        DbDataReader reader = await _userRepository.GetLimit("User", limit);

        if (reader.HasRows)
        {
            while (await reader.ReadAsync())
            {
                int id = reader.GetInt32(0);
                string[] role = [reader.GetString(0)];
                string name = reader.GetString(0);
                bool speak = reader.GetBoolean(0);
                bool settingsTime = reader.GetBoolean(0);
                bool settingsUser = reader.GetBoolean(0);
                bool turnOnMusic = reader.GetBoolean(0);
                bool createPlayList = reader.GetBoolean(0);
                bool saveMusic = reader.GetBoolean(0);
                bool settingsScinaria = reader.GetBoolean(0);
                bool turnOnSector = reader.GetBoolean(0);

                _user = new GetUser(id, role, name, speak, settingsTime, settingsUser, turnOnMusic,
                    createPlayList, saveMusic, settingsScinaria, turnOnSector);
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