using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _041LINQ之CRUD
{
    public class DonatorsContext:DbContext
    {
        public DonatorsContext() : base("name=FirstCodeFirstApp")
        {
            Database.SetInitializer<DonatorsContext>(new DropCreateDatabaseIfModelChanges<DonatorsContext>());
        }

        public virtual DbSet<Donator> Donators { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
    }
}
