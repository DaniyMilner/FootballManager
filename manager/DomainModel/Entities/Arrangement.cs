using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class Arrangement : Entity
    {
        protected internal Arrangement() { }
        protected internal Arrangement(string scheme, ArrangementType type) 
        {
            Scheme = scheme;
            Type = type;
        }

        public string Scheme { get; private set; }
        public ArrangementType Type { get; private set; }
    }

    public enum ArrangementType
    { 
        Scheme442,
        Scheme433,
        Scheme451,
        Scheme343,
        Scheme352,
        Scheme532,
        Scheme541
    }
}
