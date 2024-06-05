using System.Data.Common;
using Api.Interface;
using Api.Model.ResponseModel.User;
using Api.Services.JsonServices;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.User;

public class UserRepository(
    IConfiguration configuration,
    ILogger<UserRepository> logger,
    MySqlConnection mySqlConnection,
    MySqlCommand mySqlCommand,
    IJsonServices<string[]> jsonServices) : IRepository<Model.RequestModel.User.User, DtoUser, Model.RequestModel.User.User>
{
    private DbDataReader? _dataReader;
    private List<DtoUser>? _dtoUsers;
    private DtoUser? _dtoUser;

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
    
    public async Task<bool> CreateOrSave(string item, Api.Model.RequestModel.User.User user)
    {
        string command = $"INSERT INTO {item} " +
                         "(FullName, Login, Role) " +
                         "VALUES(@FullName, @Login, @Role)";

        try
        { 
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);

            mySqlCommand.Parameters.Add("@FullName", MySqlDbType.LongText).Value = user.FullName;
            mySqlCommand.Parameters.Add("@Login", MySqlDbType.LongText).Value = user.Login;
            mySqlCommand.Parameters.Add("@Role", MySqlDbType.LongText).Value = jsonServices.SerJson(user.Role);
            
            await mySqlCommand.ExecuteNonQueryAsync();
            await mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            logger.LogWarning(e.ToString());
            return false;
        }

        return true;
    }

    public async Task<List<DtoUser>?> GetAll(string item)
    {
        _dtoUsers = new List<DtoUser>();
        string command = $"SELECT * FROM {item} " +
                         "WHERE id = @Id";
        
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
                    string fullname = _dataReader.GetString(1);
                    string login = _dataReader.GetString(2);
                    string role = _dataReader.GetString(3);

                    _dtoUser = new DtoUser(id, fullname, login, role);
                }
            }
            else
            {
                return null;
            }

            await mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _dtoUsers;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<DtoUser?> GetId(string item, int id)
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
                    string fullname = _dataReader.GetString(1);
                    string login = _dataReader.GetString(2);
                    string role = _dataReader.GetString(3);

                    _dtoUser = new DtoUser(id, fullname, login, role);
                }
            }
            else
            {
                return null;
            }

            await mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _dtoUser;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<List<DtoUser>?> GetField(string item, string namePurpose, string field)
    {
        _dtoUsers = new List<DtoUser>();
        string command = $"SELECT * FROM {item} " +
                         $"WHERE {field} = @NamePurpose";
        
        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();
            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.AddWithValue("@Name", namePurpose);

            _dataReader = await mySqlCommand.ExecuteReaderAsync();

            if (_dataReader.HasRows)
            {
                while (await _dataReader.ReadAsync())
                {
                    int id = _dataReader.GetInt32(0);
                    string fullname = _dataReader.GetString(1);
                    string login = _dataReader.GetString(2);
                    string role = _dataReader.GetString(3);

                    _dtoUser = new DtoUser(id, fullname, login, role);
                    _dtoUsers.Add(_dtoUser);
                }
            }
            else
            {
                return null;
            }

            await _dataReader.CloseAsync();
            await mySqlConnection.CloseAsync();

            return _dtoUsers;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<List<DtoUser>?> GetLike(string item, string namePurpose, string field)
    {
        _dtoUsers = new List<DtoUser>();
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
                    string fullname = _dataReader.GetString(1);
                    string login = _dataReader.GetString(2);
                    string role = _dataReader.GetString(3);

                    _dtoUser = new DtoUser(id, fullname, login, role);
                    _dtoUsers.Add(_dtoUser);
                }
            }
            else
            {
                return null;
            }

            await mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _dtoUsers;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<List<DtoUser>?> GetLimit(string item, int currentPage, int limit)
    {
        _dtoUsers = new List<DtoUser>();
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
                    string fullname = _dataReader.GetString(1);
                    string login = _dataReader.GetString(2);
                    string role = _dataReader.GetString(3);

                    _dtoUser = new DtoUser(id, fullname, login, role);
                    _dtoUsers.Add(_dtoUser);
                }
            }
            else
            {
                return null;
            }

            await mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _dtoUsers;
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
                         $"WHERE id = @Id";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;

            await mySqlCommand.ExecuteNonQueryAsync();
            await mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            logger.LogWarning(e.ToString());
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateId(string item, Api.Model.RequestModel.User.User user, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET " +
                         $"FullName = @FullName, " +
                         $"Login = @Login, " +
                         $"Role = @Role " +
                         $"WHERE id = @Id";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);

            mySqlCommand.Parameters.Add("@FullName", MySqlDbType.LongText).Value = user.FullName;
            mySqlCommand.Parameters.Add("@Login", MySqlDbType.LongText).Value = user.Login;
            mySqlCommand.Parameters.Add("@Role", MySqlDbType.LongText).Value = user.Role;
            mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;
            
            await mySqlCommand.ExecuteNonQueryAsync();
            await mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            logger.LogWarning(e.ToString());
            return false;
        }

        return true;
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        string command = $"SELECT EXISTS(SELECT * FROM {item} " +
                         $"WHERE {field} = @Login)";
        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Login", MySqlDbType.LongText).Value = name;

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