﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07管理事务和并发
{
    [Table("OutputAccounts")]
   public class OutputAccount
    {
        public int Id { get; set; }
        [StringLength(8)]
        public string Name { get; set; }

        public decimal Balance { get; set; }
    }
}
