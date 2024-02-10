using AutoMapper;

namespace Api.Services.AutoMapperServices;

public interface IAutoMapperServices<One, Two>
{
    public Two TypeData(One one, Two tWo);
}

public class AutoMapperServices<One, Two>(IMapper mapper) : IAutoMapperServices<One, Two>
{
    public Two TypeData(One one, Two tWo)
    {
        throw new NotImplementedException();
    }
}