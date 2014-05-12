using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Entities;
using DomainModel.Repositories;

namespace DataAccess.Repositories
{
    public class EquipmentRepository : Repository<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(IDataContext dataContext) : base(dataContext)
        {
        }

        public ICollection<Equipment> GetEquipmentsByType(EquipmentType type)
        {
            return _dataContext.GetSet<Equipment>().Where(e => e.Type == type).ToList();
        }
    }
}
