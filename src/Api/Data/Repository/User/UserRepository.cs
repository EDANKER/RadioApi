using System.Data.Common;
using Api.Model.ResponseModel.User;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.User;

public interface IUserRepository
{
    Task<bool> CreateOrSave(string item, Model.RequestModel.User.User user);
    Task<List<DtoUser>> GetId(string item, int id);
    Task<List<DtoUser>> GetLimit(string item, int limit);
    Task<List<DtoUser>> GetName(string item, string name);
    Task<bool> DeleteId(string item, int id);
    Task<bool> Update(string item, Api.Model.RequestModel.User.User user, int id);
    Task<bool> Search(string item, string name, string login);
}

public class UserRepository(
    IConfiguration configuration,
    ILogger<IUserRepository> _logger,
    MySqlConnection mySqlConnection,
    MySqlCommand mySqlCommand) : IUserRepository
{
    private DbDataReader _dataReader;
    private List<DtoUser> _getUsers;
    private DtoUser _user;

    private readonly string _connect = configuration.GetConnectionString("MySql");

    public async Task<bool> CreateOrSave(string item, Api.Model.RequestModel.User.User user)
    {
        string command = $"INSERT INTO {item} " +
                         "(FullName, Login, Role) " +
                         "VALUES(@FullName, @Login, @Role)";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);

        mySqlCommand.Parameters.Add("@FullName", MySqlDbType.LongText).Value = user.FullName;
        mySqlCommand.Parameters.Add("@Login", MySqlDbType.LongText).Value = user.Login;
        mySqlCommand.Parameters.Add("@Role", MySqlDbType.LongText).Value = user.Role;

        try
        {
            await mySqlCommand.ExecuteNonQueryAsync();
            await mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            _logger.LogWarning(e.ToString());
            return false;
        }

        return true;
    }

    public async Task<List<DtoUser>> GetId(string item, int id)
    {
        _getUsers = new List<DtoUser>();
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

                    _user = new DtoUser(id, fullname, login, role);
                    _getUsers.Add(_user);
                }
            }

            await mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _getUsers;
        }
        catch (MySqlException e)
        {
            _logger.LogError(e.ToString());
            throw;
        }
    }

    public async Task<List<DtoUser>> GetLimit(string item, int limit)
    {
        _getUsers = new List<DtoUser>();
        string command = $"SELECT * FROM {item} " +
                         "LIMIT @Limit";
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
                    string fullname = _dataReader.GetString(1);
                    string login = _dataReader.GetString(2);
                    string role = _dataReader.GetString(3);

                    _user = new DtoUser(id, fullname, login, role);
                    _getUsers.Add(_user);
                }
            }

            await mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _getUsers;
        }
        catch (MySqlException e)
        {
            _logger.LogError(e.ToString());
            throw;
        }
    }

    public async Task<List<DtoUser>> GetName(string item, string name)
    {
        _getUsers = new List<DtoUser>();
        string command = $"SELECT * FROM  {item} " +
                         $"WHERE Name = @Name";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);
        mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = name;

        _dataReader = await mySqlCommand.ExecuteReaderAsync();

        if (_dataReader.HasRows)
        {
            while (await _dataReader.ReadAsync())
            {
                int id = _dataReader.GetInt32(0);
                string fullname = _dataReader.GetString(1);
                string login = _dataReader.GetString(2);
                string role = _dataReader.GetString(3);

                _user = new DtoUser(id, fullname, login, role);
                _getUsers.Add(_user);
            }
        }

        await _dataReader.CloseAsync();
        await mySqlConnection.CloseAsync();

        return _getUsers;
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
            _logger.LogWarning(e.ToString());
            return false;
        }

        return true;
    }

    public async Task<bool> Update(string item, Api.Model.RequestModel.User.User user, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET " +
                         $"FullName = @FullName, Login = " +
                         $"@Login, Role = @Role " +
                         $"WHERE id = @Id";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);

        mySqlCommand.Parameters.Add("@FullName", MySqlDbType.LongText).Value = user.FullName;
        mySqlCommand.Parameters.Add("@Login", MySqlDbType.LongText).Value = user.Login;
        mySqlCommand.Parameters.Add("@Role", MySqlDbType.LongText).Value = user.Role;
        mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;

        try
        {
            await mySqlCommand.ExecuteNonQueryAsync();
            await mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            _logger.LogWarning(e.ToString());
            return false;
        }

        return true;
    }

    public async Task<bool> Search(string item, string name, string login)
    {
        string command = $"SELECT EXISTS(SELECT * FROM {item} " +
                         $"WHERE FullName = @FullName " +
                         $"AND Login = @Login)";

        mySqlConnection = new MySqlConnection(_connect);
        await mySqlConnection.OpenAsync();

        mySqlCommand = new MySqlCommand(command, mySqlConnection);
        mySqlCommand.Parameters.Add("@FullName", MySqlDbType.LongText).Value = name;
        mySqlCommand.Parameters.Add("@Login", MySqlDbType.LongText).Value = login;

        object? exist = await mySqlCommand.ExecuteScalarAsync();
        bool convertBool = Convert.ToBoolean(exist);
        await mySqlConnection.CloseAsync();

        return convertBool;
    }
}