using System.Data.Common;
using Api.Interface;
using Api.Model.RequestModel.Create.CreateMusic;
using Api.Model.RequestModel.Update.UpdateMusic;
using Api.Model.ResponseModel.Music;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.Music;

public class MusicRepository(
    ILogger<MusicRepository> logger,
    IConfiguration configuration,
    MySqlConnection mySqlConnection,
    MySqlCommand mySqlCommand)
    : IRepository<CreateMusic, DtoMusic, UpdateMusic>
{
    private DbDataReader? _dataReader;
    private List<DtoMusic>? _dtoMusics;
    private DtoMusic? _dtoMusic;

    private readonly string _connect = configuration.GetConnectionString("MySql") ?? string.Empty;

    public async Task<int> GetCount(string item)
    { 
        string command = $"SELECT COUNT(*) FROM {item}";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);
        
        object? count = await mySqlCommand.ExecuteScalarAsync();
        await mySqlConnection.CloseAsync();

        if (count != null)
            return Convert.ToInt32(count);
        
        return -1;
    }
    
    public async Task<bool> CreateOrSave(string item, CreateMusic createMusic)
    {
        string command = $"INSERT INTO {item} " +
                         $"(name, namePlayList, timeMusic) " +
                         $"VALUES(@Name, @NamePlayList, @TimeMusic)";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);

            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = createMusic.Name;
            mySqlCommand.Parameters.Add("@NamePlayList", MySqlDbType.VarChar).Value = createMusic.NamePlayList;
            mySqlCommand.Parameters.Add("@TimeMusic", MySqlDbType.VarChar).Value = createMusic.TimeMusic.ToString();

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

    public async Task<List<DtoMusic>?> GetAll(string item)
    {
        _dtoMusics = new List<DtoMusic>();
        string command = $"SELECT * FROM {item} ";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);

            _dataReader = await mySqlCommand.ExecuteReaderAsync();
            if (_dataReader.HasRows)
            {
                while (await _dataReader.ReadAsync())
                {
                    int id = _dataReader.GetInt32(0);
                    string name = _dataReader.GetString(1);
                    string namePlayList = _dataReader.GetString(2);
                    int timeMusic = _dataReader.GetInt32(3);

                    _dtoMusic = new DtoMusic(id, name, namePlayList, timeMusic);
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

    public async Task<DtoMusic?> GetId(string item, int id)
    {
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
                    int timeMusic = _dataReader.GetInt32(3);

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

    public async Task<List<DtoMusic>?> GetField(string item, string namePurpose, string field)
    {
        _dtoMusics = new List<DtoMusic>();
        string command = $"SELECT * FROM {item} " +
                         $"WHERE {field} = @NamePurpose";
        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();
            
            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.AddWithValue("@NamePurpose", namePurpose);
            
            _dataReader = await mySqlCommand.ExecuteReaderAsync();
            if (_dataReader.HasRows)
            {
                while (await _dataReader.ReadAsync())
                {
                    int id = _dataReader.GetInt32(0);
                    string name = _dataReader.GetString(1);
                    string namePlayList = _dataReader.GetString(2);
                    int timeMusic = _dataReader.GetInt32(3);

                    _dtoMusic = new DtoMusic(id, name, namePlayList, timeMusic);
                    _dtoMusics.Add(_dtoMusic);
                }
            }
            else
            {
                return null;
            }

            await mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _dtoMusics;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<List<DtoMusic>?> GetLike(string item, string namePurpose, string field)
    {
        _dtoMusics = new List<DtoMusic>();
        string command = $"SELECT * FROM {item} " +
                         $"WHERE {field} LIKE @NamePurpose ";
        
        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@NamePurpose", MySqlDbType.String).Value = $"%{namePurpose}%";

            _dataReader = await mySqlCommand.ExecuteReaderAsync();
            if (_dataReader.HasRows)
            {
                while (await _dataReader.ReadAsync())
                {
                    int id = _dataReader.GetInt32(0);
                    string name = _dataReader.GetString(1);
                    string namePlayList = _dataReader.GetString(2);
                    int timeMusic = _dataReader.GetInt32(3);

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

    public async Task<List<DtoMusic>?> GetLimit(string item, int currentPage, int limit)
    {
        _dtoMusics = new List<DtoMusic>();
        string command = $"SELECT * FROM {item} " +
                         $"LIMIT @Limit " +
                         $"OFFSET @Sum";;

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Limit", MySqlDbType.Int32).Value = limit;
            mySqlCommand.Parameters.Add("@Sum", MySqlDbType.Int32).Value = (currentPage - 1) * limit;

            _dataReader = await mySqlCommand.ExecuteReaderAsync();
            if (_dataReader.HasRows)
            {
                while (await _dataReader.ReadAsync())
                {
                    int id = _dataReader.GetInt32(0);
                    string name = _dataReader.GetString(1);
                    string namePlayList = _dataReader.GetString(2);
                    int timeMusic = _dataReader.GetInt32(3);

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

    public async Task<bool> DeleteId(string item, int id)
    {
        string command = $"DELETE FROM {item} " +
                         $"WHERE Id = @Id";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
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

    public async Task<bool> UpdateId(string item, UpdateMusic model, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET Name = @Name, " +
                         $"NamePlayList = @NamePlayList "+
                         $"WHERE id = @Id";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = model.Name;
            mySqlCommand.Parameters.Add("@NamePlayList", MySqlDbType.LongText).Value = model.NamePlayList;
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

  

    public async Task<bool> Search(string item, string name, string field)
    {
        string command = $"SELECT EXISTS(SELECT * FROM {item} " +
                         $"WHERE {field} = @Name)";
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