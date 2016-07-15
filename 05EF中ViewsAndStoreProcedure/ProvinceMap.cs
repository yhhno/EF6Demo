using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05EF中ViewsAndStoreProcedure
{
    public class ProvinceMap:EntityTypeConfiguration<Province>
    {
        public ProvinceMap()
        {
            HasMany(p => p.Donators)
                .WithRequired()
                .HasForeignKey(d => d.ProvinceId);
        }
    }
}
