using System;
using System.Collections.Generic;
using DomainModel.Entities;

namespace DomainModel.Repositories
{
    public interface IQuerableRepository<T> where T:Entity
    {
        T Get(Guid id);

        ICollection<T> GetCollection();
        ICollection<T> GetCollection(Func<T, bool> predicate);
    }
}
