﻿using System.Data.Common;
using Api.Interface;
using Api.Model.ResponseModel.Scenario;
using Api.Services.JsonServices;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.Scenario;

public class ScenarioRepository(
    ILogger<ScenarioRepository> logger,
    IConfiguration configuration,
    MySqlConnection mySqlConnection,
    MySqlCommand mySqlCommand,
    IJsonServices<int[]?> jsonServices,
    IJsonServices<string[]> jsonServicesS)
    : IRepository<Model.RequestModel.Scenario.Scenario, DtoScenario, Model.RequestModel.Scenario.Scenario>
{
    private DbDataReader? _dataReader;
    private List<DtoScenario>? _dtoScenarios;
    private DtoScenario? _dtoScenario;
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
    
    public async Task<bool> CreateOrSave(string item, Model.RequestModel.Scenario.Scenario scenario)
    {
        string command = $"INSERT INTO {item} " +
                         "(Name, IdMicroControllers, " +
                         "Time, Days, IdMusic) " +
                         "VALUES(@Name,@IdMicroControllers, " +
                         "@Time, @Days, @IdMusic)";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);

            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = scenario.Name;
            mySqlCommand.Parameters.Add("@Time", MySqlDbType.LongText).Value = scenario.Time;
            mySqlCommand.Parameters.Add("@IdMicroControllers", MySqlDbType.LongText).Value = jsonServices.SerJson(scenario.IdMicroController);
            mySqlCommand.Parameters.Add("@Days", MySqlDbType.LongText).Value = jsonServicesS.SerJson(scenario.Days);
            mySqlCommand.Parameters.Add("@IdMusic", MySqlDbType.Int32).Value = scenario.IdMusic;

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

    public async Task<List<DtoScenario>?> GetAll(string item)
    {
        _dtoScenarios = new List<DtoScenario>();
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
                    string idMicroController = _dataReader.GetString(2);
                    string time = _dataReader.GetString(3);
                    string days = _dataReader.GetString(4);
                    int idMusic = _dataReader.GetInt32(5);

                    int[]? array = jsonServices.DesJson(idMicroController);
                    if (array != null)
                        _dtoScenario = new DtoScenario(id, name, array,
                            time, days, idMusic);
                    if (_dtoScenario != null) _dtoScenarios.Add(_dtoScenario);
                }
            }
            else
            {
                return null;
            }

            await mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _dtoScenarios;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<DtoScenario?> GetId(string item, int id)
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
                    string name = _dataReader.GetString(1);
                    string idMicroController = _dataReader.GetString(2);
                    string time = _dataReader.GetString(3);
                    string days = _dataReader.GetString(4);
                    int idMusic = _dataReader.GetInt32(5);

                    int[]? array = jsonServices.DesJson(idMicroController);
                    if (array != null)
                        _dtoScenario = new DtoScenario(id, name, array,
                            time, days, idMusic);
                    if (_dtoScenarios != null)
                        if (_dtoScenario != null)
                            _dtoScenarios.Add(_dtoScenario);
                }
            }
            else
            {
                return null;
            }

            await mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _dtoScenario;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<List<DtoScenario>?> GetField(string item, string namePurpose, string field)
    {
        _dtoScenarios = new List<DtoScenario>();
        string command = $"SELECT * FROM {item} " +
                         $"WHERE {field} = @NamePurpose";
        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();
            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@NamePurpose", MySqlDbType.LongText).Value = namePurpose;

            _dataReader = await mySqlCommand.ExecuteReaderAsync();

            if (_dataReader.HasRows)
            {
                while (await _dataReader.ReadAsync())
                {
                    int id = _dataReader.GetInt32(0);
                    string name = _dataReader.GetString(1);
                    string idMicroController = _dataReader.GetString(2);
                    string time = _dataReader.GetString(3);
                    string days = _dataReader.GetString(4);
                    int idMusic = _dataReader.GetInt32(5);

                    int[]? array = jsonServices.DesJson(idMicroController);
                    if (array != null)
                        _dtoScenario = new DtoScenario(id, name, array,
                            time, days, idMusic);
                    if (_dtoScenarios != null)
                        if (_dtoScenario != null)
                            _dtoScenarios.Add(_dtoScenario);
                }
            }
            else
            {
                return null;
            }

            await _dataReader.CloseAsync();
            await mySqlConnection.CloseAsync();

            return _dtoScenarios;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<List<DtoScenario>?> GetLike(string item, string namePurpose, string field)
    {
        _dtoScenarios = new List<DtoScenario>();
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
                    string name = _dataReader.GetString(1);
                    string idMicroController = _dataReader.GetString(2);
                    string time = _dataReader.GetString(3);
                    string days = _dataReader.GetString(4);
                    int idMusic = _dataReader.GetInt32(5);

                    int[]? array = jsonServices.DesJson(idMicroController);
                    if (array != null)
                        _dtoScenario = new DtoScenario(id, name, array,
                            time, days, idMusic);
                    if (_dtoScenarios != null)
                        if (_dtoScenario != null)
                            _dtoScenarios.Add(_dtoScenario);
                }
            }

            await _dataReader.CloseAsync();
            await mySqlConnection.CloseAsync();

            return _dtoScenarios;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<List<DtoScenario>?> GetLimit(string item, int currentPage, int limit)
    {
        _dtoScenarios = new List<DtoScenario>();
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
                    string name = _dataReader.GetString(1);
                    string idMicroController = _dataReader.GetString(2);
                    string time = _dataReader.GetString(3);
                    string days = _dataReader.GetString(4);
                    int idMusic = _dataReader.GetInt32(5);

                    int[]? array = jsonServices.DesJson(idMicroController);
                    if (array != null)
                        _dtoScenario = new DtoScenario(id, name, array,
                            time, days, idMusic);
                    if (_dtoScenarios != null)
                        if (_dtoScenario != null)
                            _dtoScenarios.Add(_dtoScenario);
                }
            }
            else
            {
                return null;
            }

            await mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _dtoScenarios;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteId(string item, int id)
    {
        _dtoScenarios = new List<DtoScenario>();
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
            logger.LogError(e.ToString());
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateId(string item, Model.RequestModel.Scenario.Scenario scenario, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET " +
                         $"Name = @Name, " +
                         $"IdMicroControllers = @IdMicroController," +
                         $"Time = @Time," +
                         $"Days = @Days, " +
                         $"IdMusic = @IdMusic " +
                         $"WHERE id = @Id";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);

            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = scenario.Name;
            mySqlCommand.Parameters.Add("@Time", MySqlDbType.LongText).Value = scenario.Time;
            mySqlCommand.Parameters.Add("@IdMicroController", MySqlDbType.LongText).Value = jsonServices.SerJson(scenario.IdMicroController);
            mySqlCommand.Parameters.Add("@Days", MySqlDbType.LongText).Value = jsonServicesS.SerJson(scenario.Days);
            mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;
            mySqlCommand.Parameters.Add("@IdMusic", MySqlDbType.Int32).Value = scenario.IdMusic;

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
        string command = $"SELECT EXISTS(SELECT * FROM {item} " +
                         $"WHERE {field} = @Name)";
        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = name;

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