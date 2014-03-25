using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;

namespace DomainModel.Entities
{
    public class Position : Entity
    {
        protected internal Position() { }

        protected internal Position(string publicId, string name)
        {
            PublicId = publicId;
            Name = name;
            PlayerCollection = new Collection<Player>();
        }

        public string PublicId { get; private set; }
        public string Name { get; private set; }
        [ScriptIgnore]
        public virtual ICollection<Player> PlayerCollection { get; private set; }
    }
}
