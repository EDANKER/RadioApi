using Api.Data.Repository.Scenario;
using Api.Model.RequestModel.Scenario;
using Api.Model.ResponseModel.Scenario;

namespace Api.Services.ScenarioServices;

public interface IScenarioServices
{
    public Task<bool> CreateOrSave(string item, Scenario scenario);
    public Task<List<DtoScenario>> GetId(string item, int id);
    public Task<List<DtoScenario>> GetLimit(string item, int limit);
    public Task<bool> DeleteId(string item, int id);
    public Task<bool> Update(string item, Scenario scenario, int id);
    public Task<bool> Search(string item, string name);
}

public class ScenarioServices(IScenarioRepository scenarioRepository) : IScenarioServices
{
    public async Task<bool> CreateOrSave(string item, Scenario scenario)
    {
        return await scenarioRepository.CreateOrSave(item, scenario);
    }

    public async Task<List<DtoScenario>> GetId(string item, int id)
    {
        return await scenarioRepository.GetId(item, id);
    }

    public async Task<List<DtoScenario>> GetLimit(string item, int limit)
    {
        return await scenarioRepository.GetLimit(item, limit);
    }
    
    public async Task<bool> DeleteId(string item, int id)
    {
        return await scenarioRepository.DeleteId(item, id);
    }

    public async Task<bool> Update(string item, Scenario scenario, int id)
    {
        return await scenarioRepository.Update(item, scenario, id);
    }

    public async Task<bool> Search(string item, string name)
    {
        return await scenarioRepository.Search(item, name);
    }
}