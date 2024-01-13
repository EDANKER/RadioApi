using Microsoft.AspNetCore.Mvc;

namespace Radio.Controller.MusicScript.SavePlayList;

public interface ISavePlayListController
{
    public Task<IActionResult> SaveMusic();
}

public class SavePlayListController : ISavePlayListController
{
    
}