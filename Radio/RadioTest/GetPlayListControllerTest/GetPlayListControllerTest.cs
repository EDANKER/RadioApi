using Microsoft.AspNetCore.Mvc;
using Radio.Controller.MusicScript.GetPlayList;

namespace RadioTest.GetPlayListControllerTest;

[TestFixture]
public class GetPlayListControllerTest
{
    private IGetPlayListController _playListController;
    
    [SetUp]
    public void SetUp()
    {
        _playListController = new GetPlayListController();
    }

    [Test]
    public async Task GetPlayListLimitReturnStatusCodeOk()
    {
       var result = await _playListController.GetLimitPlayList(5);
       Assert.That(result, Is.InstanceOf<OkResult>());
    }

    [Test]
    public async Task GetPlayListNameReturnStatusCodeOk()
    {
        var result = await _playListController.GetNamePlayList("Audio");
        Assert.That(result, Is.InstanceOf<OkResult>());
    }
}