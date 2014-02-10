using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Repositories
{
    public class QuerableRepository<T> :IQuerableRepository<T> where T:Entity
    {
        protected readonly IDataContext _dataContext;

        public QuerableRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public T Get(Guid id)
        {
            return _dataContext.GetSet<T>().SingleOrDefault(e => e.Id == id);
        }

        public ICollection<T> GetCollection()
        {
            return _dataContext.GetSet<T>().ToList();
        }

        public ICollection<T> GetCollection(Func<T, bool> predicate)
        {
            return _dataContext.GetSet<T>().Where(predicate).ToList();
        }
    }
}
