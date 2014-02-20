using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}
