﻿namespace Api.Interface;

public interface IRepository<in T, K, U>
{
    Task<int> GetCount(string item);
    Task<bool> CreateOrSave(string item, T model);
    public Task<List<K>?> GetAll(string item);
    Task<List<K>?> GetLimit(string item, int currentPage, int limit);
    Task<K?> GetId(string item, int id);
    Task<List<K>?> GetField(string item, string namePurpose, string field);
    Task<List<K>?> GetLike(string item, string namePurpose, string field);
    Task<bool> DeleteId(string item, int id);
    Task<bool> UpdateId(string item, U model, int id);
    Task<bool> Search(string item, string namePurpose, string field);
}