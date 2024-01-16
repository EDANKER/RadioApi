namespace Radio.Repository.UserRepository;

public interface IRepository<T>
{
    public void Save(T item);
    public void GetName(T item, string name);
    public void GetLimit(T item, int limit);
    public void Delete(T item);
    public void DeleteName(T item, string name);
    public void Update(T item);
}

public class Repository<T> : IRepository<T>
{
    public void Save(T item)
    {
        throw new NotImplementedException();
    }

    public void GetName(T item, string name)
    {
        throw new NotImplementedException();
    }

    public void GetLimit(T item, int limit)
    {
        throw new NotImplementedException();
    }

    public void Delete(T item)
    {
        throw new NotImplementedException();
    }

    public void DeleteName(T item, string name)
    {
        throw new NotImplementedException();
    }

    public void Update(T item)
    {
        throw new NotImplementedException();
    }
}