using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DomainModel.Entities
{
    public class Country : Entity
    {
        protected internal Country() { }

        protected internal Country(string publicId, string name)
        {
            PublicId = publicId;
            Name = name;

            TeamCollection = new Collection<Team>();
            PlayerCollection = new Collection<Player>();
        }

        public string PublicId { get; private set; }
        public string Name { get; private set; }
        public virtual ICollection<Team> TeamCollection { get; private set; }
        public virtual ICollection<Player> PlayerCollection { get; private set; }
    }
}
