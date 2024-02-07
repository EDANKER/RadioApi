namespace Radio.Model.RequestModel.Scenari;

public class Scenari
{
    public Scenari(string sector, string time)
    {
        Sector = sector;
        Time = time;
    }

    public string Sector { get; set; }
    public string Time { get; set; }
}