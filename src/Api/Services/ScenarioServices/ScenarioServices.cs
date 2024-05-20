using Api.Data.Repository.Scenario;
using Api.Interface;
using Api.Model.RequestModel.Scenario;
using Api.Model.ResponseModel.Scenario;
using Api.Services.HebrideanCacheServices;
using Api.Services.JsonServices;

namespace Api.Services.ScenarioServices;

public interface IScenarioServices
{
    Task<bool> CreateOrSave(string item, Scenario scenario);
    Task<DtoScenario?> GetId(string item, int id);
    Task<List<DtoScenario>?> GetAll(string item);
    Task<List<DtoScenario>?> GetLimit(string item, int limit);
    Task<bool> DeleteId(string item, int id);
    Task<bool> UpdateId(string item, Scenario scenario, int id);
    Task<bool> Search(string item, string name, string field);
}

public class ScenarioServices(
    IRepository<Scenario, DtoScenario, Scenario> scenarioRepository,
    IHebrideanCacheServices hebrideanCacheServices,
    IJsonServices<DtoScenario> jsonServices) : IScenarioServices
{
    public async Task<bool> CreateOrSave(string item, Scenario scenario)
    {
        DateTime dataTime = DateTime.Now;
        if (await scenarioRepository.CreateOrSave(item, scenario))
            if (scenario.Days == dataTime.ToString("ddd"))
            {
                List<DtoScenario>? dtoScenarios = await scenarioRepository.GetString("Scenario", scenario.Name, "Name");
                if (dtoScenarios != null)
                    foreach (var data in dtoScenarios)
                        return await hebrideanCacheServices.Put(scenario.Time,
                            jsonServices.SerJson(data));
            }

        return true;
    }

    public async Task<DtoScenario?> GetId(string item, int id)
    {
        return await scenarioRepository.GetId(item, id);
    }

    public async Task<List<DtoScenario>?> GetAll(string item)
    {
        return await scenarioRepository.GetAll(item);
    }

    public async Task<List<DtoScenario>?> GetLimit(string item, int limit)
    {
        return await scenarioRepository.GetFloor(item, limit);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        return await scenarioRepository.DeleteId(item, id);
    }

    public async Task<bool> UpdateId(string item, Scenario scenario, int id)
    {
        return await scenarioRepository.UpdateId(item, scenario, id);
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        return await scenarioRepository.Search(item, name, field);
    }
}