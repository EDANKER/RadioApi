﻿namespace Radio.Model.PlayList;

public class GetPlayList
{
    public GetPlayList(string name, string description, string imgPath)
    {
        Name = name;
        Description = description;
        ImgPath = imgPath;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public string ImgPath { get; set; }
}