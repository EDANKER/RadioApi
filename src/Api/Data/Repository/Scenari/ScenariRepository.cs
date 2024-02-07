using System.Data.Common;
using MySql.Data.MySqlClient;
using Radio.Model.ResponseModel.Scenari;

namespace Radio.Data.Repository.Scenari;

public interface IScenariRepository
{
    public Task<bool> CreateOrSave(string item, Api.Model.RequestModel.User.User user);
    public Task<List<GetScenari>> GetId(string item, int id);
    public Task<List<GetScenari>> GetLimit(string item, int limit);
    public Task<List<GetScenari>> GetName(string item, string name);
    public Task<bool> DeleteId(string item, int id);
    public Task<bool> Update(string item, Api.Model.RequestModel.User.User user, int id);
    public Task<bool> Search(string item, string name, string login);
}

public class ScenariRepository : IScenariRepository
{
    private MySqlConnection _mySqlConnection;
    private MySqlCommand _mySqlCommand;
    private DbDataReader _dataReader;
    private List<GetScenari> _getScenaris;
    private GetScenari _getScenari;

    private IConfiguration _configuration;
    private const string Connect = "Server=mysql.students.it-college.ru;Database=i22s0909;Uid=i22s0909;pwd=5x9PVV83;charset=utf8";


    public async Task<bool> CreateOrSave(string item, Api.Model.RequestModel.User.User user)
    {
        string command = $"INSERT INTO {item} " +
                         "(FullName, Login, Role) " +
                         "VALUES(@FullName, @Login, @Role)";

        _mySqlConnection = new MySqlConnection(Connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);

        _mySqlCommand.Parameters.Add("@FullName", MySqlDbType.LongText).Value = user.FullName;
        _mySqlCommand.Parameters.Add("@Login", MySqlDbType.LongText).Value = user.Login;
        _mySqlCommand.Parameters.Add("@Role", MySqlDbType.LongText).Value = user.Role;

        try
        {
            await _mySqlCommand.ExecuteNonQueryAsync();
            await _mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            return false;
        }

        return true;
    }

    public async Task<List<GetScenari>> GetId(string item, int id)
    {
        _getScenaris = new List<GetScenari>();
        string command = $"SELECT * FROM {item} " +
                         "WHERE id = @Id";

        _mySqlConnection = new MySqlConnection(Connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("Id", MySqlDbType.Int32).Value = id;

        _dataReader = await _mySqlCommand.ExecuteReaderAsync();

        if (_dataReader.HasRows)
        {
            while (await _dataReader.ReadAsync())
            {
                string sector = _dataReader.GetString(1);
                string time = _dataReader.GetString(2);

                _getScenari = new GetScenari(id, sector, time);
                _getScenaris.Add(_getScenari);
            }
        }

        await _mySqlConnection.CloseAsync();
        await _dataReader.CloseAsync();

        return _getScenaris;
    }

    public async Task<List<GetScenari>> GetLimit(string item, int limit)
    {
        _getScenaris = new List<GetScenari>();
        string command = $"SELECT * FROM {item} " +
                         "LIMIT @Limit";

        _mySqlConnection = new MySqlConnection(Connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@Limit", MySqlDbType.Int32).Value = limit;


        _dataReader = await _mySqlCommand.ExecuteReaderAsync();

        if (_dataReader.HasRows)
        {
            while (await _dataReader.ReadAsync())
            {
                int id = _dataReader.GetInt32(0);
                string sector = _dataReader.GetString(1);
                string time = _dataReader.GetString(2);

                _getScenari = new GetScenari(id, sector, time);
                _getScenaris.Add(_getScenari);
            }
        }

        await _mySqlConnection.CloseAsync();
        await _dataReader.CloseAsync();

        return _getScenaris;
    }

    public async Task<List<GetScenari>> GetName(string item, string name)
    {
        string command = $"SELECT * FROM  {item} " +
                         $"WHERE Name = @Name";

        _mySqlConnection = new MySqlConnection(Connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = name;

        _dataReader = await _mySqlCommand.ExecuteReaderAsync();
        
        if (_dataReader.HasRows)
        {
            while (await _dataReader.ReadAsync())
            {
                int id = _dataReader.GetInt32(0);
                string sector = _dataReader.GetString(1);
                string time = _dataReader.GetString(2);

                _getScenari = new GetScenari(id, sector, time);
                _getScenaris.Add(_getScenari);
            }
        }

        await _dataReader.CloseAsync();
        await _mySqlConnection.CloseAsync();

        return _getScenaris;
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        _getScenaris = new List<GetScenari>();
        string command = $"DELETE FROM {item} " +
                         $"WHERE id = @Id";

        _mySqlConnection = new MySqlConnection(Connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;

        try
        {
            await _mySqlCommand.ExecuteNonQueryAsync();
            await _mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            Console.WriteLine(e);
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

        _mySqlConnection = new MySqlConnection(Connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        
        _mySqlCommand.Parameters.Add("@FullName", MySqlDbType.LongText).Value = user.FullName;
        _mySqlCommand.Parameters.Add("@Login", MySqlDbType.LongText).Value = user.Login;
        _mySqlCommand.Parameters.Add("@Role", MySqlDbType.LongText).Value = user.Role;
        _mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;

        try
        {
            await _mySqlCommand.ExecuteNonQueryAsync();
            await _mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            Console.WriteLine(e);
            return false;
        }

        return true;
    }

    public async Task<bool> Search(string item, string name, string login)
    {
        string command = $"SELECT EXISTS(SELECT * FROM {item} " +
                         $"WHERE FullName = @FullName " +
                         $"AND Login = @Login)";

        _mySqlConnection = new MySqlConnection(Connect);
        await _mySqlConnection.OpenAsync();

        _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
        _mySqlCommand.Parameters.Add("@FullName", MySqlDbType.LongText).Value = name;
        _mySqlCommand.Parameters.Add("@Login", MySqlDbType.LongText).Value = login;

        object? exist = await _mySqlCommand.ExecuteScalarAsync();
        bool convertBool = Convert.ToBoolean(exist);
        await _mySqlConnection.CloseAsync();

        return convertBool;
    }
}