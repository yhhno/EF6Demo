using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05EF中ViewsAndStoreProcedure
{
    public class DonatorContext:DbContext
    {
        public DonatorContext() : base("name=FirstCodeFirstApp")
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DonatorContext>());
        }

        public virtual DbSet<DonatorViewInfo> DonatorViews { get; set; }
        public virtual DbSet<Donator> Donators { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DonatorViewInfoMap());
            modelBuilder.Configurations.Add(new DonatorMap());
            modelBuilder.Configurations.Add(new ProvinceMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
