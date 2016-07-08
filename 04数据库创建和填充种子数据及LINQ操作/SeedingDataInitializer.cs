using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04数据库创建和填充种子数据及LINQ操作
{
    public class SeedingDataInitializer:DropCreateDatabaseAlways<SeedingDataContext>
    {
        protected override void Seed(SeedingDataContext context)
        {
            for (int i = 0; i < 6; i++)
            {
                var employer = new Employer { EmployerName = "Employer" + (i + 1) };
                context.Employers.Add(employer);
            }
            base.Seed(context);
        }
    }
}
