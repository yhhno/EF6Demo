using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace _031三种继承关系
{
    #region Version1 TPT继承
    ////使用EF默认约定映射到数据库，但是为了区分之前的Person表 这里也使用数据注解方式映射
    //[Table("031TPT继承Persons")]
    //public class Person
    //{

    //    public string Email { get; set; }

    //    public int Id { get; set; }

    //    public string Name { get; set; }

    //    public string PhoneNumber { get; set; }
    //}
    #endregion

    #region Version2 TPH继承
    //public class Person
    //{

    //    public string Email { get; set; }

    //    public int Id { get; set; }

    //    public string Name { get; set; }

    //    public string PhoneNumber { get; set; }
    //}
    #endregion
    #region Version3 TPC继承
    public abstract class Person
    {

        public string Email { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
    #endregion
}