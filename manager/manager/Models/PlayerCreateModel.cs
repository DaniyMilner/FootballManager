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
        public string CountryId { get; set; }
        public string PositionId { get; set; }
        public string Weight { get; set; }
        public string Growth { get; set; }
    }
}