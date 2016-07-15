using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05EF中ViewsAndStoreProcedure
{
    public class DonatorViewInfoMap:EntityTypeConfiguration<DonatorViewInfo>
    {
        public DonatorViewInfoMap()
        {
            HasKey(d => d.DonatorId);
            ToTable("DonatorViews");
        }
    }
}
