using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class StringHelper
    {
        public static string CheckSymbols(string id)
        {
            return id.Replace("=", "").Replace("/", "");
        }
    }
}
