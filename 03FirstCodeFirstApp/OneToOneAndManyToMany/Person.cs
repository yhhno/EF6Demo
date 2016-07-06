using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02FirstCodeFirstApp
{
    #region Version1 OneToOne
    //public class Person
    //{
    //    public int PersonId { get; set; }
    //    public string Name { get; set; }
    //    public bool IsActive { get; set; }
    //    public virtual Student Student { get; set; }//注意这里我们为了启用懒加载，用了virtual
    //}
    #endregion

    #region Version2   ManyToMany
    public class Person
    {
        public Person()
        {
            Companies = new HashSet<Company>();
        }
        public int PersonId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public virtual Student Student { get; set; }//注意这里我们为了启用懒加载，用了virtual

        public virtual ICollection<Company> Companies { get; set; } 
    }

    #endregion
}
