using Radio.Data.Repository.Scenari;
using Radio.Model.RequestModel.Scenari;
using Radio.Model.ResponseModel.Scenari;

namespace Radio.Services.SettingsScenariServices;

public interface IScenariServices
{
    public Task<List<GetScenari>> GetLimit(string item, int limit);
}

public class ScenariServices : IScenariServices
{
    private IScenariRepository _scenarioRepository;
    
    public ScenariServices(IScenariRepository scenarioRepository)
    {
        _scenarioRepository = scenarioRepository;
    }
    
    public async Task<List<GetScenari>> GetLimit(string item, int limit)
    {
        return await _scenarioRepository.GetLimit(item, limit);
    }
}