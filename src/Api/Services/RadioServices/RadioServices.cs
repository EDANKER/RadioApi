namespace Api.Services.RadioServices;

public interface IRadioServices
{
    Task<bool> PostStream();
}

public class RadioServices : IRadioServices
{
    public Task<bool> PostStream()
    {
        throw new NotImplementedException();
    }
}