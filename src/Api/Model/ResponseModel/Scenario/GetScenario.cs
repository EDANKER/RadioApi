namespace Radio.Model.ResponseModel.Scenari;

public class GetScenario
{
    public GetScenario(int id, string sector, string time)
    {
        Id = id;
        Sector = sector;
        Time = time;
    }

    public int Id { get; set; }
    public string Sector { get; set; }
    public string Time { get; set; }
}