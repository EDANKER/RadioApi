using System.Data.Common;
using Api.Interface;
using Api.Model.ResponseModel.PlayList;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.PlayList;

public class PlayListRepository(
    ILogger<PlayListRepository> logger,
    IConfiguration configuration,
    MySqlConnection mySqlConnection,
    MySqlCommand mySqlCommand) : IRepository<Model.RequestModel.PlayList.PlayList, DtoPlayList>
{
    private DbDataReader? _dataReader;
    private List<DtoPlayList>? _dtoPlayLists;
    private DtoPlayList? _dtoPlayList;
    private readonly string _connect = configuration.GetConnectionString("MySql") ?? string.Empty;

    public async Task<bool> CreateOrSave(string item, Model.RequestModel.PlayList.PlayList playList)
    {
        string command = $"INSERT INTO {item} " +
                         "(name, description ,imgPath)" +
                         "VALUES(@Name, @Description,@ImgPath)";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);

            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = playList.Name;
            mySqlCommand.Parameters.Add("@Description", MySqlDbType.LongText).Value = playList.Description;
            mySqlCommand.Parameters.Add("@ImgPath", MySqlDbType.LongText).Value = playList.ImgPath;

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

    public async Task<DtoPlayList?> GetId(string item, int id)
    {
        _dtoPlayLists = new List<DtoPlayList>();
        string command = $"SELECT * FROM {item} " +
                         $"WHERE Id = @Id";

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
                    string description = _dataReader.GetString(2);
                    string imgPath = _dataReader.GetString(3);

                    _dtoPlayList = new DtoPlayList(id, name, description, imgPath);
                }
            }
            else
            {
                return null;
            }

            await _dataReader.CloseAsync();
            await mySqlConnection.CloseAsync();

            return _dtoPlayList;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<List<DtoPlayList>?> GetString(string item, string namePurpose, string field)
    {
        _dtoPlayLists = new List<DtoPlayList>();
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
                    string description = _dataReader.GetString(2);
                    string imgPath = _dataReader.GetString(3);

                    _dtoPlayList = new DtoPlayList(id, name, description, imgPath);
                    _dtoPlayLists.Add(_dtoPlayList);
                }
            }
            else
            {
                return null;
            }

            await mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _dtoPlayLists;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<List<DtoPlayList>?> GetLimit(string item, int limit)
    {
        _dtoPlayLists = new List<DtoPlayList>();
        string command = $"SELECT * FROM {item} " +
                         $"LIMIT @Limit";

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
                    string description = _dataReader.GetString(2);
                    string imgPath = _dataReader.GetString(3);

                    _dtoPlayList = new DtoPlayList(id, name, description, imgPath);
                    _dtoPlayLists.Add(_dtoPlayList);
                }
            }
            else
            {
                return null;
            }

            await _dataReader.CloseAsync();
            await mySqlConnection.CloseAsync();

            return _dtoPlayLists;
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
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateId(string item, Model.RequestModel.PlayList.PlayList model, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET Name = @Name," +
                         $"Description = @Description, " +
                         $"ImgPath = @ImgPath " +
                         $"WHERE Id = @Id";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = model.Name;
            mySqlCommand.Parameters.Add("@Description", MySqlDbType.LongText).Value = model.Description;
            mySqlCommand.Parameters.Add("@ImgPath", MySqlDbType.LongText).Value = model.ImgPath;
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

    public async Task<bool> Search(string item, string namePurpose, string field)
    {
        string command = $"SELECT EXISTS(SELECT * FROM {item} " +
                         $"WHERE {field} = @Name)";
        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = namePurpose;

            object? exist = await mySqlCommand.ExecuteScalarAsync();
            bool convertBool = Convert.ToBoolean(exist);
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