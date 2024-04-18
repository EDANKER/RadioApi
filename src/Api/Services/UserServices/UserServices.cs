using Api.Data.Repository.User;
using Api.Model.RequestModel.User;
using Api.Model.ResponseModel.User;

namespace Api.Services.UserServices;

public interface IUserServices
{
    Task<bool> CreateOrSave(string item, User user);
    Task<List<DtoUser>?> GetLimit(string item, int limit);
    Task<DtoUser?> GetId(string item, int id);
    Task<DtoUser?> GetName(string item, string name);
    Task<bool> DeleteId(string item, int id);
    Task<bool> Update(string item, User user, int id);
    Task<bool> Search(string item, string name, string login);
}

public class UserServices(IUserRepository userRepository) : IUserServices
{
    public async Task<bool> CreateOrSave(string item, User user)
    {
        return await userRepository.CreateOrSave(item, user);
    }

    public async Task<List<DtoUser>?> GetLimit(string item, int limit)
    {
        return await userRepository.GetLimit(item, limit);
    }

    public async Task<DtoUser?> GetId(string item, int id)
    {
        return await userRepository.GetId(item, id);
    }

    public async Task<DtoUser?> GetName(string item, string name)
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