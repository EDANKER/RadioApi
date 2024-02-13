using System.Data.Common;
using Api.Model.ResponseModel.Music;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.Music;

public interface IMusicRepository
{
    public Task<bool> CreateOrSave(string item, Model.RequestModel.Music.Music music);
    public Task<List<GetMusic>> GetId(string item, int id);
    public Task<List<GetMusic>> GetLimit(string item, int limit);
    public Task<List<GetMusic>> GetPlayListTag(string item, int id);
    public Task<bool> DeleteId(string item, int id);
    public Task<bool> Update(string item, string field, string name, int id);
    public Task<bool> Search(string item, string name);
}

public class MusicRepository(IConfiguration configuration, MySqlConnection mySqlConnection, MySqlCommand mySqlCommand) : IMusicRepository
{
    private DbDataReader _dataReader;
    private List<GetMusic> _musicsList;
    private GetMusic _music;

    private readonly string _connect =  configuration.GetConnectionString("MySql");

    public async Task<bool> CreateOrSave(string item, Model.RequestModel.Music.Music music)
    {
        string command = $"INSERT INTO {item} " +
                         $"(name, path, idPlayList) " +
                         $"VALUES(@Name, @Path, @IdPlayList)";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);

        mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = music.Name;
        mySqlCommand.Parameters.Add("@Path", MySqlDbType.LongText).Value = music.Path;
        mySqlCommand.Parameters.Add("@IdPlayList", MySqlDbType.Int32).Value = music.IdPlayList;

        try
        {
            await mySqlCommand.ExecuteNonQueryAsync();
            await mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }

    public async Task<List<GetMusic>> GetId(string item, int id)
    {
        _musicsList = new List<GetMusic>();
        string command = $"SELECT * FROM {item} " +
                         "WHERE id = @Id";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);
        mySqlCommand.Parameters.Add("Id", MySqlDbType.Int32).Value = id;

        _dataReader = await mySqlCommand.ExecuteReaderAsync();
        if (_dataReader.HasRows)
        {
            while (await _dataReader.ReadAsync())
            {
                string name = _dataReader.GetString(1);
                string path = _dataReader.GetString(2);
                int idPlayList = _dataReader.GetInt32(3);

                _music = new GetMusic(id, name, path, idPlayList);
                _musicsList.Add(_music);
            }
        }

        await _dataReader.CloseAsync();
        await mySqlConnection.CloseAsync();

        return _musicsList;
    }

    public async Task<List<GetMusic>> GetLimit(string item, int limit)
    {
        _musicsList = new List<GetMusic>();
        string command = $"SELECT * FROM {item} " +
                         $"LIMIT  @Limit";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);
        mySqlCommand.Parameters.Add("@Limit", MySqlDbType.Int32).Value = limit;


        _dataReader = await mySqlCommand.ExecuteReaderAsync();
        if (_dataReader.HasRows)
        {
            while (await _dataReader.ReadAsync())
            {
                int id = _dataReader.GetInt32(0);
                string name = _dataReader.GetString(1);
                string path = _dataReader.GetString(2);
                int idPlayList = _dataReader.GetInt32(3);

                _music = new GetMusic(id, name, path, idPlayList);
                _musicsList.Add(_music);
            }
        }

        await _dataReader.CloseAsync();
        await mySqlConnection.CloseAsync();

        return _musicsList;
    }

    public async Task<List<GetMusic>> GetPlayListTag(string item, int id)
    {
        _musicsList = new List<GetMusic>();
        string command = $"SELECT * FROM {item} " +
                         $"WHERE IdPlayList = @IdPlayList";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);
        mySqlCommand.Parameters.Add("@IdPlayList", MySqlDbType.Int32).Value = id;
        
        _dataReader = await mySqlCommand.ExecuteReaderAsync();
        if (_dataReader.HasRows)
        {
            while (await _dataReader.ReadAsync())
            {
                string name = _dataReader.GetString(1);
                string path = _dataReader.GetString(2);
                int idPlayList = _dataReader.GetInt32(3);

                _music = new GetMusic(id, name, path, idPlayList);
                _musicsList.Add(_music);
            }
        }

        await _dataReader.CloseAsync();
        await mySqlConnection.CloseAsync();

        return _musicsList;
    }
    
    public async Task<bool> DeleteId(string item, int id)
    {
        string command = $"DELETE FROM {item} " +
                         $"WHERE id = @Id";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);
        mySqlCommand.Parameters.Add("Id", MySqlDbType.Int32).Value = id;

        try
        {
            await mySqlCommand.ExecuteNonQueryAsync();
            await mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }

    public async Task<bool> Update(string item, string field, string name, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET {field} = @Purpose " +
                         $"WHERE id = @Id";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);
        mySqlCommand.Parameters.Add("@Purpose", MySqlDbType.LongText).Value = name;
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;

        try
        {
            await mySqlCommand.ExecuteNonQueryAsync();
            await mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }

    public async Task<bool> Search(string item, string name)
    {
        string command = $"SELECT EXISTS(SELECT * FROM {item} " +
                         $"WHERE name = @Name)";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);
        mySqlCommand.Parameters.Add("Name", MySqlDbType.LongText).Value = name;

        object? exist = await mySqlCommand.ExecuteScalarAsync();
        bool convertBool = Convert.ToBoolean(exist);
        await mySqlConnection.CloseAsync();

        return convertBool;
    }
}