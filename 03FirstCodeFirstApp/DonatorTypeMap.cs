using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02FirstCodeFirstApp
{
    //Version 4.2时添加的
    public class DonatorTypeMap:EntityTypeConfiguration<DonatorType>
    {
        public DonatorTypeMap()
        {
            HasMany(dt=>dt.Donators)
                .WithOptional(d=>d.DonatorType)//表示 外键约束可以为空
                .HasForeignKey(d=>d.DonatorTypeId)//
                .WillCascadeOnDelete(false);//可以指定约束的删除规则
        }
    }
}
