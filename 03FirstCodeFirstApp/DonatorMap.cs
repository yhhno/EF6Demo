using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02FirstCodeFirstApp
{
    public class DonatorMap:EntityTypeConfiguration<Donator>
    {
        #region Version 3 配置伙伴类
        //public DonatorMap()
        //{
        //    ToTable("DonatorFromConfig").HasKey(m => m.DonitorId);//为了区分之前的结果
        //    Property(m => m.Name)
        //        .IsRequired() //将Name设置为必须的
        //        .HasColumnName("DonatorName");//为了区分之前的结果，将Name映射到数据表的DonatorName。

        //}
        #endregion

        #region Version 4 一对多关系   EF默认链接关系

        //public DonatorMap()
        //{
        //    ToTable("Donators");
        //    Property(m => m.Name)
        //        .IsRequired();//将Name设置为必须的
        //}
        #endregion

        #region Version 4.1 一对多关系  手动配置链接关系

        public DonatorMap()
        {
            HasMany(d => d.PayWays)//HasMany方法告诉EF在Donator和Payway类之间有一个一对多的关系
                .WithRequired()//WithRequired方法表明链接在PayWays属性上的Donator是必须的，换言之，Payway对象不是独立的对象，必须要链接到一个Donator
                .HasForeignKey(p => p.DonatorId);//HasForeignKey方法会识别哪一个属性会作为链接
        }
        #endregion
    }
}
