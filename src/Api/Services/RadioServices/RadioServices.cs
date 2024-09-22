using Api.Services.HttpMicroControllerServices;
using NAudio.Wave;

namespace Api.Services.RadioServices;

public interface IRadioServices
{
    Task<bool> PostStream(Stream stream);
}

public class RadioServices(IHttpMicroControllerServices httpMicroControllerServices) : IRadioServices
{
    public async Task<bool> PostStream(Stream stream)
    {
        return await httpMicroControllerServices.PostStream(stream);
    }
}