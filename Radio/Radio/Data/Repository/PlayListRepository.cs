using System.Data.Common;
using MySql.Data.MySqlClient;
using Radio.Model.PlayList;

namespace Radio.Data.Repository;

public interface IPlayListRepository
{
    public Task CreateOrSave(string item, PlayList playLis);
    public Task<DbDataReader> GetId(string item, int id);
    public Task<DbDataReader> GetLimit(string item, int limit);
    public Task<DbDataReader> GetName(string item, string name);
    public Task Delete(string item, string name);
    public Task DeleteName(string item, string name);
    public Task Update(string item, string name);
}

public class PlayListRepository : IPlayListRepository
{
    private string _connect;
    private MySqlConnection _mySqlConnection;
    private MySqlCommand _mySqlCommand;
    private DbDataReader _dataReader;

    public PlayListRepository(IConfiguration configuration, MySqlCommand mySqlCommand, MySqlConnection mySqlConnection,
        DbDataReader dataReader)
    {
        _mySqlConnection = mySqlConnection;
        _dataReader = dataReader;
        _mySqlCommand = mySqlCommand;
        _connect = configuration.GetConnectionString("MySql");
    }

    public async Task CreateOrSave(string item, PlayList playList)
    {
        const string command = $"INSERT INTO @Item" +
                               $"(name, description ,imgPath,)" +
                               $"VALUES(@Name, @Description,@ImgPath)";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);

        _mySqlCommand.Parameters.Add("@Item", MySqlDbType.String).Value = item;
        _mySqlCommand.Parameters.Add("@Name", MySqlDbType.VarChar).Value = playList.Name;
        _mySqlCommand.Parameters.Add("@Description", MySqlDbType.VarChar).Value = playList.Description;
        _mySqlCommand.Parameters.Add("@ImgPath", MySqlDbType.VarChar).Value = playList.ImgPath;

        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();
    }

    public async Task<DbDataReader> GetId(string item, int id)
    {
        const string command = $"SELECT * FROM @Item " +
                               "WHERE id = @Id";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("Item", MySqlDbType.String).Value = item;
        _mySqlCommand.Parameters.Add("Id", MySqlDbType.Int64).Value = id;

        _dataReader = await _mySqlCommand.ExecuteReaderAsync();

        await _mySqlConnection.CloseAsync();

        return _dataReader;
    }

    public async Task<DbDataReader> GetLimit(string item, int limit)
    {
        string command = $"SELECT * FROM @Item" +
                         $" LIMIT = @Limit";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@Limit", MySqlDbType.Int64).Value = limit;
        _mySqlCommand.Parameters.Add("@Item", MySqlDbType.Int64).Value = item;


        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();

        return _dataReader;
    }

    public async Task<DbDataReader> GetName(string item, string name)
    {
        string command = $"SELECT * FROM @Item" +
                         $"WHERE name = @Name";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@Name", MySqlDbType.VarChar).Value = name;
        _mySqlCommand.Parameters.Add("@Item", MySqlDbType.Int64).Value = item;


        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();

        return _dataReader;
    }

    public async Task Delete(string item, string name)
    {
        string command = $"DELETE FROM {item}";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);


        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();
    }

    public async Task DeleteName(string item, string name)
    {
        string command = $"DELETE FROM {item} " +
                         $"WHERE name = @Name";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);


        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();
    }

    public async Task Update(string item, string name)
    {
        string command = $"UPDATE {item} " +
                         $"SET {name} = @What";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);


        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();
    }
}