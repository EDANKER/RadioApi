using Microsoft.AspNetCore.Mvc;
using Radio.Controller.MusicScript.SavePlayList;

namespace RadioTest;

[TestFixture]
public class SaveMusicControllerTest
{
    private ISaveMusicController _playListController;
    
    [SetUp]
    public void SetUp()
    {
        _playListController = new SaveMusicController();
    }

    [Test]
    public async Task SavePlayListReturnStatusCodeOk()
    {
        var result =  await _playListController.SaveMusic();
        Assert.That(result, Is.InstanceOf<OkResult>());
    }
}