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
        try
        {
            int endeavors = 0;
            DateTime days = DateTime.Now;
            List<DtoScenario>? dtoScenarios = await scenarioServices.GetLike("Scenario", days.ToString("dddd"), "Days");
            if (dtoScenarios != null)
            {
                foreach (var data in dtoScenarios)
                {
                    Console.WriteLine(data);
                    string[]? day = await jsonServicesS.DesJson(data.Days);
                    if (day != null)
                        await hebrideanCacheServices.Put(data.Time.Split('-')[0], await jsonServices.SerJson(data));
                }

                while (!stoppingToken.IsCancellationRequested)
                {
                    DateTime dataTime = DateTime.Now;
                    string time = $"{dataTime.Hour}:{dataTime.Minute}";
                    string? json = await hebrideanCacheServices.GetId(time);
                    if (json != null)
                    {
                        DtoScenario? dtoScenario = await jsonServices.DesJson(json);
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

                if (stoppingToken.IsCancellationRequested)
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