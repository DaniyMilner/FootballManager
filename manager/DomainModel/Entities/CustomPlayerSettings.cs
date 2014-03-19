using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Entities
{
    public class CustomPlayerSettings : Entity
    {
        public string Orient { get; set; }
        public string Pas { get; set; }
        public string Strike { get; set; }
        public string Oneone { get; set; }
        public string Canopy { get; set; }
        public string Selection { get; set; }
        public string Dedication { get; set; }
        public string Penalty { get; set; }
        public bool IsCaptain { get; set; }
        public Guid? TeamId { get; set; }
    }
}
