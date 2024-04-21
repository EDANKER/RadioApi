namespace Api.Interface;

public interface IRepository<T, K>
{
    Task<bool> CreateOrSave(string item, T model);
    Task<List<K>?> GetLimit(string item, int floor);
    Task<K?> GetId(string item, int id);
    Task<List<K>?> GetString(string item, string namePurpose, string field);
    Task<bool> DeleteId(string item, int id);
    Task<bool> UpdateId(string item, T model, int id);
    Task<bool> Search(string item, string fullName);
}