using System.Data.Common;
using Api.Model.RequestModel.Scenario;
using MySql.Data.MySqlClient;
using Radio.Model.ResponseModel.Scenari;

namespace Api.Data.Repository.Scenari
{
    public interface IScenarioRepository
    {
        public Task<bool> CreateOrSave(string item, Scenario scenario);
        public Task<List<GetScenario>> GetId(string item, int id);
        public Task<List<GetScenario>> GetLimit(string item, int limit);
        public Task<bool> DeleteId(string item, int id);
        public Task<bool> Update(string item, Scenario scenario, int id);
        public Task<bool> Search(string item, string name);
    }

    public class ScenarioRepository(IConfiguration configuration) : IScenarioRepository
    {
        private MySqlConnection _mySqlConnection;
        private MySqlCommand _mySqlCommand;
        private DbDataReader _dataReader;
        private List<GetScenario> _getScenaris;
        private GetScenario _getScenario;

        private readonly string _connect = configuration.GetConnectionString("MySql");


        public async Task<bool> CreateOrSave(string item, Scenario scenario)
        {
            string command = $"INSERT INTO {item} " +
                             "(Name, Sector, Time) " +
                             "VALUES(@Name, @Time, @Sector)";

            _mySqlConnection = new MySqlConnection(_connect);
            await _mySqlConnection.OpenAsync();

            _mySqlCommand = new MySqlCommand(command, _mySqlConnection);

            _mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = scenario.Name;
            _mySqlCommand.Parameters.Add("@Time", MySqlDbType.DateTime).Value = scenario.Time;
            _mySqlCommand.Parameters.Add("@Sector", MySqlDbType.LongText).Value = scenario.Sector;


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

        public async Task<List<GetScenario>> GetId(string item, int id)
        {
            _getScenaris = new List<GetScenario>();
            string command = $"SELECT * FROM {item} " +
                             "WHERE id = @Id";

            _mySqlConnection = new MySqlConnection(_connect);
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

                    _getScenario = new GetScenario(id, sector, time);
                    _getScenaris.Add(_getScenario);
                }
            }

            await _mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _getScenaris;
        }

        public async Task<List<GetScenario>> GetLimit(string item, int limit)
        {
            _getScenaris = new List<GetScenario>();
            string command = $"SELECT * FROM {item} " +
                             "LIMIT @Limit";

            _mySqlConnection = new MySqlConnection(_connect);
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

                    _getScenario = new GetScenario(id, sector, time);
                    _getScenaris.Add(_getScenario);
                }
            }

            await _mySqlConnection.CloseAsync();
            await _dataReader.CloseAsync();

            return _getScenaris;
        }

        public async Task<bool> DeleteId(string item, int id)
        {
            _getScenaris = new List<GetScenario>();
            string command = $"DELETE FROM {item} " +
                             $"WHERE id = @Id";

            _mySqlConnection = new MySqlConnection(_connect);
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

        public async Task<bool> Update(string item, Scenario scenario, int id)
        {
            string command = $"UPDATE {item} " +
                             $"SET " +
                             $"Name = @Name, Sector = " +
                             $"@Sector, Time = @Time " +
                             $"WHERE id = @Id";

            _mySqlConnection = new MySqlConnection(_connect);
            await _mySqlConnection.OpenAsync();

            _mySqlCommand = new MySqlCommand(command, _mySqlConnection);

            _mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = scenario.Name;
            _mySqlCommand.Parameters.Add("@Sector", MySqlDbType.LongText).Value = scenario.Time;
            _mySqlCommand.Parameters.Add("@Time", MySqlDbType.LongText).Value = scenario.Time;
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

        public async Task<bool> Search(string item, string name)
        {
            string command = $"SELECT EXISTS(SELECT * FROM {item} " +
                             $"WHERE Name = @Name)";

            _mySqlConnection = new MySqlConnection(_connect);
            await _mySqlConnection.OpenAsync();

            _mySqlCommand = new MySqlCommand(command, _mySqlConnection);
            _mySqlCommand.Parameters.Add("@Name", MySqlDbType.LongText).Value = name;

            object? exist = await _mySqlCommand.ExecuteScalarAsync();
            bool convertBool = Convert.ToBoolean(exist);
            await _mySqlConnection.CloseAsync();

            return convertBool;
        }
    }
}