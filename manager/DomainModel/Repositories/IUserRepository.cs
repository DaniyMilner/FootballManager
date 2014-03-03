using DomainModel.Entities;

namespace DomainModel.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserByEmail(string email);
        User GetUserByUserName(string userName);
    }
}
