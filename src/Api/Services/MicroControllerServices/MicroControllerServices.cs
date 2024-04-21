using Api.Data.Repository.HebrideanCacheRepository;
using Api.Interface;
using Api.Model.RequestModel.MicroController;
using Api.Model.ResponseModel.MicroController;
using Api.Services.JsonServices;

namespace Api.Services.MicroControllerServices;

public interface IMicroControllerServices
{
    Task<bool> CreateOrSave(string item, MicroController microController);
    Task<List<DtoMicroController>?> GetLimit(string item, int limit);
    Task<DtoMicroController?> GetId(string item, int id);
    Task<bool> DeleteId(string item, int id);
    Task<bool> Update(string item, MicroController microController, int id);
    Task<bool> Search(string item, string name);
}

public class MicroControllerServices(
    IJsonServices<DtoMicroController?> jsonServices,
    IRepository<MicroController, DtoMicroController> controllerRepository,
    IHebrideanCacheServices<DtoMicroController> hebrideanCacheServices)
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

    public async Task<List<DtoMicroController>?> GetLimit(string item, int limit)
    {
        List<DtoMicroController> dtoMicroControllers = new List<DtoMicroController>();
        for (int i = 0; i < limit; i++)
        {
            DtoMicroController? dtoMicroController =
                jsonServices.DesJson((await hebrideanCacheServices.GetLimit(i.ToString())));
            if (dtoMicroController != null)
            {
                dtoMicroControllers.Add(dtoMicroController);
                return dtoMicroControllers;
            }
        }

        return await controllerRepository.GetLimit(item, limit);
    }

    public async Task<DtoMicroController?> GetId(string item, int id)
    {
        DtoMicroController? dtoMicroController =
            jsonServices.DesJson(await hebrideanCacheServices.GetId(id.ToString()));

        if (dtoMicroController != default)
            return dtoMicroController;

        return await controllerRepository.GetId(item, id);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        bool isReturnTrueDb = await controllerRepository.DeleteId(item, id);
        if (!isReturnTrueDb)
            return isReturnTrueDb;

        return await hebrideanCacheServices.DeleteId(id.ToString());
    }

    public async Task<bool> Update(string item, MicroController microController, int id)
    {
        return await controllerRepository.UpdateId(item, microController, id);
    }

    public async Task<bool> Search(string item, string name)
    {
        return await controllerRepository.Search(item, name);
    }
}