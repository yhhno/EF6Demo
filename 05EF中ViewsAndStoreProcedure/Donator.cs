using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05EF中ViewsAndStoreProcedure
{
    public class Donator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public  DateTime DonateDate { get; set; }
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
    }
}
