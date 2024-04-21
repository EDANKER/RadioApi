using Api.Data.Repository.Scenario;
using Api.Interface;
using Api.Model.RequestModel.Scenario;
using Api.Model.ResponseModel.Scenario;

namespace Api.Services.ScenarioServices;

public interface IScenarioServices
{
    Task<bool> CreateOrSave(string item, Scenario scenario);
    Task<DtoScenario?> GetId(string item, int id);
    Task<List<DtoScenario>?> GetLimit(string item, int limit);
    Task<bool> DeleteId(string item, int id);
    Task<bool> UpdateId(string item, Scenario scenario, int id);
    Task<bool> Search(string item, string name);
}

public class ScenarioServices(IRepository<Scenario, DtoScenario> scenarioRepository) : IScenarioServices
{
    public async Task<bool> CreateOrSave(string item, Scenario scenario)
    {
        return await scenarioRepository.CreateOrSave(item, scenario);
    }

    public async Task<DtoScenario?> GetId(string item, int id)
    {
        return await scenarioRepository.GetId(item, id);
    }

    public async Task<List<DtoScenario>?> GetLimit(string item, int limit)
    {
        return await scenarioRepository.GetLimit(item, limit);
    }
    
    public async Task<bool> DeleteId(string item, int id)
    {
        return await scenarioRepository.DeleteId(item, id);
    }

    public async Task<bool> UpdateId(string item, Scenario scenario, int id)
    {
        return await scenarioRepository.UpdateId(item, scenario, id);
    }

    public async Task<bool> Search(string item, string name)
    {
        return await scenarioRepository.Search(item, name);
    }
}