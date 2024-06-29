using Api.Interface;
using Api.Interface.Repository;
using Api.Model.RequestModel.Scenario;
using Api.Model.ResponseModel.Music;
using Api.Model.ResponseModel.Scenario;
using Api.Services.HebrideanCacheServices;
using Api.Services.JsonServices;
using Api.Services.MusicServices;

namespace Api.Services.ScenarioServices;

public interface IScenarioServices
{
    Task<int> GetCountPage(string item, int currentPage, int limit);
    Task<bool> ValidationTime(string time, string[] days);
    Task<DtoScenario?> CreateOrSave(string item, Scenario scenario);
    Task<DtoScenario?> GetId(string item, int id);
    Task<List<DtoScenario>?> GetAll(string item);
    Task<DtoScenario?> GetField(string item, string namePurpose, string field);
    public Task<List<DtoScenario>?> GetLike(string item, string namePurpose, string field);
    Task<List<DtoScenario>?> GetLimit(string item, int currentPage, int limit);
    Task<bool> DeleteId(string item, int id);
    Task<DtoScenario?> UpdateId(string item, Scenario scenario, int id);
    Task<bool> Search(string item, string name, string field);
}

public class ScenarioServices(
    IRepository<Scenario, DtoScenario, Scenario> scenarioRepository,
    IHebrideanCacheServices hebrideanCacheServices,
    IJsonServices<DtoScenario> jsonServices,
    IMusicServices musicServices) : IScenarioServices
{
    public async Task<int> GetCountPage(string item, int currentPage, int limit)
    {
        while (true)
        {
            List<DtoScenario>? list = await GetLimit(item, currentPage, limit);
            if (list != null)
                ++currentPage;
            else
                break;
        }

        return --currentPage;
    }

    public async Task<bool> ValidationTime(string time, string[] days)
    {
        foreach (var data in days)
        {
            List<DtoScenario>? dtoScenarios =
                await scenarioRepository.GetLike("Scenario", data, "Days");

            if (dtoScenarios != null)
            {
                foreach (var dataScenario in dtoScenarios)
                {
                    DtoMusic? dtoMusic = await musicServices.GetId("Musics", dataScenario.IdMusic);

                    if (dtoMusic != null)
                    {
                        int timeMin = dtoMusic.TimeMusic;
                        string[] scnearioTime = time.Split([':']);

                        int hoursScenarioFirstTime = int.Parse(scnearioTime[0]);
                        int minutesScenarioFirstTime = int.Parse(scnearioTime[1]);

                        int hoursScenarioLastTime = int.Parse(scnearioTime[0]);
                        int minutesScenarioLastTime = int.Parse(scnearioTime[1]);

                        for (int i = 0; i < timeMin; i++)
                        {
                            if (minutesScenarioLastTime >= 59)
                            {
                                timeMin -= 1;
                                hoursScenarioLastTime += 1;
                                minutesScenarioLastTime = 0;
                            }
                            else minutesScenarioLastTime += 1;
                        }

                        string[] timeDtoScenario = dataScenario.Time.Split(['-']);
                        string[] dtoFirstTime = timeDtoScenario[0].Split([':']);
                        string[] dtoLastTime = timeDtoScenario[1].Split(':');

                        if (hoursScenarioFirstTime == int.Parse(dtoFirstTime[0]))
                        {
                            if (minutesScenarioFirstTime >= int.Parse(dtoFirstTime[1]) &&
                                minutesScenarioFirstTime <= int.Parse(dtoLastTime[1]))
                                return false;
                            if (minutesScenarioLastTime >= int.Parse(dtoFirstTime[1]) &&
                                minutesScenarioLastTime <= int.Parse(dtoLastTime[1]))
                                return false;
                        }
                    }
                }
            }
        }

        return true;
    }

    public async Task<DtoScenario?> CreateOrSave(string item, Scenario scenario)
    {
        DtoMusic? dtoMusic = await musicServices.GetField("Musics", scenario.IdMusic.ToString(), "Id");

        if (dtoMusic != null)
        {
            int timeMin = dtoMusic.TimeMusic;
            string[] time = scenario.Time.Split([':']);

            int hours = int.Parse(time[0]);
            int minutes = int.Parse(time[1]);

            for (int i = 0; i < timeMin; i++)
            {
                if (minutes >= 59)
                {
                    timeMin -= 1;
                    hours += 1;
                    minutes = 0;
                }
                else minutes += 1;
            }

            Scenario scenarioMap = new Scenario(scenario.Name, scenario.IdMicroController,
                $"{scenario.Time}-{hours.ToString()}:{minutes}", scenario.Days,
                scenario.IdMusic);

            DateTime dataTime = DateTime.Now;
            DtoScenario? dtoScenario = await scenarioRepository.CreateOrSave(item, scenarioMap);
            if (dtoScenario != null)
            {
                foreach (var days in scenario.Days)
                {
                    if (days == dataTime.ToString("dddd"))
                    {
                        DtoScenario? dtoScenarios =
                            await scenarioRepository.GetField("Scenario", scenario.Name, "Name");
                        if (dtoScenarios != null)
                            await hebrideanCacheServices.Put(
                                scenario.Time,
                                jsonServices.SerJson(dtoScenarios));
                    }
                }
                return dtoScenario;
            }
        }
        
        return null;
    }

    public async Task<DtoScenario?> GetId(string item, int id)
    {
        return await scenarioRepository.GetId(item, id);
    }

    public async Task<List<DtoScenario>?> GetAll(string item)
    {
        return await scenarioRepository.GetAll(item);
    }

    public async Task<DtoScenario?> GetField(string item, string namePurpose, string field)
    {
        return await scenarioRepository.GetField(item, namePurpose, field);
    }

    public async Task<List<DtoScenario>?> GetLike(string item, string namePurpose, string field)
    {
        return await scenarioRepository.GetLike(item, namePurpose, field);
    }

    public async Task<List<DtoScenario>?> GetLimit(string item, int currentPage, int limit)
    {
        return await scenarioRepository.GetLimit(item, currentPage, limit);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        return await scenarioRepository.DeleteId(item, id);
    }

    public async Task<DtoScenario?> UpdateId(string item, Scenario scenario, int id)
    {
        DtoMusic? dtoMusic = await musicServices.GetField("Musics", scenario.IdMusic.ToString(), "Id");

        if (dtoMusic != null)
        {
            int timeMin = dtoMusic.TimeMusic;
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
            DtoScenario? dtoScenario = await scenarioRepository.UpdateId(item, scenarioMap, id);
            if (dtoScenario != null)
                foreach (var days in scenario.Days)
                {
                    if (days == dataTime.ToString("dddd"))
                    {
                        DtoScenario? dtoScenarios =
                            await scenarioRepository.GetField("Scenario", scenario.Name, "Name");
                        if (dtoScenarios != null)
                            await hebrideanCacheServices.Put(
                                scenario.Time,
                                jsonServices.SerJson(dtoScenarios));
                    }
                }

            return dtoScenario;
        }

        return null;
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        return await scenarioRepository.Search(item, name, field);
    }
}