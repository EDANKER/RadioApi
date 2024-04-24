using Api.Interface;
using Api.Model.RequestModel.PlayList;
using Api.Model.RequestModel.Update.UpdatePlayList;
using Api.Model.ResponseModel.PlayList;
using TagLib.Riff;

namespace Api.Services.PlayListServices;

public interface IPlayListServices
{
    Task<bool> CreateOrSave(string item, string name, string description, IFormFile formFile);
    Task<List<DtoPlayList>?> GetLimit(string item, int limit);
    Task<DtoPlayList?> GetId(string item, int id, bool isActiveGetUrl);
    Task<List<DtoPlayList>?> GetString(string item, string namePurpose, string field);
    Task<bool> DeleteId(string item, int id);
    Task<bool> UpdateId(string item, UpdatePlayList updatePlayList, int id);
    Task<bool> Search(string item, string name, string field);
}

public class PlayListServices(
    IRepository<CreatePlayList, DtoPlayList, UpdatePlayList> playListRepository,
    IFileServices.IFileServices fileServices)
    : IPlayListServices
{
    public async Task<bool> CreateOrSave(string item, string name, string description, IFormFile formFile)
    {
        if (await fileServices.Save(formFile, formFile.Name, "photo", "image/jpeg") &&
            await playListRepository.CreateOrSave(item,
                new CreatePlayList(name, description, formFile.FileName)))
        {
            List<DtoPlayList>? playLists = await playListRepository.GetString(item, name, "Name");
            if (playLists != null)
            {
                foreach (var data in playLists)
                    return await fileServices.CreateBucket(data.Id.ToString());
            }
        }

        return false;
    }

    public async Task<List<DtoPlayList>?> GetLimit(string item, int limit)
    {
        List<DtoPlayList> getPlayLists = new List<DtoPlayList>();
        List<DtoPlayList>? getPlayListRepo = await playListRepository.GetLimit(item, limit);
        if (getPlayListRepo != null)
        {
            foreach (var data in getPlayListRepo)
            {
                DtoPlayList dtoPlayList = new DtoPlayList(data.Id, data.Name,
                    data.Description,
                    await fileServices.GetUrl(data.ImgPath, "photo"));
                getPlayLists.Add(dtoPlayList);
            }
        }
        else
        {
            return null;
        }

        return getPlayLists;
    }

    public async Task<DtoPlayList?> GetId(string item, int id, bool isActiveGetUrl)
    {
        DtoPlayList? dtoPlayListRepo = await playListRepository.GetId(item, id);
        if (isActiveGetUrl && dtoPlayListRepo != null)
        {
            return new DtoPlayList(dtoPlayListRepo.Id, dtoPlayListRepo.Name, dtoPlayListRepo.Description,
                await fileServices.GetUrl(dtoPlayListRepo.ImgPath, "photo"));
        }

        return dtoPlayListRepo;
    }

    public async Task<List<DtoPlayList>?> GetString(string item, string namePurpose, string field)
    {
        return await playListRepository.GetString(item, namePurpose, field);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        DtoPlayList? dtoPlayList = await GetId(item, id, false);
        bool delete = await playListRepository.DeleteId(item, id);
        if (dtoPlayList != null && delete &&
            !await Search(item, dtoPlayList.ImgPath, "ImgPath"))
            return await fileServices.Delete(dtoPlayList.ImgPath, "photo");

        return delete;
    }

    public async Task<bool> UpdateId(string item, UpdatePlayList updatePlayList, int id)
    {
        return await playListRepository.UpdateId(item, updatePlayList, id);
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        return await playListRepository.Search(item, name, field);
    }
}