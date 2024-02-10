namespace Api.Services.TransmissionToMicroController;

public interface ITransmissionToMicroController
{
    public Task<bool> Transmission();
}

public class TransmissionToMicroController : ITransmissionToMicroController
{
    public async Task<bool> Transmission()
    {
        throw new NotImplementedException();
    }
}