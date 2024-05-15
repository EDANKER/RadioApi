﻿using Api.Model.ResponseModel.Scenario;
using Api.Services.HebrideanCacheServices;
using Api.Services.JsonServices;
using Api.Services.MusicServices;
using Api.Services.ScenarioServices;

namespace Api.Services.BackGroundTaskServices.ScenarioTimeGetServices;

public class ScenarioTimeGetServices(
    IScenarioServices scenarioServices,
    IHebrideanCacheServices hebrideanCacheServices,
    IJsonServices<DtoScenario?> jsonServices,
    IMusicServices musicServices,
    ILogger<ScenarioTimeGetServices> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            DateTime days = DateTime.Now;
            List<DtoScenario>? dtoScenarios = await scenarioServices.GetAll("Scenario");
            if (dtoScenarios != null)
            {
                foreach (var data in dtoScenarios)
                    if(data.Days == days.ToString("ddd"))
                        await hebrideanCacheServices.Put(data.Time, jsonServices.SerJson(data));

                while (!stoppingToken.IsCancellationRequested)
                {
                    DateTime dataTime = DateTime.Now;
                    string time = $"{dataTime.Hour}:{dataTime.Minute}";
                    string? json = await hebrideanCacheServices.GetId(time);
                    if (json != null)
                    {
                        DtoScenario? dtoScenario = jsonServices.DesJson(json);
                        if (dtoScenario != null)
                            if (await musicServices.Play(dtoScenario.IdMusic, dtoScenario.IdMicroControllers))
                                await hebrideanCacheServices.DeleteId(dtoScenario.Time);
                    }
                    
                }
            
                if (stoppingToken.IsCancellationRequested)
                    foreach (var data in dtoScenarios)
                        await hebrideanCacheServices.DeleteId(data.Time);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
        }
    }
}