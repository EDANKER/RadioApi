using Microsoft.AspNetCore.Mvc;
using Radio.Controller.Music.DeleteMusic;

namespace RadioTest;

[TestFixture]
public class DeleteMusicControllerTest
{
    private IDeleteMusicController _deleteMusicController;
    
    [SetUp]
    public void SetUp()
    {
        _deleteMusicController = new DeleteMusicController();
    }

    [Test]
    public async Task DeleteMusicIdReturnStatusCodeOk()
    {
       var result = await _deleteMusicController.DeleteIdMusic(1);
       Assert.That(result, Is.InstanceOf<OkResult>());
    }

    [Test]
    public async Task DeleteMusicAllReturnStatusCodeOk()
    {
        var result = await _deleteMusicController.DeleteAllMusic();
        Assert.That(result, Is.InstanceOf<OkResult>());
    }
}