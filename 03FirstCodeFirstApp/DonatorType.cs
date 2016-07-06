using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02FirstCodeFirstApp
{
    public class DonatorType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Donator> Donators { get; set; } //我们要在DonatorType类中添加一个集合属性，表示每种赞助者类型有很多赞助者
    }
}
