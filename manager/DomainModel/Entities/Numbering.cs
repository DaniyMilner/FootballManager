using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class Numbering : Entity
    {
        public Numbering() {}

        public Numbering(NumberingType type, int maxLength, int number)
        {
            Type = type;
            Number = number;
        }

        public NumberingType Type { get; private set; }


        public int Number { get; set; }

        public void UpdateNumber(int number)
        {
            Number = number;
        }
    }
}
