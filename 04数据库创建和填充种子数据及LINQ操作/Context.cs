using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04数据库创建和填充种子数据及LINQ操作
{
    //public class Context : DbContext
    //{
    //    public Context() : base("name=FirstCodeFirstApp")
    //    {
    //        //无论什么时候创建上下文类，Database.SetInitializer方法都会调用，并且将数据库初始化策略设置为DropCreateDataIfModelChanges
    //        Database.SetInitializer<Context>(new DropCreateDatabaseIfModelChanges<Context>());
    //        //如果处于生产环境，那么我们肯定不想丢失已存在的数据。这时我们需要关闭该初始化器，只需要将null传给Database.SetInitializer方法
    //        //Database.SetInitializer<Context>(null);
    //    }
    //}

    #region SeedingDbContext

    public class SeedingDataContext : DbContext
    {
        public  SeedingDataContext() : base("name=FirstCodeFirstApp")
        {
            Database.SetInitializer<SeedingDataContext>(new SeedingDataInitializer());
        }
        public virtual DbSet<Employer> Employers { get; set; }
    }
    #endregion


}
