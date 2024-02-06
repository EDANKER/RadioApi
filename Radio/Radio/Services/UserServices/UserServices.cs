using System.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Radio.Data.Repository;
using Radio.Data.Repository.User;
using Radio.Model.RequestModel.User;
using Radio.Model.ResponseModel.User;

namespace Radio.Services.AdminPanelServices;

public interface IUserServices
{
    public Task<List<GetUser>> GetLimitUser(string item, int limit);
    public Task<List<GetUser>> GetIdUser(string item, int id);
    public Task<List<GetUser>> GetName(string item, string name);
}

public class UserServices : IUserServices
{
    private IUserRepository _userRepository;

    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<GetUser>> GetLimitUser(string item, int limit)
    {
        return await _userRepository.GetLimit(item, limit);
    }

    public async Task<List<GetUser>> GetIdUser(string item, int id)
    {
        return await _userRepository.GetId(item, id);
    }

    public async Task<List<GetUser>> GetName(string item, string name)
    {
        return await _userRepository.GetName(item, name);
    }
}