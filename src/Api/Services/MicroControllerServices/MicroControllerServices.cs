using Api.Interface;
using Api.Model.RequestModel.MicroController;
using Api.Model.ResponseModel.MicroController;
using Api.Services.HebrideanCacheServices;
using Api.Services.JsonServices;
using Api.Services.MusicPlayerToMicroControllerServices;

namespace Api.Services.MicroControllerServices;

public interface IMicroControllerServices
{
    Task<bool> SoundVol(int[] idMicroControllers, int vol);
    Task<bool> CreateOrSave(string item, MicroController microController);
    Task<List<DtoMicroController>?> GetAll(string item);
    Task<List<DtoMicroController>?> GetFloor(string item, int floor);
    Task<DtoMicroController?> GetId(string item, int id);
    Task<bool> DeleteId(string item, int id);
    Task<bool> Update(string item, MicroController microController, int id);
    Task<bool> Search(string item, string name, string field);
}

public class MicroControllerServices(
    IJsonServices<DtoMicroController?> dtoJsonServices,
    IJsonServices<MicroController> jsonServices,
    IMusicPlayerToMicroControllerServices musicPlayerToMicroControllerServices,
    IRepository<MicroController, DtoMicroController, MicroController> controllerRepository,
    IHebrideanCacheServices hebrideanCacheServices)
    : IMicroControllerServices
{
    public async Task<bool> SoundVol(int[] idMicroControllers, int vol)
    {
        foreach (var data in idMicroControllers)
        {
            DtoMicroController? microController = await GetId("MicroControllers", data);
            if (microController != null)
                return await musicPlayerToMicroControllerServices.SoundVol(microController, vol);
        }

        return false;
    }

    public async Task<bool> CreateOrSave(string item, MicroController microController)
    {
        if (await controllerRepository.CreateOrSave(item, microController))
        {
            List<DtoMicroController>? dtoMicroController =
                await controllerRepository.GetUni(item, microController.Name, "Name");
            if (dtoMicroController != null)
                foreach (var data in dtoMicroController)
                    return await hebrideanCacheServices.Put(data.Id.ToString(), await dtoJsonServices.SerJson(data));
        }

        return false;
    }

    public async Task<List<DtoMicroController>?> GetAll(string item)
    {
        return await controllerRepository.GetAll(item);
    }

    public async Task<List<DtoMicroController>?> GetFloor(string item, int floor)
    {
        return await controllerRepository.GetFloor(item, floor);
    }

    public async Task<DtoMicroController?> GetId(string item, int id)
    {
        string? dtoMicroController = await hebrideanCacheServices.GetId(id.ToString());

        if (dtoMicroController != default)
            return await dtoJsonServices.DesJson(dtoMicroController);

        return await controllerRepository.GetId(item, id);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        bool isDelete = await hebrideanCacheServices.DeleteId(id.ToString());
        if (isDelete)
            return await controllerRepository.DeleteId(item, id);
        return false;
    }

    public async Task<bool> Update(string item, MicroController microController, int id)
    {
        if (await hebrideanCacheServices.DeleteId(id.ToString()))
        {
            if (await controllerRepository.UpdateId(item, microController, id))
            {
                return await hebrideanCacheServices.Put(id.ToString(), await jsonServices.SerJson(microController));
            }
        }

        return false;
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        return await controllerRepository.Search(item, name, field);
    }
}