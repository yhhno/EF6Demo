using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02FirstCodeFirstApp
{
   public  class Student
    {
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }//注意这里我们为了启用懒加载，用了virtual
        public string CollegeName { get; set; }
        public DateTime EnrollmentDateTime { get; set; }
    }
}
