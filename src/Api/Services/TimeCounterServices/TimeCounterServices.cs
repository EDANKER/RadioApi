using TagLib;

namespace Api.Services.TimeCounterServices;

public interface ITimeCounterServices
{
    Task<int> TimeToMinutes(IFormFile formFile);
}

public class TimeCounterServices : ITimeCounterServices
{
    public Task<int> TimeToMinutes(IFormFile formFile)
    {
        TagLib.File bit = TagLib.File.Create(new StreamFileAbstraction(formFile.FileName, formFile.OpenReadStream(),
            formFile.OpenReadStream()));
        return Task.FromResult((int)Math.Ceiling(bit.Properties.Duration.TotalMinutes));
    }
}