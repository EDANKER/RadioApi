using Api.Model.ResponseModel.Scenario;
using Api.Services.HebrideanCacheServices;
using Api.Services.JsonServices;
using Api.Services.MusicServices;
using Api.Services.ScenarioServices;

namespace Api.Services.BackGroundTaskServices.ScenarioTimeGetServices;

public class ScenarioTimeGetServices(
    IScenarioServices scenarioServices,
    IHebrideanCacheServices hebrideanCacheServices,
    IJsonServices<DtoScenario?> jsonServices,
    IJsonServices<string[]?> jsonServicesS,
    IMusicServices musicServices,
    ILogger<ScenarioTimeGetServices> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        int endeavors = 0;

        try
        {
            DateTime oldDataTime = DateTime.Now;
            string oldTime = $"{oldDataTime.Hour}:{oldDataTime.Minute}";

            List<DtoScenario>? dtoScenarios =
                await scenarioServices.GetLike("Scenario", oldDataTime.Day.ToString("dddd"), "Days");

            if (dtoScenarios != null)
            {
                foreach (var data in dtoScenarios)
                {
                    string[]? day = jsonServicesS.DesJson(data.Days);
                    if (day != null)
                        await hebrideanCacheServices.Put(data.Time.Split('-')[0], jsonServices.SerJson(data));
                }
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                DateTime newDataTime = DateTime.Now;
                string newTime = $"{newDataTime.Hour}:{newDataTime.Minute}";

                if (newTime != oldTime)
                {
                    oldTime = newTime;
                    
                    string? json = await hebrideanCacheServices.GetId(newTime);
                    if (json != null)
                    {
                        DtoScenario? dtoScenario = jsonServices.DesJson(json);
                        if (dtoScenario != null)
                            if (await musicServices.Play(dtoScenario.IdMusic, dtoScenario.IdMicroControllers)
                                || endeavors >= 3)
                            {
                                Console.WriteLine("Hello");
                                await hebrideanCacheServices.DeleteId(dtoScenario.Time.Split('-')[0]);
                            }
                            else
                                ++endeavors;
                    }
                }
            }

            if (stoppingToken.IsCancellationRequested)
            {
                dtoScenarios = await scenarioServices.GetAll("Scenario");
                if (dtoScenarios != null)
                    foreach (var data in dtoScenarios)
                        await hebrideanCacheServices.DeleteId(data.Time.Split('-')[0]);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
        }
    }
}