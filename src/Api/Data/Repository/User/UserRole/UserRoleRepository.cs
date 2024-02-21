using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.Data.Repository.User.UserRole;

public interface IUserRoleRepository
{
    public Task<bool> CreateOrSave(string name, List<string> role);
    public Task<bool> Update(string name, List<string> role);
    public Task<bool> DeleteId(string name);
    public Task<bool> GetRoleUser(string name);
}

public class UserRoleRepository(IConfiguration configuration) : IUserRoleRepository
{
    private readonly string _connect = configuration.GetConnectionString("MongoDb");
    private MongoClient mongoClient;
    public async Task<bool> CreateOrSave(string name, List<string> role)
    {
        mongoClient = new MongoClient(_connect);
        IMongoDatabase? db = mongoClient.GetDatabase("Radio");
        IMongoCollection<BsonDocument>? userRole = db.GetCollection<BsonDocument>("UserRole");

        BsonDocument bsonDocument = new BsonDocument
        {
            { "Name", $"{name}" },
            { "Role", new BsonArray { $"{role}" } }
        };

        await userRole.InsertOneAsync(bsonDocument);

        return true;
    }

    public async Task<bool> Update(string name, List<string> role)
    {
        return true;
    }

    public async Task<bool> DeleteId(string name)
    {
        return true;
    }

    public async Task<bool> GetRoleUser(string name)
    {
        return true;
    }
}