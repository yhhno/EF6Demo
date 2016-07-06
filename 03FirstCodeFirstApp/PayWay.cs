using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02FirstCodeFirstApp
{
    #region Version 1
    //public class PayWay
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
    #endregion

    #region Version 2
    //    [Table("PayWay")]
    //public class PayWay
    //{
    //    public int Id { get; set; }
    //    [MaxLength(8,ErrorMessage="支付方式的名称长度不能大于8")]
    //    public string Name { get; set; }
    //}
    #endregion

    #region Version 4 一对多关系  数据注解  EF默认链接关系

    //[Table("PayWay")]
    //public class PayWay
    //{
    //    public int Id { get; set; }
    //    [MaxLength(8, ErrorMessage = "支付方式的名称长度不能大于8")]
    //    public string Name { get; set; }
    //}
    #endregion

    #region Version 4.1 一对多关系 手动指定链接关系
    [Table("PayWay")]
    public class PayWay
    {
        public int Id { get; set; }
        [MaxLength(8, ErrorMessage = "支付方式的名称长度不能大于8")]
        public string Name { get; set; }
        public int DonatorId { get; set; }
    }
    #endregion
}
