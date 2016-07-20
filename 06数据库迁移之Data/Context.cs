using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06数据库迁移之Data
{
    public class Context:DbContext
    {
        public Context() : base("name=FirstCodeFirstApp")
        {
            
        }

        public virtual DbSet<Donator> Donators { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
    }
}
