using Radio.Model.RequestModel.Music;

namespace Api.Services.SaveMusicServices;

public interface ISaveMusicServices
{
    public Task<Music> SaveMusic(IFormFile formFile, int id);
}

public class SaveMusicServices : ISaveMusicServices
{
    public async Task<Music> SaveMusic(IFormFile formFile, int id)
    {
        string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Uploads/Music");
        string filePath = Path.Combine(uploadsPath, formFile.FileName);
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        await formFile.CopyToAsync(fileStream);

        return new Music(formFile.FileName, "Data/Uploads/Music/" + formFile.FileName, id);
    }
}