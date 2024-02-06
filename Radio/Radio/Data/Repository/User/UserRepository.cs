using System.Data.Common;
using MySql.Data.MySqlClient;

namespace Radio.Data.Repository.User;

public interface IUserRepository
{
    public Task<string> CreateOrSave(string item, Model.RequestModel.User.User user);
    public Task<DbDataReader> GetId(string item, int id);
    public Task<DbDataReader> GetLimit(string item, int limit);
    public Task<DbDataReader> GetName(string item, string name);
    public Task Delete(string item, string name);
    public Task DeleteName(string item, string name);
    public Task Update(string item, string name);
    public Task<bool> Search(string item, string name);
}

public class UserRepository : IUserRepository
{
    private MySqlConnection _mySqlConnection;
    private MySqlCommand _mySqlCommand;
    private DbDataReader _dataReader;

    private string _connect =
        "Server=mysql.students.it-college.ru;Database=i22s0909;Uid=i22s0909;pwd=5x9PVV83;charset=utf8";


    public async Task<string> CreateOrSave(string item, Model.RequestModel.User.User user)
    {
        string command = $"INSERT INTO {item} " +
                         "(name, login, Role) " +
                         "VALUES(@Name, @Login, @Role)";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);

        _mySqlCommand.Parameters.Add("@Name", MySqlDbType.VarChar).Value = user.Name;
        _mySqlCommand.Parameters.Add("@Login", MySqlDbType.VarChar).Value = user.Role;

        try
        {
            await _mySqlCommand.ExecuteNonQueryAsync();
            await _mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            return e.ToString();
        }

        return "Good";
    }

    public async Task<DbDataReader> GetId(string item, int id)
    {
        string command = $"SELECT * FROM {item} " +
                         "WHERE id = @Id";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("Item", MySqlDbType.String).Value = item;

        _dataReader = await _mySqlCommand.ExecuteReaderAsync();

        await _mySqlConnection.CloseAsync();

        return _dataReader;
    }

    public async Task<DbDataReader> GetLimit(string item, int limit)
    {
        string command = $"SELECT * FROM {item} " +
                         "LIMIT = @Limit";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@Limit", MySqlDbType.Int64).Value = limit;


        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();

        return _dataReader;
    }

    public async Task<DbDataReader> GetName(string item, string name)
    {
        string command = $"SELECT * FROM  {item} " +
                         $"WHERE Name = @Name";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = name;


        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();

        return _dataReader;
    }

    public async Task Delete(string item, string name)
    {
        string command = $"DELETE FROM {item}";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);


        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();
    }

    public async Task DeleteName(string item, string name)
    {
        string command = $"DELETE FROM {item} " +
                         $"WHERE name = @Name";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);


        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();
    }

    public async Task Update(string item, string name)
    {
        string command = $"UPDATE {item} " +
                         $"SET {name} = @What";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);


        await _mySqlCommand.ExecuteNonQueryAsync();
        await _mySqlConnection.CloseAsync();
    }

    public async Task<bool> Search(string item, string name)
    {
        string command = $"SELECT EXISTS(SELECT * FROM {item} " +
                         $"WHERE name = @Name)";

        _mySqlConnection = new MySqlConnection(_connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("Name", MySqlDbType.LongText).Value = name;

        object? exist = await _mySqlCommand.ExecuteScalarAsync();
        bool convertBool = Convert.ToBoolean(exist);
        await _mySqlConnection.CloseAsync();

        return convertBool;
    }
}