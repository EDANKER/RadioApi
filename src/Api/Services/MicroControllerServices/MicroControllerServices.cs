using Api.Interface;
using Api.Model.RequestModel.MicroController;
using Api.Model.ResponseModel.MicroController;
using Api.Services.HebrideanCacheServices;
using Api.Services.JsonServices;

namespace Api.Services.MicroControllerServices;

public interface IMicroControllerServices
{
    Task<bool> CreateOrSave(string item, MicroController microController);
    Task<List<DtoMicroController>?> GetFloor(string item, int floor);
    Task<DtoMicroController?> GetId(string item, int id);
    Task<bool> DeleteId(string item, int id);
    Task<bool> Update(string item, MicroController microController, int id);
    Task<bool> Search(string item, string name, string field);
}

public class MicroControllerServices(
    IJsonServices<DtoMicroController?> jsonServices,
    IRepository<MicroController, DtoMicroController, MicroController> controllerRepository,
    IHebrideanCacheServices hebrideanCacheServices)
    : IMicroControllerServices
{
    public async Task<bool> CreateOrSave(string item, MicroController microController)
    {
        if (!await controllerRepository.CreateOrSave(item, microController))
            return false;

        List<DtoMicroController>? dtoMicroController =
            await controllerRepository.GetString(item, microController.Name, "Name");
        if (dtoMicroController != null)
        {
            foreach (var data in dtoMicroController)
            {
                string? json = jsonServices.SerJson(data);
                if (json != null)
                    return await hebrideanCacheServices.Put(data.Id.ToString(), json);
            }
        }

        return false;
    }

    public async Task<List<DtoMicroController>?> GetFloor(string item, int floor)
    {
        return await controllerRepository.GetLimit(item, floor);
    }

    public async Task<DtoMicroController?> GetId(string item, int id)
    {
        string? dtoMicroController = await hebrideanCacheServices.GetId(id.ToString());

        if (dtoMicroController != default)
            return jsonServices.DesJson(dtoMicroController);
        
        return await controllerRepository.GetId(item, id);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        bool isDelete = await hebrideanCacheServices.DeleteId(id.ToString());
        if (!isDelete)
            return await controllerRepository.DeleteId(item, id);
        return false;
    }

    public async Task<bool> Update(string item, MicroController microController, int id)
    {
        return await controllerRepository.UpdateId(item, microController, id);
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        return await controllerRepository.Search(item, name, field);
    }
}