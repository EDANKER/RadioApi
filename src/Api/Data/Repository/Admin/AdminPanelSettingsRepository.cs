using System.Data.Common;
using Api.Interface.Repository;
using Api.Model.ResponseModel.User;
using Api.Services.JsonServices;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.Admin;

public class AdminPanelSettingsRepository(
    ILogger<AdminPanelSettingsRepository> logger,
    IConfiguration configuration,
    MySqlCommand mySqlCommand,
    IJsonServices<string[]> jsonServices) : IRepository<Model.RequestModel.User.User, DtoUser, Model.RequestModel.User.User>
{
    private DbDataReader? _dataReader;
    private List<DtoUser>? _dtoUsers;
    private DtoUser? _dtoUser;

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
    
    public async Task<DtoUser?> CreateOrSave(string item, Model.RequestModel.User.User user)
    {
        string command = $"INSERT INTO {item} " +
                         "(FullName, Login, Role) " +
                         "VALUES(@FullName, @Login, @Role)";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);

            mySqlCommand.Parameters.Add("@FullName", MySqlDbType.LongText).Value = user.FullName;
            mySqlCommand.Parameters.Add("@Login", MySqlDbType.LongText).Value = user.Login;
            mySqlCommand.Parameters.Add("@Role", MySqlDbType.LongText).Value = jsonServices.SerJson(user.Role);

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
        return await GetField(item, user.Login, "Login");
    }

    public async Task<List<DtoUser>?> GetAll(string item)
    {
        _dtoUsers = new List<DtoUser>();
        string command = $"SELECT * FROM {item} " +
                         "WHERE id = @Id";

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
                    string fullname = _dataReader.GetString(1);
                    string login = _dataReader.GetString(2);
                    string role = _dataReader.GetString(3);

                    _dtoUser = new DtoUser(id, fullname, login, role);
                }
            }
            return _dtoUsers;
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
            await _dataReader.DisposeAsync();
        }
    }

    public async Task<DtoUser?> GetId(string item, int id)
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
                    string fullname = _dataReader.GetString(1);
                    string login = _dataReader.GetString(2);
                    string role = _dataReader.GetString(3);

                    _dtoUser = new DtoUser(id, fullname, login, role);
                }
            }
            return _dtoUser;
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
            await _dataReader.DisposeAsync();
        }
    }

    public async Task<DtoUser?> GetField(string item, string namePurpose, string field)
    {
        string command = $"SELECT * FROM {item} " +
                         $"WHERE {field} = @NamePurpose";
        
        try
        {
            await _mySqlConnection.OpenAsync();
            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.VarChar).Value = namePurpose;

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
            return _dtoUser;
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
            await _dataReader.DisposeAsync();
        }
    }

    public async Task<List<DtoUser>?> GetLike(string item, string namePurpose, string field)
    {
        _dtoUsers = new List<DtoUser>();
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
                    string fullname = _dataReader.GetString(1);
                    string login = _dataReader.GetString(2);
                    string role = _dataReader.GetString(3);

                    _dtoUser = new DtoUser(id, fullname, login, role);
                    _dtoUsers.Add(_dtoUser);
                }
            }
            return _dtoUsers;
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
            await _dataReader.DisposeAsync();
        }
    }

    public async Task<List<DtoUser>?> GetLimit(string item, int currentPage, int limit)
    {
        _dtoUsers = new List<DtoUser>();
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
                    string fullname = _dataReader.GetString(1);
                    string login = _dataReader.GetString(2);
                    string role = _dataReader.GetString(3);

                    _dtoUser = new DtoUser(id, fullname, login, role);
                    _dtoUsers.Add(_dtoUser);
                }
            }
            return _dtoUsers;
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
            await _dataReader.DisposeAsync();
        }
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        string command = $"DELETE FROM {item} " +
                         $"WHERE id = @Id";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
            mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;

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

    public async Task<DtoUser?> UpdateId(string item, Model.RequestModel.User.User user, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET " +
                         $"FullName = @FullName, " +
                         $"Login = @Login, " +
                         $"Role = @Role " +
                         $"WHERE id = @Id";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);

            mySqlCommand.Parameters.Add("@FullName", MySqlDbType.LongText).Value = user.FullName;
            mySqlCommand.Parameters.Add("@Login", MySqlDbType.LongText).Value = user.Login;
            mySqlCommand.Parameters.Add("@Role", MySqlDbType.LongText).Value = user.Role;
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
        return await GetField(item, user.Login, "Login");
    }

    public async Task<bool> Search(string item, string name, string field)
    {
        string command = $"SELECT EXISTS(SELECT * FROM {item} " +
                         $"WHERE {field} = @Login)";
        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
            mySqlCommand.Parameters.Add("@Login", MySqlDbType.LongText).Value = name;

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