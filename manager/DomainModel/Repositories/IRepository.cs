using DomainModel.Entities;

namespace DomainModel.Repositories
{
    public interface IRepository<T> : IQuerableRepository<T> where T : Entity
    {
        void Add(T entity);
        void Remove(T entity);
    }
}
