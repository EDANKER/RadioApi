using System.Data.Common;
using Api.Model.ResponseModel.Music;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.Music;

public interface IMusicRepository
{
    Task<bool> CreateOrSave(string item, Model.RequestModel.Music.Music? music);
    Task<DtoMusic?> GetId(string item, int id);
    Task<List<DtoMusic>?> GetLimit(string item, int limit);
    Task<List<DtoMusic>?> GetMusicPlayListTag(string item, string name);
    Task<bool> DeleteId(string item, int id);
    Task<bool> Update(string item, string field, string name, int id);
    Task<bool> Search(string item, string name);
}

public class MusicRepository(
    ILogger<MusicRepository> logger,
    IConfiguration configuration,
    MySqlConnection mySqlConnection,
    MySqlCommand mySqlCommand)
    : IMusicRepository
{
    private DbDataReader? _dataReader;
    private List<DtoMusic>? _dtoMusics;
    private DtoMusic? _dtoMusic;

    private readonly string _connect = configuration.GetConnectionString("MySql") ?? string.Empty;

    public async Task<bool> CreateOrSave(string item, Model.RequestModel.Music.Music? music)
    {
        string command = $"INSERT INTO {item} " +
                         $"(name, namePlayList, timeMusic) " +
                         $"VALUES(@Name, @NamePlayList, @TimeMusic)";

        mySqlConnection = new MySqlConnection(_connect);
       
        try
        {
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);

            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = music.Name;
            mySqlCommand.Parameters.Add("@NamePlayList", MySqlDbType.VarChar).Value = music.NamePlayList;
            mySqlCommand.Parameters.Add("@TimeMusic", MySqlDbType.VarChar).Value = music.TimeMusic.ToString();

            await mySqlCommand.ExecuteNonQueryAsync();
            await mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return false;
        }

        return true;
    }

    public async Task<DtoMusic?> GetId(string item, int id)
    {
        _dtoMusics = new List<DtoMusic>();
        string command = $"SELECT * FROM {item} " +
                         "WHERE id = @Id";

        try
        {
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
                    string namePlayList = _dataReader.GetString(2);
                    string timeMusic = _dataReader.GetString(3);

                    _dtoMusic = new DtoMusic(id, name, namePlayList, timeMusic);
                }
            }
            else
            {
                return null;
            }
            
            await _dataReader.CloseAsync();
            await mySqlConnection.CloseAsync();
            
            return _dtoMusic;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
        
    }

    public async Task<List<DtoMusic>?> GetLimit(string item, int limit)
    {
        _dtoMusics = new List<DtoMusic>();
        string command = $"SELECT * FROM {item} " +
                         $"LIMIT  @Limit";

        try
        {
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
                    string namePlayList = _dataReader.GetString(2);
                    string timeMusic = _dataReader.GetString(3);

                    _dtoMusic = new DtoMusic(id, name, namePlayList, timeMusic);
                    _dtoMusics.Add(_dtoMusic);
                }
            }
            else
            {
                return null;
            }

            await _dataReader.CloseAsync();
            await mySqlConnection.CloseAsync();

            return _dtoMusics;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<List<DtoMusic>?> GetMusicPlayListTag(string item, string name)
    {
        _dtoMusics = new List<DtoMusic>();
        string command = $"SELECT * FROM {item} " +
                         $"WHERE namePlayList = @NamePlayList";
        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@NamePlayList", MySqlDbType.VarChar).Value = name;

            _dataReader = await mySqlCommand.ExecuteReaderAsync();
            if (_dataReader.HasRows)
            {
                while (await _dataReader.ReadAsync())
                {
                    int id = _dataReader.GetInt32(0);
                    string nameMusic = _dataReader.GetString(1);
                    string namePlayList = _dataReader.GetString(2);
                    string timeMusic = _dataReader.GetString(3);

                    _dtoMusic = new DtoMusic(id, nameMusic, namePlayList, timeMusic);
                    _dtoMusics.Add(_dtoMusic);
                }
            }
            else
            {
                return null;
            }

            await _dataReader.CloseAsync();
            await mySqlConnection.CloseAsync();

            return _dtoMusics;

        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        string command = $"DELETE FROM {item} " +
                         $"WHERE Id = @Id";

        mySqlConnection = new MySqlConnection(_connect);
        
        try
        {
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("Id", MySqlDbType.Int32).Value = id;
            
            await mySqlCommand.ExecuteNonQueryAsync();
            await mySqlConnection.CloseAsync();
            return true;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }

    public async Task<bool> Update(string item, string field, string name, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET {field} = @Purpose " +
                         $"WHERE id = @Id";

        mySqlConnection = new MySqlConnection(_connect);

        try
        {
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Purpose", MySqlDbType.LongText).Value = name;
            mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;
            
            await mySqlCommand.ExecuteNonQueryAsync();
            await mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return false;
        }

        return true;
    }

    public async Task<bool> Search(string item, string name)
    {
        string command = $"SELECT EXISTS(SELECT * FROM {item} " +
                         $"WHERE Name = @Name)";
        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("Name", MySqlDbType.LongText).Value = name;

            bool convertBool = Convert.ToBoolean(await mySqlCommand.ExecuteScalarAsync());
            await mySqlConnection.CloseAsync();

            return convertBool;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return false;
        }
    }
}