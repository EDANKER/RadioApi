namespace Api.Interface;

public interface IRepository<in T, K, U>
{
    Task<bool> CreateOrSave(string item, T model);
    public Task<List<K>?> GetAll(string item);
    Task<List<K>?> GetFloor(string item, int floor);
    Task<K?> GetId(string item, int id);
    Task<List<K>?> GetUni(string item, string namePurpose, string field);
    Task<List<K>?> GetLike(string item, string namePurpose, string field);
    Task<bool> DeleteId(string item, int id);
    Task<bool> UpdateId(string item, U model, int id);
    Task<bool> Search(string item, string namePurpose, string field);
}