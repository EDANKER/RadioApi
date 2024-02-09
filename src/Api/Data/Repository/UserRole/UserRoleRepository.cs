namespace Api.Data.Repository.UserRole;

public interface IUserRoleRepository
{
    public Task<bool> CreateOrSave();
    public Task<bool> Update();
    public Task<bool> Delete();
    public Task<bool> GetRoleUser();
}

public class UserRoleRepository : IUserRoleRepository
{
    public Task<bool> CreateOrSave()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete()
    {
        throw new NotImplementedException();
    }

    public Task<bool> GetRoleUser()
    {
        throw new NotImplementedException();
    }
}