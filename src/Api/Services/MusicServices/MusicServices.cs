﻿using Api.Interface.MicroControllerServices;
using Api.Interface.Repository;
using Api.Model.RequestModel.Create.CreateMusic;
using Api.Model.RequestModel.MicroController;
using Api.Model.RequestModel.Update.UpdateMusic;
using Api.Model.ResponseModel.MicroController;
using Api.Model.ResponseModel.Music;
using Api.Services.HttpMicroControllerServices;
using Api.Services.RadioServices;
using Api.Services.TimeCounterServices;

namespace Api.Services.MusicServices;

public interface IMusicServices
{
    Task<int> GetCountPage(string item, int currentPage, int limit);
    Task<List<DtoMusic>?> GetAll(string item);
    Task<bool> Play(int idMusic, int[] idController);
    Task<bool> PlayLife(IFormFile formFile, int[] idController);
    Task<bool> Stop(int[] idController);
    Task<DtoMusic?> CreateOrSave(string item, IFormFile formFile, string namePlayList);
    Task<List<DtoMusic>?> GetLimit(string item, int currentPage, int limit);
    Task<DtoMusic?> GetId(string item, int id);
    Task<DtoMusic?> GetField(string item, string namePurpose, string field);
    Task<bool> DeleteId(string item, int id);
    Task<DtoMusic?> UpdateId(string item, UpdateMusic updateMusic, int id);
    Task<bool> Search(string item, string name, string field);
}

public class MusicServices(
    IRepository<CreateMusic, DtoMusic, UpdateMusic> musicRepository,
    IFileServices.IFileServices fileServices,
    IHttpMicroControllerServices httpMicroControllerServices,
    IMicroControllerServices<MicroController, DtoMicroController> floorMicroControllerServices,
    ITimeCounterServices timeCounterServices,
    IRadioServices radioServices) : IMusicServices
{
    private async Task<Stream?> GetMusicInMinio(int id)
    {
        DtoMusic? dtoMusic = await musicRepository.GetId("Musics", id);
        if (dtoMusic != null)
        {
            string name = dtoMusic.Name;
            Stream? read = await fileServices.GetStream(name, "music");
            if (read != null)
                return read;
        }

        return null;
    }

    public async Task<int> GetCountPage(string item, int currentPage, int limit)
    {
        while (true)
        {
            List<DtoMusic>? list = await GetLimit(item, currentPage, limit);
            if (list != null)
                ++currentPage;
            else
                break;
        }

        return --currentPage;
    }

    public async Task<List<DtoMusic>?> GetAll(string item)
    {
        return await musicRepository.GetAll(item);
    }

    public async Task<bool> Play(int idMusic, int[] idController)
    {
        Stream? stream = await GetMusicInMinio(idMusic);
        if (stream != null)
        {
            if (await radioServices.PostStream(stream))
            {
                foreach (var data in idController)
                {
                    if (data < 0)
                        continue;
                    DtoMicroController? dtoMicroController =
                        await floorMicroControllerServices.GetId("MicroControllers", data);
                    if (dtoMicroController != null)
                        if (await httpMicroControllerServices.Play(
                                dtoMicroController));
                }
            }
        }

        return false;
    }

    public async Task<bool> PlayLife(IFormFile formFile, int[] idController)
    {
        if (await radioServices.PostStream(formFile.OpenReadStream()))
        {
            foreach (var data in idController)
            {
                if (data < 0)
                    continue;
                DtoMicroController? dtoMicroController = await floorMicroControllerServices.GetId("MicroControllers", data);
                if (dtoMicroController != null)
                    return await httpMicroControllerServices.Play(
                        dtoMicroController);
            }
        }

        return false;
    }

    public async Task<bool> Stop(int[] idController)
    {
        foreach (var data in idController)
        {
            if (data < 0)
                continue;

            DtoMicroController? dtoMicroController = await floorMicroControllerServices.GetId("MicroControllers", data);
            if (dtoMicroController != null)
                return await httpMicroControllerServices.Stop(dtoMicroController);
        }

        return false;
    }

    public async Task<DtoMusic?> CreateOrSave(string item, IFormFile formFile, string namePlayList)
    {
        if (await fileServices.Save(formFile, formFile.FileName, "music", "audio/mpeg"))
            return await musicRepository.CreateOrSave(item,
                new CreateMusic(formFile.FileName, namePlayList, await timeCounterServices.TimeToMinutes(formFile)));

        return null;
    }

    public async Task<List<DtoMusic>?> GetLimit(string item, int currentPage, int limit)
    {
        return await musicRepository.GetLimit(item, currentPage, limit);
    }

    public async Task<DtoMusic?> GetId(string item, int id)
    {
        return await musicRepository.GetId(item, id);
    }

    public async Task<DtoMusic?> GetField(string item, string namePurpose, string field)
    {
        return await musicRepository.GetField(item, namePurpose, field);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        DtoMusic? dtoMusic = await GetId(item, id);
        if (dtoMusic != null && await fileServices.Delete(dtoMusic.Name, "music"))
            return await musicRepository.DeleteId(item, id);

        return false;
    }

    public async Task<DtoMusic?> UpdateId(string item, UpdateMusic updateMusic, int id)
    {
        return await musicRepository.UpdateId(item, updateMusic, id);
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        return await musicRepository.Search(item, name, field);
    }
}