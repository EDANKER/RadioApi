using Api.Interface;
using Api.Interface.Repository;
using Api.Model.RequestModel.MicroController;
using Api.Model.ResponseModel.MicroController;
using Api.Services.HebrideanCacheServices;
using Api.Services.HttpMicroControllerServices;
using Api.Services.JsonServices;

namespace Api.Services.MicroControllerServices;

public interface IMicroControllerServices
{
    Task<int> GetCountPage(string item, int currentPage, int limit);
    Task<bool> SoundVol(int[] idMicroControllers, int vol);
    Task<DtoFloorMicroController?> CreateOrSave(string item, MicroController microController);
    Task<List<DtoFloorMicroController>?> GetAll(string item);
    Task<List<DtoFloorMicroController>?> GetLimit(string item, int currentPage, int floor);
    Task<DtoFloorMicroController?> GetField(string item, string namePurpose, string field);
    Task<DtoFloorMicroController?> GetId(string item, int id);
    Task<bool> DeleteId(string item, int id);
    Task<DtoFloorMicroController?> UpdateId(string item, MicroController microController, int id);
    Task<bool> Search(string item, string name, string field);
}

public class MicroControllerServices(
    IJsonServices<DtoFloorMicroController?> dtoJsonServices,
    IJsonServices<MicroController> jsonServices,
    IHttpMicroControllerServices httpMicroControllerServices,
    IRepository<MicroController, DtoFloorMicroController, MicroController> controllerRepository,
    IHebrideanCacheServices hebrideanCacheServices)
    : IMicroControllerServices
{
    public async Task<int> GetCountPage(string item, int currentPage, int limit)
    {
        while (true)
        {
            List<DtoFloorMicroController>? list = await GetLimit(item, currentPage, limit);
            if (list != null)
                ++currentPage;
            else
                break;
        }

        return --currentPage;
    }

    public async Task<bool> SoundVol(int[] idMicroControllers, int vol)
    {
        foreach (var data in idMicroControllers)
        {
            DtoFloorMicroController? microController = await GetId("MicroControllers", data);
            if (microController != null)
                return await httpMicroControllerServices.PostVol(microController, vol);
        }

        return false;
    }

    public async Task<DtoFloorMicroController?> CreateOrSave(string item, MicroController microController)
    {
        DtoFloorMicroController? dtoMicroController = await controllerRepository.CreateOrSave(item, microController);

        if (dtoMicroController != null)
            if (await hebrideanCacheServices.Put(dtoMicroController.Id.ToString(),
                    dtoJsonServices.SerJson(dtoMicroController)))
                return dtoMicroController;

        return null;
    }

    public async Task<List<DtoFloorMicroController>?> GetAll(string item)
    {
        return await controllerRepository.GetAll(item);
    }

    public async Task<List<DtoFloorMicroController>?> GetLimit(string item, int currentPage, int floor)
    {
        return await controllerRepository.GetLimit(item, currentPage, floor);
    }

    public async Task<DtoFloorMicroController?> GetField(string item, string namePurpose, string field)
    {
        return await controllerRepository.GetField(item, namePurpose, item);
    }

    public async Task<DtoFloorMicroController?> GetId(string item, int id)
    {
        string? dtoMicroController = await hebrideanCacheServices.GetId(id.ToString());

        if (dtoMicroController != default)
            return dtoJsonServices.DesJson(dtoMicroController);

        return await controllerRepository.GetId(item, id);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        bool isDelete = await hebrideanCacheServices.DeleteId(id.ToString());
        if (isDelete)
            return await controllerRepository.DeleteId(item, id);
        return false;
    }

    public async Task<DtoFloorMicroController?> UpdateId(string item, MicroController microController, int id)
    {
        if (await hebrideanCacheServices.DeleteId(id.ToString()))
        {
            DtoFloorMicroController? dtoMicroController = await controllerRepository.UpdateId(item, microController, id);
            
            if (dtoMicroController != null)
            {
                 await hebrideanCacheServices.Put(id.ToString(),  jsonServices.SerJson(microController));
                 return dtoMicroController;
            }
        }

        return null;
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        return await controllerRepository.Search(item, name, field);
    }
}