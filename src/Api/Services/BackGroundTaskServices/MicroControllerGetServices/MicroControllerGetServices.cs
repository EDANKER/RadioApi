using Api.Model.ResponseModel.MicroController;
using Api.Services.HebrideanCacheServices;
using Api.Services.MicroControllerServices;
using Newtonsoft.Json;

namespace Api.Services.BackGroundTaskServices.MicroControllerGetServices;

public class MicroControllerGetServices(
    IMicroControllerServices microControllerServices,
    ILogger<MicroControllerGetServices> logger,
    IHebrideanCacheServices hebrideanCacheServices) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            List<DtoMicroController>? dtoMicroControllers = await microControllerServices.GetAll("MicroControllers");
            if (dtoMicroControllers !=  null)
                foreach (var data in dtoMicroControllers)
                    await hebrideanCacheServices.Put(data.Id.ToString(), JsonConvert.SerializeObject(data));
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            List<DtoMicroController>? dtoMicroControllers = await microControllerServices.GetAll("MicroControllers");
            if (dtoMicroControllers !=  null)
                foreach (var data in dtoMicroControllers)
                    await hebrideanCacheServices.DeleteId(data.Id.ToString());
            
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
        }
    }
}