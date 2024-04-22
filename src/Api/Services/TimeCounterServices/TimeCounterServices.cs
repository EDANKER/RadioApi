using TagLib;

namespace Api.Services.TimeCounterServices;

public interface ITimeCounterServices
{
    Task<double> Time(IFormFile formFile);
}

public class TimeCounterServices : ITimeCounterServices
{
    public Task<double> Time(IFormFile formFile)
    {
        TagLib.File bit = TagLib.File.Create(new StreamFileAbstraction(formFile.FileName, formFile.OpenReadStream(),
            formFile.OpenReadStream()));
        return Task.FromResult(bit.Properties.Duration.TotalSeconds);
    }
}