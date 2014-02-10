using System.Data.Entity;
using DomainModel.Entities;

namespace DataAccess
{
    public interface IDataContext
    {
        IDbSet<T> GetSet<T>() where T : Entity;
    }
}
