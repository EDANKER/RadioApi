using Microsoft.AspNetCore.Authorization;
using Radio.Model.User;

namespace Radio.Services.AdminPanelServices;

public interface IAdminPanelServices
{
    public List<User> GetLimitUser();
    public List<User> GetIdUser(int id);
}

public class AdminPanelServices : IAdminPanelServices
{
    public List<User> GetLimitUser()
    {
        throw new NotImplementedException();
    }

    public List<User> GetIdUser(int id)
    {
        throw new NotImplementedException();
    }
}