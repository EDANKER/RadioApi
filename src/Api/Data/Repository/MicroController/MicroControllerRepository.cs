using System.Data.Common;
using Api.Interface;
using Api.Model.ResponseModel.MicroController;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.MicroController;

public class MicroControllerRepository(
    ILogger<MicroControllerRepository> logger,
    MySqlConnection mySqlConnection,
    MySqlCommand mySqlCommand,
    IConfiguration configuration
) : IRepository<Model.RequestModel.MicroController.MicroController, DtoMicroController, Model.RequestModel.MicroController.MicroController>
{
    private readonly string _connect = configuration.GetConnectionString("MySql") ?? string.Empty;
    private DbDataReader? _dataReader;
    private List<DtoMicroController>? _dtoMicroControllers;
    private DtoMicroController? _dtoMicroController;

    public async Task<bool> CreateOrSave(string item,
        Model.RequestModel.MicroController.MicroController microController)
    {
        string command = $"INSERT INTO {item} (Name, Ip, Port, Cabinet, Floor) " +
                         "VALUES (@Name, @Ip, @Port, " +
                         "@Cabinet, @Floor)";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.VarChar).Value = microController.Name;
            mySqlCommand.Parameters.Add("@Ip", MySqlDbType.VarChar).Value = microController.Ip;
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

    public async Task<List<DtoMicroController>?> GetAll(string item)
    {
        _dtoMicroControllers = new List<DtoMicroController>();
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
                    string ip = _dataReader.GetString(2);
                    int port = _dataReader.GetInt32(3);
                    int cabinet = _dataReader.GetInt32(4);
                    int floor = _dataReader.GetInt32(5);

                    _dtoMicroController = new DtoMicroController(id, name, ip, port, cabinet, floor);
                    _dtoMicroControllers.Add(_dtoMicroController);
                }
            }
            else
            {
                return null;
            }

            await mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _dtoMicroControllers;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<List<DtoMicroController>?> GetFloor(string item, int floor)
    {
        _dtoMicroControllers = new List<DtoMicroController>();
        string command = $"SELECT * FROM {item} " +
                         " WHERE Floor = @Floor";
        
        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();
            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Floor", MySqlDbType.Int32).Value = floor;
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
                    floor = _dataReader.GetInt32(5);

                    _dtoMicroController = new DtoMicroController(id, name, ip,
                        port, floor, cabinet);
                    _dtoMicroControllers.Add(_dtoMicroController);
                }
            }
            else
            {
                return null;
            }

            await _dataReader.CloseAsync();
            await mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
        }

        return _dtoMicroControllers;
    }

    public async Task<DtoMicroController?> GetId(string item, int id)
    {
        string command = $"SELECT * FROM {item} " +
                         "WHERE Id = @Id";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();
            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;
            _dataReader = await mySqlCommand.ExecuteReaderAsync();
            if (_dataReader.HasRows)
            {
                while (await _dataReader.ReadAsync())
                {
                    id = _dataReader.GetInt32(0);
                    string name = _dataReader.GetString(1);
                    string ip = _dataReader.GetString(2);
                    int port = _dataReader.GetInt32(3);
                    int cabinet = _dataReader.GetInt32(4);
                    int floor = _dataReader.GetInt32(5);

                    _dtoMicroController = new DtoMicroController(id, name, ip,
                        port, floor, cabinet);
                }
            }
            else
            {
                return null;
            }

            await _dataReader.CloseAsync();
            await mySqlConnection.CloseAsync();
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
        }

        return _dtoMicroController;
    }

    public async Task<List<DtoMicroController>?> GetString(string item, string namePurpose, string field)
    {
        _dtoMicroControllers = new List<DtoMicroController>();
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
                    string ip = _dataReader.GetString(2);
                    int port = _dataReader.GetInt32(3);
                    int cabinet = _dataReader.GetInt32(4);
                    int floor = _dataReader.GetInt32(5);

                    _dtoMicroController = new DtoMicroController(id, name, ip, port, cabinet, floor);
                    _dtoMicroControllers.Add(_dtoMicroController);
                }
            }
            else
            {
                return null;
            }

            await mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _dtoMicroControllers;
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

    public async Task<bool> UpdateId(string item, Model.RequestModel.MicroController.MicroController model, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET Name = @Name," +
                         $"Ip = @Ip, " +
                         $"Port = @Port, " +
                         $"Cabinet = @Cabinet, " +
                         $"Floor = @Floor " +
                         $"WHERE Id = @Id";
        
        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = model.Name;
            mySqlCommand.Parameters.Add("@Ip", MySqlDbType.LongText).Value = model.Ip;
            mySqlCommand.Parameters.Add("@Port", MySqlDbType.Int32).Value = model.Port;
            mySqlCommand.Parameters.Add("@Cabinet", MySqlDbType.Int32).Value = model.Ip;
            mySqlCommand.Parameters.Add("@Floor", MySqlDbType.Int32).Value = model.Floor;
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
        string command = $"SELECT EXISTS(SELECT * FROM  {item} " +
                         $"WHERE {field} = @Name)";

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