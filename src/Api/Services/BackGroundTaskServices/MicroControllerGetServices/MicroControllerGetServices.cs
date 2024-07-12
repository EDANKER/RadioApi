using Api.Interface.MicroControllerServices;
using Api.Model.RequestModel.MicroController.FloorMicroController;
using Api.Model.ResponseModel.FloorMicroController;
using Api.Services.HebrideanCacheServices;
using Newtonsoft.Json;

namespace Api.Services.BackGroundTaskServices.MicroControllerGetServices;

public class MicroControllerGetServices(
    IMicroControllerServices<MicroController, DtoMicroController> floorMicroControllerServices,
    ILogger<MicroControllerGetServices> logger,
    IHebrideanCacheServices hebrideanCacheServices) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            List<DtoMicroController>? dtoMicroControllersFloor = await floorMicroControllerServices.GetAll("MicroControllers");
            if (dtoMicroControllersFloor !=  null)
                foreach (var data in dtoMicroControllersFloor)
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
            List<DtoMicroController>? dtoMicroControllersFloor = await floorMicroControllerServices.GetAll("MicroControllers");
            if (dtoMicroControllersFloor !=  null)
                foreach (var data in dtoMicroControllersFloor)
                    await hebrideanCacheServices.DeleteId(data.Id.ToString());
        }
        catch (Exception e)
        {
            logger.LogError(e.ToString());
        }
    }
}