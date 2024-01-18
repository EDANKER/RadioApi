using Npgsql;

namespace Radio.Repository.Repository;

public interface IRepository<T>
{
    public Task Save(T item);
    public void GetName(T item, string name);
    public void GetLimit(T item, int limit);
    public void Delete(T item);
    public void DeleteName(T item, string name);
    public void Update(T item, string what);
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
                         $"VALUES(@Name, @Lofin, @Speak, " +
                         $"@SettingsTime, @SettingsUser, TurnItOneMusic)";

        _npgsqlConnection = new NpgsqlConnection(_connect);
        await _npgsqlConnection.OpenAsync();

        _npgsqlCommand = new NpgsqlCommand(command, _npgsqlConnection);
        _npgsqlCommand.Parameters.Add("");

        await _npgsqlCommand.ExecuteNonQueryAsync();
        await _npgsqlConnection.CloseAsync();
    }

    public void GetName(T item, string name)
    {
        string command = $"SELECT * FROM {item} " +
                         "WHERE name = @Name";
    }

    public void GetLimit(T item, int limit)
    {
        string command = $"SELECT * FROM {item}" +
                         $" LIMIT {limit}";
    }

    public void Delete(T item)
    {
        string command = $"DELETE FROM {item}";
    }

    public void DeleteName(T item, string name)
    {
        string command = $"DELETE FROM {item} " +
                         $"WHERE name = @Name";
    }

    public void Update(T item, string what)
    {
        string command = $"UPDATE {item} " +
                         $"SET {what} = @What";
    }
}