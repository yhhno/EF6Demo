using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace _02FirstCodeFirstApp
{
    //这里可以明显地看到，数据库上下文代表整个数据库，它包含多个表，每张表都成为了Context类的一个属性。
    public class Context:DbContext
    {
        public Context() : base("name=FirstCodeFirstApp")
        {
        }
        public DbSet<Donator> Donators { get; set;   }

        //数据库模式更改
        public DbSet<PayWay> PayWays { get; set; }

        //Version 4.2 添加的
        public DbSet<DonatorType> DonatorTypes { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Person> Persons { get; set; }

       //Version3 使用Fluent API来定义Donator表的数据库模式
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            #region Version3 
            //modelBuilder.Entity<Donator>().ToTable("Donators").HasKey(m => m.DonitorId);//映射到表Donators，DonatorId当做主键对待
            //modelBuilder.Entity<Donator>().Property(m => m.DonitorId).HasColumnName("Id");//映射到数据库表中的主键名为Id而不是DonatorId
            //modelBuilder.Entity<Donator>().Property(m => m.Name)
            //    .IsRequired()//设置Name是必须的，即不为null，默认是可为null的
            //    .IsUnicode()//设置Name是Unicode字符，实际上默认就是Unicode，所以该方法可不写
            //    .HasMaxLength(10);//最大长度为10
            //base.OnModelCreating(modelBuilder);
            #endregion

            #region Version 4 每个类配置一个伙伴类 比如DonatorMap类

            //modelBuilder.Configurations.Add(new DonatorMap());
            //base.OnModelCreating(modelBuilder);

            #endregion

            #region Version 4.2  一对多关系， 主实体有个查询属性

            modelBuilder.Configurations.Add(new DonatorMap());
            modelBuilder.Configurations.Add(new DonatorTypeMap());
            modelBuilder.Configurations.Add(new StudentMap());
            modelBuilder.Configurations.Add(new PersonMap());

            //从 model builder中移除全局的约定，在数据库上下文的OnModelCreating方法中关闭整个数据库模型的级联删除规则
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);

            #endregion

        }
    }
}
