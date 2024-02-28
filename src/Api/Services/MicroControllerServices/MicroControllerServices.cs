using Api.Data.Repository.MicroController;
using Api.Model.RequestModel.MicroController;
using Api.Model.ResponseModel.MicroController;

namespace Api.Services.MicroControllerServices;

public interface IMicroControllerServices
{
    public Task<bool> CreateOrSave(string item, MicroController microController);
    public Task<List<DtoMicroController>> GetData(string item);
    public Task<bool> DeleteId(string item, int id);
    public Task<bool> Update(string item, MicroController microController);
    public Task<bool> Search(string item, string name);
    public Task<bool> CheckMicroController(string ip);
}

public class MicroControllerServices(IMicroControllerRepository controllerRepository) : IMicroControllerServices
{
    public async Task<bool> CreateOrSave(string item, MicroController microController)
    {
        return await controllerRepository.CreateOrSave(item, microController);
    }

    public async Task<List<DtoMicroController>> GetData(string item)
    {
        return await controllerRepository.GetData(item);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        return await controllerRepository.DeleteId(item, id);
    }

    public async Task<bool> Update(string item, MicroController microController)
    {
        return await controllerRepository.Update(item, microController);
    }

    public async Task<bool> Search(string item, string name)
    {
        return await controllerRepository.Search(item, name);
    }

    public async Task<bool> CheckMicroController(string ip)
    {
        throw new NotImplementedException();
    }
}