using Api.Interface.MicroControllerServices;
using Api.Interface.Repository;
using Api.Model.RequestModel.MicroController;
using Api.Model.ResponseModel.MicroController;
using Api.Services.HebrideanCacheServices;
using Api.Services.HttpMicroControllerServices;
using Api.Services.JsonServices;

namespace Api.Services.MicroControllerServices;

public class MicroControllerServices(
    IJsonServices<DtoMicroController?> dtoJsonServices,
    IJsonServices<Model.RequestModel.MicroController.MicroController> jsonServices,
    IHttpMicroControllerServices httpMicroControllerServices,
    IRepository<Model.RequestModel.MicroController.MicroController, DtoMicroController, Model.RequestModel.MicroController.MicroController> controllerRepository,
    IHebrideanCacheServices hebrideanCacheServices)
    : IMicroControllerServices<Model.RequestModel.MicroController.MicroController, DtoMicroController>
{
    public async Task<int> GetCountPage(string item, int currentPage, int limit)
    {
        while (true)
        {
            List<DtoMicroController>? list = await GetLimit(item, currentPage, limit);
            if (list != null)
                ++currentPage;
            else
                break;
        }

        return --currentPage;
    }

    public async Task<DtoMicroController?> CreateOrSave(string item, Model.RequestModel.MicroController.MicroController microController)
    {
        DtoMicroController? dtoMicroController = await controllerRepository.CreateOrSave(item, microController);

        if (dtoMicroController != null)
            if (await hebrideanCacheServices.Put(dtoMicroController.Id.ToString(),
                    dtoJsonServices.SerJson(dtoMicroController)))
                return dtoMicroController;

        return null;
    }

    public async Task<List<DtoMicroController>?> GetAll(string item)
    {
        return await controllerRepository.GetAll(item);
    }

    public async Task<List<DtoMicroController>?> GetLimit(string item, int currentPage, int floor)
    {
        return await controllerRepository.GetLimit(item, currentPage, floor);
    }

    public async Task<DtoMicroController?> GetField(string item, string namePurpose, string field)
    {
        return await controllerRepository.GetField(item, namePurpose, item);
    }

    public async Task<DtoMicroController?> GetId(string item, int id)
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

    public async Task<DtoMicroController?> UpdateId(string item, Model.RequestModel.MicroController.MicroController microController, int id)
    {
        if (await hebrideanCacheServices.DeleteId(id.ToString()))
        {
            DtoMicroController? dtoMicroController = await controllerRepository.UpdateId(item, microController, id);
            
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