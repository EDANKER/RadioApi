using AutoMapper;

namespace Api.Services.AutoMapperServices;

public interface IAutoMapperServices<One, Two>
{
    public Two Mapping();
}

public class AutoMapperServices<One, Two>(IMapper mapper) : IAutoMapperServices<One, Two> where One : new()
{
    public Two Mapping()
    {
        return mapper.Map<One, Two>(new One());
    }
}