using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using System.Data.Entity;

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
    }
}
