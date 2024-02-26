using Api.Data.Minio;
using Api.Data.Repository.PlayList;
using Api.Model.MinioModel;
using Api.Model.RequestModel.PlayList;
using Api.Model.ResponseModel.PlayList;

namespace Api.Services.PlayListServices;

public interface IPlayListServices
{
    public Task<bool> CreateOrSave(string item, string name, string description, IFormFile formFile);
    public Task<List<GetPlayList>> GetPlayList(string item, int limit);
    public Task<GetPlayList> GetPlayListId(string item, int id);
    public Task<bool> DeleteId(string item, int id);
    public Task<bool> Update(string item, string field, string name, int id);
    public Task<bool> Search(string item, string name);
}

public class PlayListServices(IPlayListRepository playListRepository, IMinio minio) : IPlayListServices
{
    public async Task<bool> CreateOrSave(string item, string name, string description, IFormFile formFile)
    {
        if (await minio.Save(new MinioModel(formFile.FileName, "photo",
                formFile.ContentType), formFile))
            return await playListRepository.CreateOrSave(item,
                new PlayList(name, description, formFile.FileName));

        return false;
    }

    public async Task<List<GetPlayList>> GetPlayList(string item, int limit)
    {
        List<GetPlayList> getPlayLists = new List<GetPlayList>();
        List<GetPlayList> getPlayListRepo = await playListRepository.GetLimit(item, limit);
        foreach (var data in getPlayListRepo)
        {
            GetPlayList getPlayList = new GetPlayList(data.Id, data.Name,
                data.Description,
                await minio.Get(new MinioModel(data.ImgPath, "photo", "image/jpeg")) ?? string.Empty);
            getPlayLists.Add(getPlayList);
        }

        return getPlayLists;
    }

    public async Task<GetPlayList> GetPlayListId(string item, int id)
    {
        GetPlayList getPlayListRepo = await playListRepository.GetId(item, id);
        return new GetPlayList(getPlayListRepo.Id, getPlayListRepo.Name, getPlayListRepo.Description,
            await minio.Get(new MinioModel(getPlayListRepo.ImgPath, "photo", "image/jpeg")) ?? string.Empty);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        return await playListRepository.DeleteId(item, id);
    }

    public async Task<bool> Update(string item, string field, string name, int id)
    {
        return await playListRepository.Update(item, name, field, id);
    }

    public async Task<bool> Search(string item, string name)
    {
        return await playListRepository.Search(item, name);
    }
}