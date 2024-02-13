using System.Data.Common;
using Api.DTO.PlayList;
using Api.Model.ResponseModel.PlayList;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.PlayList;

public interface IPlayListRepository
{
    public Task<bool> CreateOrSave(string item, Model.RequestModel.PlayList.PlayList playLis);
    public Task<List<GetPlayList>> GetId(string item, int id);
    public Task<List<GetPlayList>> GetLimit(string item, int limit);
    public Task<bool> DeleteId(string item, int id);
    public Task<bool> Update(string item, string name, string field, int id);
    public Task<bool> Search(string item, string name);
}

public class PlayListRepository(IConfiguration configuration, MySqlConnection mySqlConnection, MySqlCommand mySqlCommand) : IPlayListRepository
{
    private DbDataReader _dataReader;
    private List<GetPlayList> _playLists;
    private GetPlayList _playList;

    private readonly string _connect = configuration.GetConnectionString("MySql");

    public async Task<bool> CreateOrSave(string item, Model.RequestModel.PlayList.PlayList playList)
    {
        string command = $"INSERT INTO {item} " +
                         "(name, description ,imgPath)" +
                         "VALUES(@Name, @Description,@ImgPath)";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);

        mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = playList.Name;
        mySqlCommand.Parameters.Add("@Description", MySqlDbType.LongText).Value = playList.Description;
        mySqlCommand.Parameters.Add("@ImgPath", MySqlDbType.LongText).Value = playList.ImgPath;

        try
        {
            await mySqlCommand.ExecuteNonQueryAsync();
            await mySqlConnection.CloseAsync();
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
                string description = _dataReader.GetString(2);
                string imgPath = _dataReader.GetString(3);

                _playList = new GetPlayList(id,name, description, imgPath);
                _playLists.Add(_playList);
            }
        }
        
        await _dataReader.CloseAsync();
        await mySqlConnection.CloseAsync();

        return _playLists;
    }

    public async Task<List<GetPlayList>> GetLimit(string item, int limit)
    {
        _playLists = new List<GetPlayList>();
        string command = $"SELECT * FROM {item} " +
                         $"LIMIT @Limit";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);
        mySqlCommand.Parameters.Add("@Limit", MySqlDbType.Int32).Value = limit;

       _dataReader =  await mySqlCommand.ExecuteReaderAsync();
        if (_dataReader.HasRows)
        {
            while (await _dataReader.ReadAsync())
            {
                int id = _dataReader.GetInt32(0);
                string name = _dataReader.GetString(1);
                string description = _dataReader.GetString(2);
                string imgPath = _dataReader.GetString(3);

                _playList = new GetPlayList(id,name, description, imgPath);
                _playLists.Add(_playList);
            }
        }

        await _dataReader.CloseAsync();
        await mySqlConnection.CloseAsync();

        return _playLists;
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

    public async Task<bool> Update(string item, string purpose, string field, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET {field} = @Purpose " +
                         $"WHERE id = @Id";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);
        mySqlCommand.Parameters.Add("@Purpose", MySqlDbType.LongText).Value = purpose;
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