using Api.Model.ResponseModel.MicroController;

namespace Api.Data.Repository.MicroController;

public interface IMicroControllerRepository
{
    public Task<bool> CreateOrSave(string item, Model.RequestModel.MicroController.MicroController microController);
    public Task<List<DtoMicroController>> GetData(string item);
    public Task<bool> DeleteId(string item, int id);
    public Task<bool> Update(string item, Model.RequestModel.MicroController.MicroController microController);
    public Task<bool> Search(string item, string name);
}

public class MicroControllerRepository : IMicroControllerRepository
{
    public async Task<bool> CreateOrSave(string item, Model.RequestModel.MicroController.MicroController microController)
    {
        throw new NotImplementedException();
    }

    public async Task<List<DtoMicroController>> GetData(string item)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(string item, Model.RequestModel.MicroController.MicroController microController)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Search(string item, string name)
    {
        throw new NotImplementedException();
    }
}