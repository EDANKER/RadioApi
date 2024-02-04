using System.Data;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MySql.Data.MySqlClient;
using Radio.Model.PlayList;

namespace Radio.Data.Repository.PlayList;

public interface IPlayListRepository
{
    public Task<bool> CreateOrSave(string item, Model.PlayList.PlayList playLis);
    public Task<List<GetPlayList>> GetId(string item, int id);
    public Task<List<GetPlayList>> GetLimit(string item, int limit);
    public Task<bool> DeleteId(string item, int id);
    public Task<bool> Update(string item, string name, string field, int id);
}

public class PlayListRepository : IPlayListRepository
{
    private MySqlConnection _mySqlConnection;
    private MySqlCommand _mySqlCommand;
    private DbDataReader _dataReader;
    private List<GetPlayList> _playLists;
    private GetPlayList _playList;

    private string _connect =
        "Server=mysql.students.it-college.ru;Database=i22s0909;" +
        "Uid=i22s0909;pwd=5x9PVV83;charset=utf8";

    public async Task<bool> CreateOrSave(string item, Model.PlayList.PlayList playList)
    {
        string command = $"INSERT INTO {item}" +
                         "(name, description ,imgPath)" +
                         "VALUES(@Name, @Description,@ImgPath)";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);

        _mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = playList.Name;
        _mySqlCommand.Parameters.Add("@Description", MySqlDbType.LongText).Value = playList.Description;
        _mySqlCommand.Parameters.Add("@ImgPath", MySqlDbType.LongText).Value = playList.ImgPath;

        try
        {
            await _mySqlCommand.ExecuteNonQueryAsync();
            await _mySqlConnection.CloseAsync();
            return true;
        }
        catch (MySqlException e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<List<GetPlayList>> GetId(string item, int id)
    {
        _playLists = new List<GetPlayList>();
        string command = $"SELECT * FROM {item} " +
                         $"WHERE Id = @Id";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("Id", MySqlDbType.Int64).Value = id;

        _dataReader = await _mySqlCommand.ExecuteReaderAsync();
        
        if (_dataReader.HasRows)
        {
            while (await _dataReader.ReadAsync())
            {
                object name = _dataReader.GetValue(1);
                object description = _dataReader.GetValue(2);
                object imgPath = _dataReader.GetValue(3);

                _playList = new GetPlayList(id,name.ToString(), description.ToString(), imgPath.ToString());
                _playLists.Add(_playList);
            }
        }
        
        await _mySqlConnection.CloseAsync();

        return _playLists;
    }

    public async Task<List<GetPlayList>> GetLimit(string item, int limit)
    {
        _playLists = new List<GetPlayList>();
        string command = $"SELECT * FROM {item} " +
                         $"LIMIT @Limit";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@Limit", MySqlDbType.Int64).Value = limit;

       _dataReader =  await _mySqlCommand.ExecuteReaderAsync();
        if (_dataReader.HasRows)
        {
            while (await _dataReader.ReadAsync())
            {
                object id = _dataReader.GetValue(0);
                object name = _dataReader.GetValue(1);
                object description = _dataReader.GetValue(2);
                object imgPath = _dataReader.GetValue(3);

                _playList = new GetPlayList((int)id,name.ToString(), description.ToString(), imgPath.ToString());
                _playLists.Add(_playList);
            }
        }
        await _mySqlConnection.CloseAsync();

        return _playLists;
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        string command = $"DELETE FROM {item} " +
                         $"WHERE id = @Id";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("Id", MySqlDbType.Int64).Value = id;

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

    public async Task<bool> Update(string item, string purpose, string field, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET {field} = @Purpose " +
                         $"WHERE id = @Id";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@Purpose", MySqlDbType.LongText).Value = purpose;
        _mySqlCommand.Parameters.Add("Id", MySqlDbType.Int64).Value = id;

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
}