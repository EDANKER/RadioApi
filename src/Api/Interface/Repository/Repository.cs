namespace Api.Interface.Repository;

public interface IRepository<in TReq, TRes, TReqU>
{
    Task<int> GetCount(string item);
    Task<TRes?> CreateOrSave(string item, TReq model);
    public Task<List<TRes>?> GetAll(string item);
    Task<List<TRes>?> GetLimit(string item, int currentPage, int limit);
    Task<TRes?> GetId(string item, int id);
    Task<TRes?> GetField(string item, string namePurpose, string field);
    Task<List<TRes>?> GetLike(string item, string namePurpose, string field);
    Task<bool> DeleteId(string item, int id);
    Task<TRes?> UpdateId(string item, TReqU model, int id);
    Task<bool> Search(string item, string namePurpose, string field);
}