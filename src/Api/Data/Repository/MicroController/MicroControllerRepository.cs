using System.Data.Common;
using Api.Model.ResponseModel.MicroController;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.MicroController;

public interface IMicroControllerRepository
{
    Task<bool> CreateOrSave(string item, Model.RequestModel.MicroController.MicroController microController);
    Task<List<DtoMicroController>> GetLimit(string item, int limit);
    Task<DtoMicroController> GetId(string item, int cabinet, int flor);
    Task<DtoMicroController> GetName(string item, string name);
    Task<bool> DeleteId(string item, int id);
    Task<bool> Update(string item, Model.RequestModel.MicroController.MicroController microController);
    Task<bool> Search(string item, string name);
}

public class MicroControllerRepository(
    ILogger<MicroControllerRepository> logger,
    MySqlConnection mySqlConnection,
    MySqlCommand mySqlCommand,
    IConfiguration configuration
) : IMicroControllerRepository
{
    private readonly string _connect = configuration.GetConnectionString("MySql");
    private DbDataReader _dataReader;
    private List<DtoMicroController> _microControllers;
    private DtoMicroController _microController;

    public async Task<bool> CreateOrSave(string item,
        Model.RequestModel.MicroController.MicroController microController)
    {
        const string command = "INSERT INTO MicroControllers(Name, Ip, Port, " +
                               "Cabinet, Floor) " +
                               "VALUES (@Name, @Ip, @Port, " +
                               "@Cabinet, @Floor)";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = microController.Name;
            mySqlCommand.Parameters.Add("@Ip", MySqlDbType.LongText).Value = microController.Ip;
            mySqlCommand.Parameters.Add("@Port", MySqlDbType.Int32).Value = microController.Port;
            mySqlCommand.Parameters.Add("@Cabinet", MySqlDbType.Int32).Value = microController.Cabinet;
            mySqlCommand.Parameters.Add("@Floor", MySqlDbType.Int32).Value = microController.Floor;

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

    public async Task<List<DtoMicroController>> GetLimit(string item, int limit)
    {
        _microControllers = new List<DtoMicroController>();
        string command = $"SELECT * FROM {item} " +
                         " LIMIT @Limit";

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
                    string ip = _dataReader.GetString(2);
                    int port = _dataReader.GetInt32(3);
                    int cabinet = _dataReader.GetInt32(4);
                    int floor = _dataReader.GetInt32(5);

                    _microController = new DtoMicroController(id, name, ip,
                        port, floor, cabinet);
                    _microControllers.Add(_microController);
                }
            }

            await _dataReader.CloseAsync();
            await mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
        }

        return _microControllers;
    }

    public async Task<DtoMicroController> GetId(string item, int cabinet, int floor)
    {
        const string command = "SELECT * FROM MicroControlles " +
                               "WHERE flor AND cabinet";

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
                    string ip = _dataReader.GetString(2);
                    int port = _dataReader.GetInt32(0);
                    cabinet = _dataReader.GetInt32(0);
                    floor = _dataReader.GetInt32(0);

                    _microController = new DtoMicroController(id, name, ip,
                        port, floor, cabinet);
                }
            }

            await _dataReader.CloseAsync();
            await mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
        }

        return _microController;
    }

    public Task<DtoMicroController> GetName(string item, string name)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        string command = $"DELETE FROM {item} " +
                         "WHERE Id = @Id";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;

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

    public async Task<bool> Update(string item, Model.RequestModel.MicroController.MicroController microController)
    {
        try
        {
            return true;
        }
        catch (MySqlException e)
        {
            return false;
        }
    }

    public async Task<bool> Search(string item, string name)
    {
        string command = $"SELECT EXISTS(SELECT * FROM  {item} " +
                         "WHERE Name = @Name)";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = name;

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