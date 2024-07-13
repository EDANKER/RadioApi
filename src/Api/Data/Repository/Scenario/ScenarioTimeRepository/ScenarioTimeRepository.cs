﻿using System.Data.Common;
using Api.Interface.Repository;
using Api.Model.RequestModel.Scenario.TimeScenario;
using Api.Model.ResponseModel.PlayScenario;
using Api.Model.ResponseModel.TimeScenario;
using Api.Services.JsonServices;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.Scenario.ScenarioTimeRepository;

public class ScenarioTimeRepository(
    ILogger<ScenarioTimeRepository> logger,
    IConfiguration configuration,
    MySqlConnection mySqlConnection,
    MySqlCommand mySqlCommand,
    IJsonServices<int[]?> jsonServices,
    IJsonServices<string[]> jsonServicesS)
    : IRepository<TimeScenario, DtoTimeScenario, TimeScenario>
{
    private DbDataReader? _dataReader;
    private List<DtoTimeScenario>? _dtoScenarios;
    private DtoTimeScenario? _dtoScenario;
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
    
    public async Task<DtoTimeScenario?> CreateOrSave(string item, TimeScenario scenario)
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
            return null;
        }

        return await GetField(item, scenario.Name, "Name");
    }

    public async Task<List<DtoTimeScenario>?> GetAll(string item)
    {
        _dtoScenarios = new List<DtoTimeScenario>();
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
                        _dtoScenario = new DtoTimeScenario(id, name, array,
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

    public async Task<DtoTimeScenario?> GetId(string item, int id)
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
                        _dtoScenario = new DtoTimeScenario(id, name, array,
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

    public async Task<DtoTimeScenario?> GetField(string item, string namePurpose, string field)
    {
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
                        _dtoScenario = new DtoTimeScenario(id, name, array,
                            time, days, idMusic);
                }
            }
            else
            {
                return null;
            }

            await _dataReader.CloseAsync();
            await mySqlConnection.CloseAsync();

            return _dtoScenario;
        }
        catch (MySqlException e)
        {
            logger.LogError(e.ToString());
            return null;
        }
    }

    public async Task<List<DtoTimeScenario>?> GetLike(string item, string namePurpose, string field)
    {
        _dtoScenarios = new List<DtoTimeScenario>();
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
                        _dtoScenario = new DtoTimeScenario(id, name, array,
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

    public async Task<List<DtoTimeScenario>?> GetLimit(string item, int currentPage, int limit)
    {
        _dtoScenarios = new List<DtoTimeScenario>();
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
                        _dtoScenario = new DtoTimeScenario(id, name, array,
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
        _dtoScenarios = new List<DtoTimeScenario>();
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

    public async Task<DtoTimeScenario?> UpdateId(string item, TimeScenario scenario, int id)
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
            return null;
        }

        return await GetField(item, scenario.Name, "Name");
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