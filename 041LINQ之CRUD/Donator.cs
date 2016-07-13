using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _041LINQ之CRUD
{
    public class Donator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public  decimal Amount { get; set; }
        public DateTime DonateDate { get; set; }

        public virtual Province Province { get; set; }
    }
}
