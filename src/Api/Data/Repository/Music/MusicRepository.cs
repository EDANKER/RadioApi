using System.Data.Common;
using Api.Interface.Repository;
using Api.Model.RequestModel.Create.CreateMusic;
using Api.Model.RequestModel.Update.UpdateMusic;
using Api.Model.ResponseModel.Music;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.Music;

public class MusicRepository(
    ILogger<MusicRepository> logger,
    IConfiguration configuration,
    MySqlCommand mySqlCommand)
    : IRepository<CreateMusic, DtoMusic, UpdateMusic>
{
    private DbDataReader? _dataReader;
    private List<DtoMusic>? _dtoMusics;
    private DtoMusic? _dtoMusic;

    private readonly MySqlConnection _mySqlConnection = new(configuration.GetConnectionString("MySql"));

    public async Task<int> GetCount(string item)
    {
        try
        {
            string command = $"SELECT COUNT(*) FROM {item}";

            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);

            object? count = await mySqlCommand.ExecuteScalarAsync();
            if (count != null)
                return Convert.ToInt32(count);
            return -1;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return -1;
        }
        finally
        {
            await _mySqlConnection.DisposeAsync();
            await mySqlCommand.DisposeAsync();
        }
    }

    public async Task<DtoMusic?> CreateOrSave(string item, CreateMusic createMusic)
    {
        string command = $"INSERT INTO {item} " +
                         $"(name, namePlayList, timeMusic) " +
                         $"VALUES(@Name, @NamePlayList, @TimeMusic)";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);

            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = createMusic.Name;
            mySqlCommand.Parameters.Add("@NamePlayList", MySqlDbType.VarChar).Value = createMusic.NamePlayList;
            mySqlCommand.Parameters.Add("@TimeMusic", MySqlDbType.VarChar).Value = createMusic.TimeMusic.ToString();

            await mySqlCommand.ExecuteNonQueryAsync();
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
        finally
        {
            await _mySqlConnection.DisposeAsync();
            await mySqlCommand.DisposeAsync();
        }

        return await GetField(item, createMusic.Name, "Name");
    }

    public async Task<List<DtoMusic>?> GetAll(string item)
    {
        _dtoMusics = new List<DtoMusic>();
        string command = $"SELECT * FROM {item} ";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);

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
            return _dtoMusics;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
        finally
        {
            await _dataReader.DisposeAsync();
            await _mySqlConnection.DisposeAsync();
            await mySqlCommand.DisposeAsync();
        }
    }

    public async Task<DtoMusic?> GetId(string item, int id)
    {
        string command = $"SELECT * FROM {item} " +
                         "WHERE id = @Id";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
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
            return _dtoMusic;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
        finally
        {
            await _dataReader.DisposeAsync();
            await _mySqlConnection.DisposeAsync();
            await mySqlCommand.DisposeAsync();
        }
    }

    public async Task<DtoMusic?> GetField(string item, string namePurpose, string field)
    {
        string command = $"SELECT * FROM {item} " +
                         $"WHERE {field} = @NamePurpose";
        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
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
                }
            }
            return _dtoMusic;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
        finally
        {
            await _dataReader.DisposeAsync();
            await _mySqlConnection.DisposeAsync();
            await mySqlCommand.DisposeAsync();
        }
    }

    public async Task<List<DtoMusic>?> GetLike(string item, string namePurpose, string field)
    {
        _dtoMusics = new List<DtoMusic>();
        string command = $"SELECT * FROM {item} " +
                         $"WHERE {field} LIKE @NamePurpose ";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
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
            return _dtoMusics;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
        finally
        {
            await _dataReader.DisposeAsync();
            await _mySqlConnection.DisposeAsync();
            await mySqlCommand.DisposeAsync();
        }
    }

    public async Task<List<DtoMusic>?> GetLimit(string item, int currentPage, int limit)
    {
        _dtoMusics = new List<DtoMusic>();
        string command = $"SELECT * FROM {item} " +
                         $"LIMIT @Limit " +
                         $"OFFSET @Sum";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
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
            return _dtoMusics;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
        finally
        {
            await _dataReader.DisposeAsync();
            await _mySqlConnection.DisposeAsync();
            await mySqlCommand.DisposeAsync();
        }
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        string command = $"DELETE FROM {item} " +
                         $"WHERE Id = @Id";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
            mySqlCommand.Parameters.Add("Id", MySqlDbType.Int32).Value = id;

            await mySqlCommand.ExecuteNonQueryAsync();
            return true;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return false;
        }
        finally
        {
            await _mySqlConnection.DisposeAsync();
            await mySqlCommand.DisposeAsync();
        }
    }

    public async Task<DtoMusic?> UpdateId(string item, UpdateMusic model, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET Name = @Name, " +
                         $"NamePlayList = @NamePlayList " +
                         $"WHERE id = @Id";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = model.Name;
            mySqlCommand.Parameters.Add("@NamePlayList", MySqlDbType.LongText).Value = model.NamePlayList;
            mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;

            await mySqlCommand.ExecuteNonQueryAsync();
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
        finally
        {
            await _mySqlConnection.DisposeAsync();
            await mySqlCommand.DisposeAsync();
        }

        return await GetId(item, id);
    }


    public async Task<bool> Search(string item, string name, string field)
    {
        string command = $"SELECT EXISTS(SELECT * FROM {item} " +
                         $"WHERE {field} = @Name)";
        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
            mySqlCommand.Parameters.Add("Name", MySqlDbType.LongText).Value = name;

            bool convertBool = Convert.ToBoolean(await mySqlCommand.ExecuteScalarAsync());
            return convertBool;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return false;
        }
        finally
        {
            await _mySqlConnection.DisposeAsync();
            await mySqlCommand.DisposeAsync();
        }
    }
}