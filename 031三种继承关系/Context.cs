using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _031三种继承关系
{
    public  class Context:DbContext
    {

        public Context() : base("name=FirstCodeFirstApp")
        {
           
        }
        #region Version1 TPT继承
        //public virtual DbSet<Person> Person { get; set; }//我们只添加了实体Person的DbSet，因为其他的两个领域模型都是从这个模型派生的，所以我们也相当于将其他的连个类添加到了DbSet集合中了，这样EF会使用多态性来使用实际的领域模型， 当然你也可以使用Fluent Api 和实体伙伴类来配置这些细节信息，
        #endregion

        #region Version2 TPH继承
        //public virtual DbSet<Person> TPHPerson { get; set; }
        #endregion

        #region Verison3 TPC继承
        public virtual DbSet<Person> People { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Map(m =>
            {
                m.MapInheritedProperties();//方法将继承的属性映射到表中，然后我们根据不同的对象类型映射到不同的表中
                m.ToTable("Employees");
            });

            modelBuilder.Entity<Vendor>().Map(m =>
            {
                m.MapInheritedProperties();//方法将继承的属性映射到表中，然后我们根据不同的对象类型映射到不同的表中
                m.ToTable("Vendors");
            });

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
