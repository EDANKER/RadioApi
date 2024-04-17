using System.Data.Common;
using Api.Model.ResponseModel.Scenario;
using Api.Services.JsonServices;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace Api.Data.Repository.Scenario
{
    public interface IScenarioRepository
    {
        Task<bool> CreateOrSave(string item, Model.RequestModel.Scenario.Scenario scenario);
        Task<List<DtoScenario>> GetId(string item, int id);
        Task<List<DtoScenario>> GetLimit(string item, int limit);
        Task<List<DtoScenario>> GetHour(string item, string timeScenario);
        Task<bool> DeleteId(string item, int id);
        Task<bool> Update(string item, Model.RequestModel.Scenario.Scenario scenario, int id);
        Task<bool> Search(string item, string name);
    }

    public class ScenarioRepository(
        ILogger<ScenarioRepository> logger,
        IConfiguration configuration,
        MySqlConnection mySqlConnection,
        MySqlCommand mySqlCommand) : IScenarioRepository
    {
        private IJsonServices<int[]> _jsonServicesIdMicroController = new JsonServices<int[]>();
        private IJsonServices<string[]> _jsonServicesDays = new JsonServices<string[]>();
        private DbDataReader _dataReader;
        private List<DtoScenario> _getScenaris;
        private DtoScenario _dtoScenario;
        private readonly string _connect = configuration.GetConnectionString("MySql") ?? string.Empty;

        public async Task<bool> CreateOrSave(string item, Model.RequestModel.Scenario.Scenario scenario)
        {
            string command = $"INSERT INTO {item} " +
                             "(Name, IdMicroController, " +
                             "Time, Days, IdMusic) " +
                             "VALUES(@Name,@IdMicroController, " +
                             "@Time, @Days, @IdMusic)";

            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);

            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = scenario.Name;
            mySqlCommand.Parameters.Add("@Time", MySqlDbType.LongText).Value = scenario.Time;
            mySqlCommand.Parameters.Add("@IdMicroController", MySqlDbType.LongText).Value = 
                _jsonServicesIdMicroController.SerJson(scenario.IdMicroController);
            mySqlCommand.Parameters.Add("@Days", MySqlDbType.LongText).Value = _jsonServicesDays.SerJson(scenario.Days);
            mySqlCommand.Parameters.Add("@IdMusic", MySqlDbType.Int32).Value = scenario.IdMusic;

            try
            {
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

        public async Task<List<DtoScenario>> GetId(string item, int id)
        {
            _getScenaris = new List<DtoScenario>();
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
                        string sector = _dataReader.GetString(2);
                        string time = _dataReader.GetString(3);
                        string days = _dataReader.GetString(4);
                        int idMusic = _dataReader.GetInt32(5);

                        _dtoScenario = new DtoScenario(id, name,_jsonServicesIdMicroController.DesJson(sector), 
                            time, days, idMusic);
                        _getScenaris.Add(_dtoScenario);
                    }
                }

                await mySqlConnection.CloseAsync();
                await _dataReader.CloseAsync();
            }
            catch (MySqlException e)
            {
                logger.LogError(e.ToString());
            }

            return _getScenaris;
        }

        public async Task<List<DtoScenario>> GetLimit(string item, int limit)
        {
            _getScenaris = new List<DtoScenario>();
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
                        string name = _dataReader.GetString(1);
                        string sector = _dataReader.GetString(2);
                        string time = _dataReader.GetString(3);
                        string days = _dataReader.GetString(4);
                        int idMusic = _dataReader.GetInt32(5);

                        _dtoScenario = new DtoScenario(id, name,_jsonServicesIdMicroController.DesJson(sector), 
                            time, days, idMusic);
                        _getScenaris.Add(_dtoScenario);
                    }
                }

                await mySqlConnection.CloseAsync();
                await _dataReader.CloseAsync();

            }
            catch (MySqlException e)
            {
                logger.LogError(e.ToString());
            }
            
            return _getScenaris;
        }

        public async Task<List<DtoScenario>> GetHour(string item, string timeScenario)
        {
            _getScenaris = new List<DtoScenario>();
            string command = $"SELECT * FROM {item} " +
                             $"WHERE Time LIKE '%{timeScenario}%'";
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
                        string sector = _dataReader.GetString(2);
                        string time = _dataReader.GetString(3);
                        string days = _dataReader.GetString(4);
                        int idMusic = _dataReader.GetInt32(5);

                        _dtoScenario = new DtoScenario(id, name,_jsonServicesIdMicroController.DesJson(sector), 
                            time, days, idMusic);
                        _getScenaris.Add(_dtoScenario);
                    }
                }

                await mySqlConnection.CloseAsync();
                await _dataReader.CloseAsync();

                return _getScenaris;
            }
            catch (MySqlException e)
            {
                logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<bool> DeleteId(string item, int id)
        {
            _getScenaris = new List<DtoScenario>();
            string command = $"DELETE FROM {item} " +
                             $"WHERE id = @Id";

            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;

            try
            {
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

        public async Task<bool> Update(string item, Model.RequestModel.Scenario.Scenario scenario, int id)
        {
            string command = $"UPDATE {item} " +
                             $"SET " +
                             $"Name = @Name, IdMicroController = " +
                             $"@IdMicroController, Time = @Time, Days = @Days, IdMusic = @IdMusic " +
                             $"WHERE id = @Id";

            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);

            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = scenario.Name;
            mySqlCommand.Parameters.Add("@Time", MySqlDbType.LongText).Value = scenario.Time;
            mySqlCommand.Parameters.Add("@IdMicroController", MySqlDbType.LongText).Value = 
                _jsonServicesIdMicroController.SerJson(scenario.IdMicroController);
            mySqlCommand.Parameters.Add("@Days", MySqlDbType.LongText).Value = _jsonServicesDays.SerJson(scenario.Days);
            mySqlCommand.Parameters.Add("@Id", MySqlDbType.Int32).Value = id;
            mySqlCommand.Parameters.Add("@IdMusic", MySqlDbType.Int32).Value = scenario.IdMusic;

            try
            {
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

        public async Task<bool> Search(string item, string name)
        {
            string command = $"SELECT EXISTS(SELECT * FROM {item} " +
                             $"WHERE Name = @Name)";

            mySqlConnection = new MySqlConnection(_connect);
            await mySqlConnection.OpenAsync();

            mySqlCommand = new MySqlCommand(command, mySqlConnection);
            mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = name;

            object? exist = await mySqlCommand.ExecuteScalarAsync();
            bool convertBool = Convert.ToBoolean(exist);
            await mySqlConnection.CloseAsync();

            return convertBool;
        }
    }
}