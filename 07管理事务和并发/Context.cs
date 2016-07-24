using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07管理事务和并发
{
    public class Context:DbContext
    {
        public Context() : base("name=FirstCodeFirstApp")
        {
            
        }

        public Context(DbConnection conn,bool ContextOwnsConnection) : base(conn, ContextOwnsConnection)
        {

        }

        public virtual DbSet<Donator> Donators { get; set; }

        public virtual DbSet<InputAccount> InputAccounts { get; set; }

        public virtual DbSet<OutputAccount> OutputAccounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Donator>().Property(d => d.RowVersion).IsRowVersion();
            base.OnModelCreating(modelBuilder);
        }
    }
}
