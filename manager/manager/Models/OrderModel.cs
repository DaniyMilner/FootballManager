using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace manager.Models
{
    public class OrderModel
    {
        public Guid id { get; set; }
        public double Price { get; set; }
    }
}