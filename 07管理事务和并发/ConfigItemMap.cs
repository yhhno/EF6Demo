using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace _07管理事务和并发
{
    public class ConfigItemMap: EntityTypeConfiguration<ConfigItem>
    {
        public ConfigItemMap()
        {
            HasKey(e => e.Id);
            Property(e => e.Id).HasMaxLength(32);
            Property(e => e.AppName).HasMaxLength(256);
            Property(e => e.Name).IsRequired().HasMaxLength(256);
            Property(e => e.FriendlyName).HasMaxLength(256);
            Property(e => e.Description).IsMaxLength();
            Property(e => e.Value).IsMaxLength();
            Property(e => e.CreatedBy).HasMaxLength(256);
            Property(e => e.ModifiedBy).HasMaxLength(256);
            Property(e => e.RowVersion).IsConcurrencyToken().HasColumnType("Timestamp");
            Ignore(e => e.ObjectValue);
            Property(e => e.SourceId).HasMaxLength(32);
            Property(e => e.ValueTypeEnum).HasMaxLength(256);
            HasMany(e => e.ChildItems)
                .WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentId);
            Property(e => e.IsCompositeValue).IsRequired();
            Property(e => e.IsDeleted).IsRequired();
            Property(e => e.ItemsInited).IsRequired();
        }
    }
}
