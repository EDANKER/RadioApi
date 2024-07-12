using Api.Interface.Repository;
using Api.Model.RequestModel.Scenario.PlayScenario;
using Api.Model.ResponseModel.PlayScenario;
using Api.Services.MusicServices;

namespace Api.Services.ScenarioServices.ScenarioServicesPlay;

public interface IScenarioPlayServices
{
    Task<int> GetCountPage(string item, int currentPage, int limit);
    Task<DtoPlayScenario?> CreateOrSave(string item, PlayScenario scenario);
    Task<DtoPlayScenario?> GetId(string item, int id);
    Task<List<DtoPlayScenario>?> GetAll(string item);
    Task<DtoPlayScenario?> GetField(string item, string namePurpose, string field);
    public Task<List<DtoPlayScenario>?> GetLike(string item, string namePurpose, string field);
    Task<List<DtoPlayScenario>?> GetLimit(string item, int currentPage, int limit);
    Task<bool> DeleteId(string item, int id);
    Task<DtoPlayScenario?> UpdateId(string item, PlayScenario scenario, int id);
    Task<bool> Search(string item, string name, string field);
}

public class ScenarioPlayServices(
    IRepository<PlayScenario, DtoPlayScenario, PlayScenario> scenarioRepository,
    IMusicServices musicServices) : IScenarioPlayServices
{
    public async Task<int> GetCountPage(string item, int currentPage, int limit)
    {
        while (true)
        {
            List<DtoPlayScenario>? list = await GetLimit(item, currentPage, limit);
            if (list != null)
                ++currentPage;
            else
                break;
        }

        return --currentPage;
    }

    public async Task<DtoPlayScenario?> CreateOrSave(string item, PlayScenario scenario)
    {
        if (await musicServices.GetId(item, scenario.IdMusic) != null)
            return await scenarioRepository.CreateOrSave(item, scenario);
        
        return null;
    }

    public async Task<DtoPlayScenario?> GetId(string item, int id)
    {
        return await scenarioRepository.GetId(item, id);
    }

    public async Task<List<DtoPlayScenario>?> GetAll(string item)
    {
        return await scenarioRepository.GetAll(item);
    }

    public async Task<DtoPlayScenario?> GetField(string item, string namePurpose, string field)
    {
        return await scenarioRepository.GetField(item, namePurpose, field);
    }

    public async Task<List<DtoPlayScenario>?> GetLike(string item, string namePurpose, string field)
    {
        return await scenarioRepository.GetLike(item, namePurpose, field);
    }

    public async Task<List<DtoPlayScenario>?> GetLimit(string item, int currentPage, int limit)
    {
        return await scenarioRepository.GetLimit(item, currentPage, limit);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        return await scenarioRepository.DeleteId(item, id);
    }

    public async Task<DtoPlayScenario?> UpdateId(string item, PlayScenario scenario, int id)
    {
        if (await musicServices.GetId(item, scenario.IdMusic) != null)
            return await scenarioRepository.CreateOrSave(item, scenario);
        
        return null;
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        return await scenarioRepository.Search(item, name, field);
    }
}