using Npgsql;

namespace Radio.Repository.UserRepository;

public interface IRepository<T>
{
    public Task Save(T item);
    public void GetName(T item, string name);
    public void GetLimit(T item, int limit);
    public void Delete(T item);
    public void DeleteName(T item, string name);
    public void Update(T item);
}

public class Repository<T> : IRepository<T>
{
    private string _connect;
    private NpgsqlConnection _npgsqlConnection;
    private NpgsqlCommand _npgsqlCommand;

    public Repository(IConfiguration configuration, NpgsqlCommand npgsqlCommand, NpgsqlConnection npgsqlConnection)
    {
        _npgsqlConnection = npgsqlConnection;
        _npgsqlCommand = npgsqlCommand;
        _connect = configuration.GetConnectionString("PostGre");
    }

    public async Task Save(T item)
    {
        string command = $"INSERT INTO {item}" +
                         $"(name, login, speak, settingsTime, " +
                         $"SettingsUser, TurnItOneMusic) " +
                         $"VALUES()";

        _npgsqlConnection = new NpgsqlConnection(_connect);
        await _npgsqlConnection.OpenAsync();
        
        _npgsqlCommand = new NpgsqlCommand(command, _npgsqlConnection);
        _npgsqlCommand.Parameters.Add("");
        
        await _npgsqlCommand.ExecuteNonQueryAsync();
        await _npgsqlConnection.CloseAsync();
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