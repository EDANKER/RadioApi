using Api.DTO.PlayList;
using Api.Model.ResponseModel.PlayList;

namespace Api.Profile.AppMappingProfile;

public class AppMappingProfile : AutoMapper.Profile
{
    public AppMappingProfile()
    {
        CreateMap<DtoPlayList, GetPlayList>();
    }
}