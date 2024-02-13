namespace Api.Services.TransmissionToMicroController;

public interface ITransmissionToMicroController
{
    public Task<bool> Transmission(IFormFile formFile);
}

public class TransmissionToMicroController : ITransmissionToMicroController
{
    public async Task<bool> Transmission(IFormFile formFile)
    {
        return true;
    }
}