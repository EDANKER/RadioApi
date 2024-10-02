using System.Data.Common;
using Api.Interface;
using Api.Interface.Repository;
using Api.Model.ResponseModel.MicroController;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.MicroController;

public class MicroControllerFloorRepository(
    ILogger<MicroControllerFloorRepository> logger,
    MySqlCommand mySqlCommand,
    IConfiguration configuration
) : IRepository<Model.RequestModel.MicroController.MicroController, DtoMicroController, Model.RequestModel.MicroController.MicroController
>
{
    private readonly MySqlConnection _mySqlConnection = new(configuration.GetConnectionString("MySql"));
    private DbDataReader? _dataReader;
    private List<DtoMicroController>? _dtoMicroControllers;
    private DtoMicroController? _dtoMicroController;
    
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
    
    public async Task<DtoMicroController?> CreateOrSave(string item,
        Model.RequestModel.MicroController.MicroController microController)
    {
        string command = $"INSERT INTO {item} (Name, Ip, Port, Place) " +
                         "VALUES (@Name, @Ip, @Port, " +
                         "@Place)";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.VarChar).Value = microController.Name;
            mySqlCommand.Parameters.Add("@Ip", MySqlDbType.VarChar).Value = microController.Ip;
            mySqlCommand.Parameters.Add("@Port", MySqlDbType.Int32).Value = microController.Port;
            mySqlCommand.Parameters.Add("@Place", MySqlDbType.VarChar).Value = microController.Place;

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
        return await GetField(item, microController.Name, "Name");
    }

    public async Task<List<DtoMicroController>?> GetAll(string item)
    {
        _dtoMicroControllers = new List<DtoMicroController>();
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
                    string ip = _dataReader.GetString(2);
                    int port = _dataReader.GetInt32(3);
                    string place = _dataReader.GetString(4);

                    _dtoMicroController = new DtoMicroController(id, name, ip,
                        port, place);
                    _dtoMicroControllers.Add(_dtoMicroController);
                }
            }
            return _dtoMicroControllers;
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

    public async Task<List<DtoMicroController>?> GetLimit(string item, int currentPage, int limit)
    {
        _dtoMicroControllers = new List<DtoMicroController>();
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
                    string ip = _dataReader.GetString(2);
                    int port = _dataReader.GetInt32(3);
                    string place = _dataReader.GetString(4);

                    _dtoMicroController = new DtoMicroController(id, name, ip,
                        port, place);
                    _dtoMicroControllers.Add(_dtoMicroController);
                }
            }
            return _dtoMicroControllers;
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

    public async Task<DtoMicroController?> GetId(string item, int id)
    {
        string command = $"SELECT * FROM {item} " +
                         "WHERE Id = @Id";

        try
        {
            await _mySqlConnection.OpenAsync();
            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
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
                    string place = _dataReader.GetString(4);

                    _dtoMicroController = new DtoMicroController(id, name, ip,
                        port, place);
                }
            }
            return _dtoMicroController;
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

    public async Task<DtoMicroController?> GetField(string item, string namePurpose, string field)
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
                    string ip = _dataReader.GetString(2);
                    int port = _dataReader.GetInt32(3);
                    string place = _dataReader.GetString(4);

                    _dtoMicroController = new DtoMicroController(id, name, ip, port, place);
                }
            }
            return _dtoMicroController;
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

    public async Task<List<DtoMicroController>?> GetLike(string item, string namePurpose, string field)
    {
        _dtoMicroControllers = new List<DtoMicroController>();
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
                    string ip = _dataReader.GetString(2);
                    int port = _dataReader.GetInt32(3);
                    string place = _dataReader.GetString(4);

                    _dtoMicroController = new DtoMicroController(id, name, ip, port, place);
                    _dtoMicroControllers.Add(_dtoMicroController);
                }
            }
            return _dtoMicroControllers;
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
                         "WHERE Id = @Id";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
            mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;
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

    public async Task<DtoMicroController?> UpdateId(string item, Model.RequestModel.MicroController.MicroController model, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET Name = @Name," +
                         $"Ip = @Ip, " +
                         $"Port = @Port, " +
                         $"Place = @Place " +
                         $"WHERE Id = @Id";
        
        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = model.Name;
            mySqlCommand.Parameters.Add("@Ip", MySqlDbType.LongText).Value = model.Ip;
            mySqlCommand.Parameters.Add("@Port", MySqlDbType.Int32).Value = model.Port;
            mySqlCommand.Parameters.Add("@Cabinet", MySqlDbType.Int32).Value = model.Ip;
            mySqlCommand.Parameters.Add("@Place", MySqlDbType.VarChar).Value = model.Place;
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
        string command = $"SELECT EXISTS(SELECT * FROM  {item} " +
                         $"WHERE {field} = @Name)";

        try
        {
            await _mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, _mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = name;

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