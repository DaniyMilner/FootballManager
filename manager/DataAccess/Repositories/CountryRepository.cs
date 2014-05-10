using System.Linq;
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

        public Country GetCountryByPublicId(string publicId)
        {
            return _dataContext.GetSet<Country>().SingleOrDefault(c => c.PublicId == publicId);
        }
    }
}
