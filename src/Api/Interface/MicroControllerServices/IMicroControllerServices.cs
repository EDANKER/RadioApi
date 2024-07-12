namespace Api.Interface.MicroControllerServices;

public interface IMicroControllerServices<TReq, TRes>
{
    Task<TRes?> CreateOrSave(string item, TReq microController);
    Task<List<TRes>?> GetAll(string item);
    Task<List<TRes>?> GetLimit(string item, int currentPage, int floor);
    Task<TRes?> GetField(string item, string namePurpose, string field);
    Task<TRes?> GetId(string item, int id);
    Task<bool> DeleteId(string item, int id);
    Task<TRes?> UpdateId(string item, TReq microController, int id);
    Task<bool> Search(string item, string name, string field);
}