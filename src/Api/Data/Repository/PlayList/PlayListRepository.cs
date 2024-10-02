using System.Data.Common;
using Api.Interface.Repository;
using Api.Model.RequestModel.Create.CreatePlayList;
using Api.Model.RequestModel.Update.UpdatePlayList;
using Api.Model.ResponseModel.PlayList;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.PlayList;

public class PlayListRepository(
    ILogger<PlayListRepository> logger,
    IConfiguration configuration,
    MySqlCommand mySqlCommand) : IRepository<CreatePlayList, DtoPlayList, UpdatePlayList>
{
    private DbDataReader? _dataReader;
    private List<DtoPlayList>? _dtoPlayLists;
    private DtoPlayList? _dtoPlayList;
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

    public async Task<DtoPlayList?> CreateOrSave(string item, CreatePlayList createPlayList)
    {
        string command = $"INSERT INTO {item} " +
                         "(name, description ,imgPath)" +
                         "VALUES(@Name, @Description,@ImgPath)";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);

            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = createPlayList.Name;
            mySqlCommand.Parameters.Add("@Description", MySqlDbType.LongText).Value = createPlayList.Description;
            mySqlCommand.Parameters.Add("@ImgPath", MySqlDbType.LongText).Value = createPlayList.ImgPath;

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
        return await GetField(item, createPlayList.Name, "Name");
    }

    public async Task<List<DtoPlayList>?> GetAll(string item)
    {
        _dtoPlayLists = new List<DtoPlayList>();
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
                    string description = _dataReader.GetString(2);
                    string imgPath = _dataReader.GetString(3);

                    _dtoPlayList = new DtoPlayList(id, name, description, imgPath);
                    _dtoPlayLists.Add(_dtoPlayList);
                }
            }
            return _dtoPlayLists;
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

    public async Task<DtoPlayList?> GetId(string item, int id)
    {
        string command = $"SELECT * FROM {item} " +
                         $"WHERE Id = @Id";

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
                    string description = _dataReader.GetString(2);
                    string imgPath = _dataReader.GetString(3);

                    _dtoPlayList = new DtoPlayList(id, name, description, imgPath);
                }
            }
            return _dtoPlayList;
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

    public async Task<DtoPlayList?> GetField(string item, string namePurpose, string field)
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
                    string description = _dataReader.GetString(2);
                    string imgPath = _dataReader.GetString(3);

                    _dtoPlayList = new DtoPlayList(id, name, description, imgPath);
                }
            }
            return _dtoPlayList;
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

    public async Task<List<DtoPlayList>?> GetLike(string item, string namePurpose, string field)
    {
        _dtoPlayLists = new List<DtoPlayList>();
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
                    string description = _dataReader.GetString(2);
                    string imgPath = _dataReader.GetString(3);

                    _dtoPlayList = new DtoPlayList(id, name, description, imgPath);
                    _dtoPlayLists.Add(_dtoPlayList);
                }
            }
            return _dtoPlayLists;
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

    public async Task<List<DtoPlayList>?> GetLimit(string item, int currentPage, int limit)
    {
        _dtoPlayLists = new List<DtoPlayList>();
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
                    string description = _dataReader.GetString(2);
                    string imgPath = _dataReader.GetString(3);

                    _dtoPlayList = new DtoPlayList(id, name, description, imgPath);
                    _dtoPlayLists.Add(_dtoPlayList);
                }
            }
            return _dtoPlayLists;
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

    public async Task<DtoPlayList?> UpdateId(string item, UpdatePlayList updatePlayList, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET Name = @Name," +
                         $"Description = @Description " +
                         $"WHERE Id = @Id";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = updatePlayList.Name;
            mySqlCommand.Parameters.Add("@Description", MySqlDbType.LongText).Value = updatePlayList.Description;
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

        return await GetField(item, updatePlayList.Name, "Name");
    }
    public async Task<bool> Search(string item, string namePurpose, string field)
    {
        string command = $"SELECT EXISTS(SELECT * FROM {item} " +
                         $"WHERE {field} = @Name)";
        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = namePurpose;

            object? exist = await mySqlCommand.ExecuteScalarAsync();
            bool convertBool = Convert.ToBoolean(exist);
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