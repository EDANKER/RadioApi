﻿using System.Data.Common;
using Api.Interface.Repository;
using Api.Model.RequestModel.Scenario.PlayScenario;
using Api.Model.ResponseModel.PlayScenario;
using Api.Services.JsonServices;
using MySql.Data.MySqlClient;

namespace Api.Data.Repository.Scenario.ScenarioPlayRepository;

public class ScenarioPlayRepository(
    ILogger<ScenarioTimeRepository.ScenarioTimeRepository> logger,
    IConfiguration configuration,
    MySqlConnection mySqlConnection,
    MySqlCommand mySqlCommand,
    IJsonServices<int[]?> jsonServices,
    IJsonServices<string[]> jsonServicesS)
    : IRepository<PlayScenario, DtoPlayScenario, PlayScenario>
{
    private DbDataReader? _dataReader;
    private List<DtoPlayScenario>? _dtoScenarios;
    private DtoPlayScenario? _dtoScenario;
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
    
    public async Task<DtoPlayScenario?> CreateOrSave(string item, PlayScenario scenario)
    {
        string command = $"INSERT INTO {item} " +
                         "(Name, IdMicroControllers, " +
                         "IdMusic) " +
                         "VALUES(@Name,@IdMicroControllers, " +
                         "@IdMusic)";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);

            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = scenario.Name;
            mySqlCommand.Parameters.Add("@IdMicroControllers", MySqlDbType.LongText).Value = jsonServices.SerJson(scenario.IdMicroController);
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

    public async Task<List<DtoPlayScenario>?> GetAll(string item)
    {
        _dtoScenarios = new List<DtoPlayScenario>();
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
                    int idMusic = _dataReader.GetInt32(3);

                    int[]? array = jsonServices.DesJson(idMicroController);
                    if (array != null)
                        _dtoScenario = new DtoPlayScenario(id, name, array,
                            idMusic);
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

    public async Task<DtoPlayScenario?> GetId(string item, int id)
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
                    int idMusic = _dataReader.GetInt32(3);

                    int[]? array = jsonServices.DesJson(idMicroController);
                    if (array != null)
                        _dtoScenario = new DtoPlayScenario(id, name, array,
                            idMusic);
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

    public async Task<DtoPlayScenario?> GetField(string item, string namePurpose, string field)
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
                    int idMusic = _dataReader.GetInt32(3);

                    int[]? array = jsonServices.DesJson(idMicroController);
                    if (array != null)
                        _dtoScenario = new DtoPlayScenario(id, name, array,
                            idMusic);
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

    public async Task<List<DtoPlayScenario>?> GetLike(string item, string namePurpose, string field)
    {
        _dtoScenarios = new List<DtoPlayScenario>();
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
                    int idMusic = _dataReader.GetInt32(3);

                    int[]? array = jsonServices.DesJson(idMicroController);
                    if (array != null)
                        _dtoScenario = new DtoPlayScenario(id, name, array,
                            idMusic);
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

    public async Task<List<DtoPlayScenario>?> GetLimit(string item, int currentPage, int limit)
    {
        _dtoScenarios = new List<DtoPlayScenario>();
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
                    int idMusic = _dataReader.GetInt32(3);

                    int[]? array = jsonServices.DesJson(idMicroController);
                    if (array != null)
                        _dtoScenario = new DtoPlayScenario(id, name, array,
                            idMusic);
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
        _dtoScenarios = new List<DtoPlayScenario>();
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

    public async Task<DtoPlayScenario?> UpdateId(string item, PlayScenario scenario, int id)
    {
        string command = $"UPDATE {item} " +
                         $"SET " +
                         $"Name = @Name, " +
                         $"IdMicroControllers = @IdMicroController," +
                         $"IdMusic = @IdMusic " +
                         $"WHERE id = @Id";

        try
        {
            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);

            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = scenario.Name;
            mySqlCommand.Parameters.Add("@IdMicroController", MySqlDbType.LongText).Value = jsonServices.SerJson(scenario.IdMicroController);
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