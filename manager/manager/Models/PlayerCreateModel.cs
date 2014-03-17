using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel.Entities;

namespace manager.Models
{
    public class PlayerCreateModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Guid CountryId { get; set; }
        public Guid PositionId { get; set; }
        public int Weight { get; set; }
        public int Growth { get; set; }
    }
}