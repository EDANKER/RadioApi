using Api.Model.ResponseModel.Scenario;
using Api.Services.HebrideanCacheServices;
using Api.Services.JsonServices;
using Api.Services.ScenarioServices;

namespace Api.Services.ScenarioTimeGetServices;

public class ScenarioTimeGetServices(
    IScenarioServices scenarioServices,
    IHebrideanCacheServices hebrideanCacheServices,
    IJsonServices<DtoScenario> jsonServices) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        List<DtoScenario>? dtoScenarios = await scenarioServices.GetAll("Scenario");
        if (dtoScenarios != null)
            foreach (var data in dtoScenarios)
                await hebrideanCacheServices.Put(data.Time, jsonServices.SerJson(data));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        List<DtoScenario>? dtoScenarios = await scenarioServices.GetAll("Scenario");
        if (dtoScenarios != null)
            foreach (var data in dtoScenarios)
                await hebrideanCacheServices.DeleteId(data.Time);
    }
}