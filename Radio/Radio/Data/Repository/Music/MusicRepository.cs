using System.Data;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Radio.Model.Music;
using Radio.Model.RequestModel.Music;

namespace Radio.Data.Repository;

public interface IMusicRepository
{
    public Task<bool> CreateOrSave(string item, Music music);
    public Task<List<GetMusic>> GetId(string item, int id);
    public Task<List<GetMusic>> GetLimit(string item, int limit);
    public Task<List<GetMusic>> GetPlayListTag(string item, int id);
    public Task<bool> DeleteId(string item, int id);
    public Task<bool> Update(string item, string field, string name);
    public Task<bool> Search(string item, string name);
}

public class MusicRepository : IMusicRepository
{
    private MySqlConnection _mySqlConnection;
    private MySqlCommand _mySqlCommand;
    private DbDataReader _dataReader;
    private List<GetMusic> _musicsList;
    private GetMusic _music;

    private const string Connect = "Server=mysql.students.it-college.ru;Database=i22s0909;Uid=i22s0909;pwd=5x9PVV83;charset=utf8";

    public async Task<bool> CreateOrSave(string item, Music music)
    {
        string command = $"INSERT INTO {item} " +
                         $"(name, path, idPlayList) " +
                         $"VALUES(@Name, @Path, @IdPlayList)";

        _mySqlConnection = new MySqlConnection(Connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);

        _mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = music.Name;
        _mySqlCommand.Parameters.Add("@Path", MySqlDbType.LongText).Value = music.Path;
        _mySqlCommand.Parameters.Add("@IdPlayList", MySqlDbType.Int32).Value = music.IdPlayList;

        try
        {
            await _mySqlCommand.ExecuteNonQueryAsync();
            await _mySqlConnection.CloseAsync();
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

        _mySqlConnection = new MySqlConnection(Connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("Id", MySqlDbType.Int32).Value = id;

        _dataReader = await _mySqlCommand.ExecuteReaderAsync();
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
        await _mySqlConnection.CloseAsync();

        return _musicsList;
    }

    public async Task<List<GetMusic>> GetLimit(string item, int limit)
    {
        _musicsList = new List<GetMusic>();
        string command = $"SELECT * FROM {item} " +
                         $"LIMIT  @Limit";

        _mySqlConnection = new MySqlConnection(Connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@Limit", MySqlDbType.Int32).Value = limit;


        _dataReader = await _mySqlCommand.ExecuteReaderAsync();
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
        await _mySqlConnection.CloseAsync();

        return _musicsList;
    }

    public async Task<List<GetMusic>> GetPlayListTag(string item, int id)
    {
        _musicsList = new List<GetMusic>();
        string command = $"SELECT * FROM {item} " +
                         $"WHERE IdPlayList = @IdPlayList";

        _mySqlConnection = new MySqlConnection(Connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@IdPlayList", MySqlDbType.Int32).Value = id;
        
        _dataReader = await _mySqlCommand.ExecuteReaderAsync();
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
        await _mySqlConnection.CloseAsync();

        return _musicsList;
    }
    
    public async Task<bool> DeleteId(string item, int id)
    {
        string command = $"DELETE FROM {item} " +
                         $"WHERE id = @Id";

        _mySqlConnection = new MySqlConnection(Connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("Id", MySqlDbType.Int32).Value = id;

        try
        {
            await _mySqlCommand.ExecuteNonQueryAsync();
            await _mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }

    public async Task<bool> Update(string item, string field, string name)
    {
        string command = $"UPDATE {item} " +
                         $"SET {field} = @Purpose";

        _mySqlConnection = new MySqlConnection(Connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@Purpose", MySqlDbType.LongText).Value = name;

        try
        {
            await _mySqlCommand.ExecuteNonQueryAsync();
            await _mySqlConnection.CloseAsync();
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

        _mySqlConnection = new MySqlConnection(Connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("Name", MySqlDbType.LongText).Value = name;

        object? exist = await _mySqlCommand.ExecuteScalarAsync();
        bool convertBool = Convert.ToBoolean(exist);
        await _mySqlConnection.CloseAsync();

        return convertBool;
    }
}