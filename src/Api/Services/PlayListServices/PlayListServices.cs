using Api.Data.Minio;
using Api.Data.Repository.PlayList;
using Api.Model.MinioModel;
using Api.Model.RequestModel.PlayList;
using Api.Model.ResponseModel.PlayList;

namespace Api.Services.PlayListServices;

public interface IPlayListServices
{
    public Task<bool> CreateOrSave(string item, string name, string description, IFormFile formFile);
    public Task<List<DtoPlayList>> GetPlayList(string item, int limit);
    public Task<DtoPlayList?> GetPlayListId(string item, int id);
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

    public async Task<List<DtoPlayList>> GetPlayList(string item, int limit)
    {
        List<DtoPlayList> getPlayLists = new List<DtoPlayList>();
        List<DtoPlayList> getPlayListRepo = await playListRepository.GetLimit(item, limit);
        foreach (var data in getPlayListRepo)
        {
            DtoPlayList dtoPlayList = new DtoPlayList(data.Id, data.Name,
                data.Description,
                await minio.GetUrl(new MinioModel(data.ImgPath, "photo", "image/jpeg")));
            getPlayLists.Add(dtoPlayList);
        }

        return getPlayLists;
    }

    public async Task<DtoPlayList?> GetPlayListId(string item, int id)
    {
        DtoPlayList dtoPlayListRepo = await playListRepository.GetId(item, id);
        if (dtoPlayListRepo != null)
            return new DtoPlayList(dtoPlayListRepo.Id, dtoPlayListRepo.Name, dtoPlayListRepo.Description,
                await minio.GetUrl(new MinioModel(dtoPlayListRepo.ImgPath, "photo", "image/jpeg")));

        return null;
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