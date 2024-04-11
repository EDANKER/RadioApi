using Api.Services.HebrideanCacheServices;
using Api.Services.ScenarioServices;

namespace Api.Services.TimeCounterServices;

public class TimeCounterServices(IScenarioServices scenarioServices, IHebrideanCacheServices hebrideanCacheServices)
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        TimeSpan timeSpan = DateTime.Now.TimeOfDay;
        TimeSpan ignoreTime = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, 0);
        foreach (var data in await scenarioServices.GetHour("Scenario", "3"))
        {
            await hebrideanCacheServices.Put(data.Id.ToString(), data.Time);
            Console.WriteLine(data.Id);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        
    }
}