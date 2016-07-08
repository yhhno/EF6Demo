﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace _031三种继承关系
{
    #region Version1 TPT继承
    //[Table("031TPT继承Employees")]//使用数据注解的方式映射到数据库，把它映射到我们想要的表名
    //public class Employee : Person
    //{
    //    public decimal Salary { get; set; }
    //}
    #endregion

    #region Version2 TPH继承
    //public class Employee : Person
    //{
    //    public decimal Salary { get; set; }
    //}
    #endregion
    #region Version3 TPC继承
    public class Employee : Person
    {
        public decimal Salary { get; set; }
    }
    #endregion
}