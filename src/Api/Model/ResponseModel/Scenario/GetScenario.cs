﻿namespace Api.Model.ResponseModel.Scenario;

public class GetScenario(int id, string sector, string time)
{
    public int Id { get; set; } = id;
    public string Sector { get; set; } = sector;
    public string Time { get; set; } = time;
}