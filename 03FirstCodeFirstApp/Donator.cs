using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02FirstCodeFirstApp
{
    //[TrackChanges]//日志跟踪标志
    #region Version 1
    //public class Donator
    //{
    //    [Key]
    //    public int DonitorId { get; set; }
    //    public string Name { get; set; }
    //    public decimal Amount { get; set; }
    //    public DateTime DonateDate { get; set; }
    //}
    #endregion

    #region Version 2 特性注解
    //    [Table("Donator")]
    //public class Donator
    //{
    //    [Key]
    //    [Column("Id")]
    //    public int DonitorId { get; set; }
    //    [StringLength(10, MinimumLength = 2)]
    //    public string Name { get; set; }
    //    public decimal Amount { get; set; }
    //    public DateTime DonateDate { get; set; }
    //}
    #endregion

    #region Version3 Fluent API
    //public class Donator
    //{  
    //    public int DonitorId { get; set; }
    //    public string Name { get; set; }
    //    public decimal Amount { get; set; }
    //    public DateTime DonateDate { get; set; }
    //}
    #endregion

    #region Version 4 一对多关系  数据注解方式  EF默认管理
   
    //public class Donator
    //{

    //    public Donator()
    //    {
    //        PayWays =new HashSet<PayWay>();//为了避免潜在的null引用异常可能性，当Donator对象创建时，我们使用HashSet的T集合类型实例创建一个新的集合实例
    //    }
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public decimal Amount { get; set; }
    //    public DateTime DonateDate { get; set; }
    //    public virtual ICollection<PayWay> PayWays { get; set; } 
    //}

    #endregion

    #region Version 4.1  一对多关系  数据注解方式  手动配置链接关系

    //public class Donator
    //{

    //    public Donator()
    //    {
    //        PayWays = new HashSet<PayWay>();//为了避免潜在的null引用异常可能性，当Donator对象创建时，我们使用HashSet的T集合类型实例创建一个新的集合实例
    //    }
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public decimal Amount { get; set; }
    //    public DateTime DonateDate { get; set; }
    //    public virtual ICollection<PayWay> PayWays { get; set; }
    //}

    #endregion


    #region Version 4.2  一对多关系  数据注解方式  手动配置链接关系  （一对多另一种用例  主实体 有查询属性）

    public class Donator
    {

        public Donator()
        {
            PayWays = new HashSet<PayWay>();//为了避免潜在的null引用异常可能性，当Donator对象创建时，我们使用HashSet的T集合类型实例创建一个新的集合实例
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public DateTime DonateDate { get; set; }
        public virtual ICollection<PayWay> PayWays { get; set; }

        //这个用例出现在当主要实体上有一个查询属性，且该属性指向另一个实体时。查询属性指向一个完整的子实体的父亲，
       // 当操作或检查一个子记录需要访问父信息时，这些属性很有用。
         //   比如，我们再创建一个类DonatorType（该类用来表示赞助者的类型，比如有博客园园友和非博客园园友），然后给Donator类添加DonatorType属性。
           // 本质上，这个例子还是一个一对多的关系，但是方法有些不同。
          //  这种情况下，我们一般会在主实体的编辑页面使用一个包含查询父表值的下拉控件。我们的查询父表很简单，只有Id和Name列，
           // 我们将使该关系为可选的，以描述如何添加可空的外键。因此，Donator类中的DonatorTypeId必须是可空的
        public int? DonatorTypeId { get; set; }
        public virtual DonatorType DonatorType { get; set; }
    }

    #endregion
}
