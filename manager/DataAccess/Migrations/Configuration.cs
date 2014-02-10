using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Migrations
{
    using System.Data.Entity.Migrations;

    namespace easygenerator.DataAccess.Migrations
    {
        public class Configuration : DbMigrationsConfiguration<DataContext>
        {
            public Configuration()
            {
                AutomaticMigrationsEnabled = false;
            }

            protected override void Seed(DataContext context)
            {
            }
        }
    }

}
