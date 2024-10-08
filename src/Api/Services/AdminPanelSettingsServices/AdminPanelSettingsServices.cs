﻿using Api.Interface;
using Api.Interface.Repository;
using Api.Model.RequestModel.User;
using Api.Model.ResponseModel.Music;
using Api.Model.ResponseModel.User;

namespace Api.Services.UserServices;

public interface IUserServices
{
    Task<int> GetCountPage(string item, int currentPage, int limit);
    Task<DtoUser?> CreateOrSave(string item, User user);
    Task<List<DtoUser>?> GetLimit(string item, int currentPage, int limit);
    Task<DtoUser?> GetId(string item, int id);
    Task<bool> DeleteId(string item, int id);
    Task<DtoUser?> UpdateId(string item, User user, int id);
    Task<bool> Search(string item, string name, string field);
}

public class AdminPanelSettingsServices(IRepository<User, DtoUser, User> userRepository) : IUserServices
{
    public async Task<int> GetCountPage(string item, int currentPage, int limit)
    {
        while (true)
        {
            List<DtoUser>? list = await GetLimit(item, currentPage, limit);
            if (list != null)
                ++currentPage;
            else
                break;
        }

        return --currentPage;
    }

    public async Task<DtoUser?> CreateOrSave(string item, User user)
    {
        return await userRepository.CreateOrSave(item, user);
    }

    public async Task<List<DtoUser>?> GetLimit(string item, int currentPage, int limit)
    {
        return await userRepository.GetLimit(item, currentPage, limit);
    }

    public async Task<DtoUser?> GetId(string item, int id)
    {
        return await userRepository.GetId(item, id);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        return await userRepository.DeleteId(item, id);
    }

    public async Task<DtoUser?> UpdateId(string item, User user, int id)
    {
        return await userRepository.UpdateId(item, user, id);
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        return await userRepository.Search(item, name, field);
    }
}