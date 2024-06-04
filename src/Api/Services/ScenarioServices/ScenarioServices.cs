using Api.Interface;
using Api.Model.RequestModel.Scenario;
using Api.Model.ResponseModel.Music;
using Api.Model.ResponseModel.Scenario;
using Api.Services.HebrideanCacheServices;
using Api.Services.JsonServices;
using Api.Services.MusicServices;

namespace Api.Services.ScenarioServices;

public interface IScenarioServices
{
    Task<int?> ValidationTime(string time);
    Task<bool> CreateOrSave(string item, Scenario scenario);
    Task<DtoScenario?> GetId(string item, int id);
    Task<List<DtoScenario>?> GetAll(string item);
    Task<List<DtoScenario>?> GetField(string item, string namePurpose, string field);
    public Task<List<DtoScenario>?> GetLike(string item, string namePurpose, string field);
    Task<List<DtoScenario>?> GetLimit(string item, int limit);
    Task<bool> DeleteId(string item, int id);
    Task<bool> UpdateId(string item, Scenario scenario, int id);
    Task<bool> Search(string item, string name, string field);
}

public class ScenarioServices(
    IRepository<Scenario, DtoScenario, Scenario> scenarioRepository,
    IHebrideanCacheServices hebrideanCacheServices,
    IJsonServices<DtoScenario> jsonServices,
    IMusicServices musicServices) : IScenarioServices
{
    public async Task<int?> ValidationTime(string time)
    {
        DateTime dateTime = DateTime.Now;
        List<DtoScenario>? dtoScenarios = await scenarioRepository.GetLike("Scenario", dateTime.ToString("dddd"), "Days");
        if (dtoScenarios != null)
            foreach (var dataScenario in dtoScenarios)
            {
                DtoMusic? dtoMusic = await musicServices.GetId("Musics", dataScenario.IdMusic);
                if (dtoMusic != null)
                {
                    
                }
                    
            }

        return null;
    }

    public async Task<bool> CreateOrSave(string item, Scenario scenario)
    {
        List<DtoMusic>? dtoMusic = await musicServices.GetField("Musics", scenario.IdMusic.ToString(), "Id");

        if (dtoMusic != null)
        {
            int timeMin = dtoMusic[0].TimeMusic;
            string[] time = scenario.Time.Split([':']);

            int hours = int.Parse(time[0]);
            int minutes = int.Parse(time[1]);

            for (int i = 0; i < timeMin; i++)
                if (int.Parse(time[1]) == 59)
                {
                    timeMin -= 1;
                    hours += 1;
                    minutes = 0;
                }
                else minutes += 1;
            
            Scenario scenarioMap = new Scenario(scenario.Name, scenario.IdMicroController,
                $"{scenario.Time}-{hours.ToString()}:{minutes}", scenario.Days,
                scenario.IdMusic);

            DateTime dataTime = DateTime.Now;
            if (await scenarioRepository.CreateOrSave(item, scenarioMap))
                foreach (var days in scenario.Days)
                {
                    if (days == dataTime.ToString("dddd"))
                    {
                        List<DtoScenario>? dtoScenarios =
                            await scenarioRepository.GetField("Scenario", scenario.Name, "Name");
                        if (dtoScenarios != null)
                            foreach (var data in dtoScenarios)
                                return await hebrideanCacheServices.Put(
                                    scenario.Time,
                                     jsonServices.SerJson(data));
                    }
                    else
                    {
                        return true;
                    }
                }
        }

        return false;
    }

    public async Task<DtoScenario?> GetId(string item, int id)
    {
        return await scenarioRepository.GetId(item, id);
    }

    public async Task<List<DtoScenario>?> GetAll(string item)
    {
        return await scenarioRepository.GetAll(item);
    }

    public async Task<List<DtoScenario>?> GetField(string item, string namePurpose, string field)
    {
        return await scenarioRepository.GetField(item, namePurpose, field);
    }

    public async Task<List<DtoScenario>?> GetLike(string item, string namePurpose, string field)
    {
        return await scenarioRepository.GetLike(item, namePurpose, field);
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
        List<DtoMusic>? dtoMusic = await musicServices.GetField("Musics", scenario.IdMusic.ToString(), "Id");

        if (dtoMusic != null)
        {
            int timeMin = dtoMusic[0].TimeMusic;
            string[] time = scenario.Time.Split([':']);

            int hours = int.Parse(time[0]);
            int minutes = int.Parse(time[1]);

            for (int i = 0; i < 1; i++)
                if (int.Parse(time[1]) == 59)
                {
                    timeMin -= 1;
                    hours += 1;
                }
                else minutes += timeMin;
            
            Scenario scenarioMap = new Scenario(scenario.Name, scenario.IdMicroController,
                $"{scenario.Time}-{hours.ToString()}:{minutes}", scenario.Days,
                scenario.IdMusic);

            DateTime dataTime = DateTime.Now;
            if (await scenarioRepository.UpdateId(item, scenarioMap, id))
                foreach (var days in scenario.Days)
                {
                    if (days == dataTime.ToString("dddd"))
                    {
                        List<DtoScenario>? dtoScenarios =
                            await scenarioRepository.GetField("Scenario", scenario.Name, "Name");
                        if (dtoScenarios != null)
                            foreach (var data in dtoScenarios)
                                return await hebrideanCacheServices.Put(
                                    scenario.Time,
                                    jsonServices.SerJson(data));
                    }
                    else
                    {
                        return true;
                    }
                }
        }

        return false;
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        return await scenarioRepository.Search(item, name, field);
    }
}