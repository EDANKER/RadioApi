using Api.Data.Repository.User;
using Api.Data.Repository.UserRole;
using Api.Model.RequestModel.User;
using Api.Model.ResponseModel.User;

namespace Api.Services.UserServices;

public interface IUserServices
{
    public Task<bool> CreateOrSave(string item, User user);
    public Task<List<GetUser>> GetLimitUser(string item, int limit);
    public Task<List<GetUser>> GetIdUser(string item, int id);
    public Task<List<GetUser>> GetName(string item, string name);
    public Task<bool> DeleteId(string item, int id);
    public Task<bool> Update(string item, User user, int id);
    public Task<bool> Search(string item, string name, string login);
}

public class UserServices(IUserRepository userRepository, IUserRoleRepository userRoleRepository) : IUserServices
{
    public async Task<bool> CreateOrSave(string item, User user)
    {
        return await userRepository.CreateOrSave(item, user);
    }

    public async Task<List<GetUser>> GetLimitUser(string item, int limit)
    {
        return await userRepository.GetLimit(item, limit);
    }

    public async Task<List<GetUser>> GetIdUser(string item, int id)
    {
        return await userRepository.GetId(item, id);
    }

    public async Task<List<GetUser>> GetName(string item, string name)
    {
        return await userRepository.GetName(item, name);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        return await userRepository.DeleteId(item, id);
    }

    public async Task<bool> Update(string item, User user, int id)
    {
        return await userRepository.Update(item, user, id);
    }

    public async Task<bool> Search(string item, string name, string login)
    {
        return await userRepository.Search(item, name, login);
    }
}