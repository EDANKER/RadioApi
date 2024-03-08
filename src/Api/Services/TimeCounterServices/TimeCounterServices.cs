using Api.Services.ScenarioServices;

namespace Api.Services.TimeCounterServices;
public class TimeCounterServices(IScenarioServices scenarioServices) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        TimeSpan timeSpan = DateTime.Now.TimeOfDay;
        TimeSpan ignoreTime = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, 0);
        foreach (var data in await scenarioServices.GetHour("Scenario", "3"))
        {
            Console.WriteLine(ignoreTime);
            Console.WriteLine(data.Time);
            if (ignoreTime.ToString() == "03:20:00")
            {
                Console.WriteLine("Я играю");
            }
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        
    }
}