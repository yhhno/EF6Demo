using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02FirstCodeFirstApp
{
    public class StudentMap:EntityTypeConfiguration<Student>
    {
        public StudentMap()
        {
            HasRequired(s => s.Person)
                .WithOptional(p => p.Student);
            HasKey(s => s.PersonId);//使HasKey方法，指定一个表的主键，换而言之，这是一个允许我们找导一个实体的独一无二的值。
            Property(s => s.CollegeName)
                .HasMaxLength(50)
                .IsRequired();

        }
    }
}
