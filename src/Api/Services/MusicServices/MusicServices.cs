﻿using Api.Data.Repository.Music;
using Radio.Model.Music;
using Radio.Model.RequestModel.Music;

namespace Api.Services.MusicServices;

public interface IMusicServices
{
    public Task<bool> CreateOrSave(string item, IFormFile formFile, int id);
    public Task<List<GetMusic>> GetMusic(string item,int limit);
    public Task<List<GetMusic>> GetMusicId(string item,int id);
    public Task<List<GetMusic>> GetMusicTagPlayList(string item,int id);
    public Task<bool> DeleteId(string item, int id);
    public Task<bool> Update(string item, string field, string name, int id);
    public Task<bool> Search(string item, string name);
}

public class MusicServices(IMusicRepository musicRepository) : IMusicServices
{
    public async Task<bool> CreateOrSave(string item, IFormFile formFile, int id)
    {
        return await musicRepository.CreateOrSave(item, await SaveMusic(formFile, id));
    }

    public async Task<List<GetMusic>> GetMusic(string item,int limit)
    {
        return await musicRepository.GetLimit(item, limit);
    }

    public async Task<List<GetMusic>> GetMusicId(string item, int id)
    {
        return await musicRepository.GetId(item, id);
    }

    public async Task<List<GetMusic>> GetMusicTagPlayList(string item, int id)
    {
        return await musicRepository.GetPlayListTag(item, id);
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        return await musicRepository.DeleteId(item, id);
    }

    public async Task<bool> Update(string item, string field, string name, int id)
    {
        return await musicRepository.Update(item, field, name, id);
    }

    public async Task<bool> Search(string item, string name)
    {
        return await musicRepository.Search(item, name);
    }

    private async Task<Music> SaveMusic(IFormFile formFile, int id)
    {
        string uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "Data/Uploads/Music");
        string filePath = Path.Combine(uploadsPath, formFile.FileName);
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        await formFile.CopyToAsync(fileStream);

        return new Music(formFile.FileName, "Data/Uploads/Music/" + formFile.FileName, id);
    }
}