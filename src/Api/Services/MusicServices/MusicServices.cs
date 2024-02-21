﻿using Api.Data.Repository.Music;
using Api.Model.RequestModel.Music;
using Api.Model.ResponseModel.Music;
using Api.Services.AudioFileSaveToMicroControllerServices;

namespace Api.Services.MusicServices;

public interface IMusicServices
{
    public Task<bool> CreateOrSave(string item, IFormFile formFile, string name);
    public Task<List<GetMusic>> GetMusic(string item,int limit);
    public Task<GetMusic> GetMusicId(string item,int id);
    public Task<List<GetMusic>> GetMusicPlayListTag(string item,string name);
    public Task<bool> DeleteId(string item, int id, string path);
    public Task<bool> Update(string item, string field, string name, int id);
    public Task<bool> Search(string item, string name);
}

public class MusicServices(IMusicRepository musicRepository, IAudioFileSaveToMicroControllerServices audioFileSaveToMicroControllerServices) : IMusicServices
{
    public async Task<bool> CreateOrSave(string item, IFormFile formFile, string name)
    {
        return await musicRepository.CreateOrSave(item, await audioFileSaveToMicroControllerServices.SaveAudio(formFile, name));
    }

    public async Task<List<GetMusic>> GetMusic(string item,int limit)
    {
        return await musicRepository.GetLimit(item, limit);
    }

    public async Task<GetMusic> GetMusicId(string item, int id)
    {
        return await musicRepository.GetId(item, id);
    }

    public async Task<List<GetMusic>> GetMusicPlayListTag(string item, string name)
    {
        return await musicRepository.GetMusicPlayListTag(item, name);
    }

    public async Task<bool> DeleteId(string item, int id, string path)
    {
        await audioFileSaveToMicroControllerServices.DeleteMusic(path);
        return await musicRepository.DeleteId(item, id);
    }

    public async Task<bool> Update(string item, string field, string name, int id)
    {
        await audioFileSaveToMicroControllerServices.UpdateName(name);
        return await musicRepository.Update(item, field, name, id);
    }

    public async Task<bool> Search(string item, string name)
    {
        return await musicRepository.Search(item, name);
    }
}