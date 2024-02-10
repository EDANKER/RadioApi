using MongoDB.Driver;

namespace Api.Data.Repository.UserRole;

public interface IUserRoleRepository
{
    public Task<bool> CreateOrSave();
    public Task<bool> Update();
    public Task<bool> Delete();
    public Task<bool> GetRoleUser();
}

public class UserRoleRepository(IConfiguration configuration) : IUserRoleRepository
{
    private string _connect = configuration.GetConnectionString("MongoDb");
    private MongoClient _mongoClient;
    
    public async Task<bool> CreateOrSave()
    {
        _mongoClient = new MongoClient(_connect);
        var data =_mongoClient.ListDatabaseNamesAsync();

        Console.WriteLine(data.Result);

        return true;
    }

    public async Task<bool> Update()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Delete()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> GetRoleUser()
    {
        throw new NotImplementedException();
    }
}