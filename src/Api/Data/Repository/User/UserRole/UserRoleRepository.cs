using MongoDB.Bson;
using MongoDB.Driver;

namespace Api.Data.Repository.UserRole;

public interface IUserRoleRepository
{
    public Task<bool> CreateOrSave(string name);
    public Task<bool> Update();
    public Task<bool> Delete();
    public Task<bool> GetRoleUser();
}

public class UserRoleRepository(IConfiguration configuration) : IUserRoleRepository
{
    private readonly string _connect = configuration.GetConnectionString("MongoDb");
    private MongoClient _mongoClient;

    public async Task<bool> CreateOrSave(string name)
    {
        IMongoDatabase? db = _mongoClient.GetDatabase("Radio");
        IMongoCollection<BsonDocument>? userRole = db.GetCollection<BsonDocument>("UserRole");

        BsonDocument bsonDocument = new BsonDocument
        {
            { "Name", $"{name}" },
            { "Role", new BsonArray { "Admin" } }
        };

        await userRole.InsertOneAsync(bsonDocument);

        return true;
    }

    public async Task<bool> Update()
    {
        return true;
    }

    public async Task<bool> Delete()
    {
        return true;
    }

    public async Task<bool> GetRoleUser()
    {
        return true;
    }
}