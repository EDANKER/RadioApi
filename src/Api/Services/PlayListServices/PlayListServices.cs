using Api.Interface;
using Api.Interface.Repository;
using Api.Model.RequestModel.Create.CreatePlayList;
using Api.Model.RequestModel.Update.UpdatePlayList;
using Api.Model.ResponseModel.PlayList;
using TagLib.Riff;

namespace Api.Services.PlayListServices;

public interface IPlayListServices
{
    Task<int> GetCountPage(string item, int currentPage, int limit);
    Task<DtoPlayList?> CreateOrSave(string item, string name, string description, IFormFile formFile);
    Task<List<DtoPlayList>?> GetLimit(string item, int currentPage, int limit);
    Task<DtoPlayList?> GetId(string item, int id, bool isActiveGetUrl);
    Task<DtoPlayList?> GetField(string item, string namePurpose, string field);
    Task<bool> DeleteId(string item, int id);
    Task<DtoPlayList?> UpdateId(string item, UpdatePlayList updatePlayList, int id);
    Task<bool> Search(string item, string name, string field);
}

public class PlayListServices(
    IRepository<CreatePlayList, DtoPlayList, UpdatePlayList> playListRepository,
    IFileServices.IFileServices fileServices)
    : IPlayListServices
{
    public async Task<int> GetCountPage(string item, int currentPage, int limit)
    {
        while (true)
        {
            List<DtoPlayList>? list = await GetLimit(item, currentPage, limit);
            if (list != null)
                ++currentPage;
            else
                break;
        }

        return --currentPage;
    }

    public async Task<DtoPlayList?> CreateOrSave(string item, string name, string description, IFormFile formFile)
    {
        if (await fileServices.Save(formFile, formFile.FileName, "photo", "image/jpeg"))
            return await playListRepository.CreateOrSave(item,
                new CreatePlayList(name, description, formFile.FileName));

        return null;
    }

    public async Task<List<DtoPlayList>?> GetLimit(string item, int currentPage, int limit)
    {
        List<DtoPlayList> getPlayLists = new List<DtoPlayList>();
        List<DtoPlayList>? getPlayListRepo = await playListRepository.GetLimit(item, currentPage, limit);
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

    public async Task<DtoPlayList?> GetField(string item, string namePurpose, string field)
    {
        return await playListRepository.GetField(item, namePurpose, field);
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

    public async Task<DtoPlayList?> UpdateId(string item, UpdatePlayList updatePlayList, int id)
    {
        return await playListRepository.UpdateId(item, updatePlayList, id);
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        return await playListRepository.Search(item, name, field);
    }
}