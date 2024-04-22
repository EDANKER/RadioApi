using Api.Data.Repository.User;
using Api.Interface;
using Api.Model.RequestModel.Music;
using Api.Model.RequestModel.User;
using Api.Model.ResponseModel.Music;
using Api.Model.ResponseModel.User;

namespace Api.Services.UserServices;

public interface IUserServices
{
    Task<bool> CreateOrSave(string item, User user);
    Task<List<DtoUser>?> GetLimit(string item, int limit);
    Task<DtoUser?> GetId(string item, int id);
    Task<bool> DeleteId(string item, int id);
    Task<bool> UpdateId(string item, User user, int id);
    Task<bool> Search(string item, string name, string field);
}

public class UserServices(IRepository<User, DtoUser> userRepository) : IUserServices
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

    public async Task<bool> DeleteId(string item, int id)
    {
        return await userRepository.DeleteId(item, id);
    }

    public async Task<bool> UpdateId(string item, User user, int id)
    {
        return await userRepository.UpdateId(item, user, id);
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        return await userRepository.Search(item, name, field);
    }
}