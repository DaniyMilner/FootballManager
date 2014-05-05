using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public User GetUserByEmail(string email)
        {
            return _dataContext.GetSet<User>().SingleOrDefault(u => u.Email == email);
        }

        public User GetUserByUserName(string userName)
        {
            return _dataContext.GetSet<User>().SingleOrDefault(u => u.UserName == userName);
        }

        public User GetUserByPublicId(string publicId)
        {
            return _dataContext.GetSet<User>().SingleOrDefault(u => u.PublicId == publicId);
        }
    }
}
