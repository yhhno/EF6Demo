using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace _07管理事务和并发
{
    
    public class ConfigItem
    {
       
        public string Id { get; set; }
       
        public string ParentId { get; set; }
       
        public ConfigItem Parent { get; set; }
       
        public List<ConfigItem> ChildItems { get; set; }
       
        public string AppName { get; set; }
       
        public string Name { get; set; }
       
        public string FriendlyName { get; set; }
       
        public string Description { get; set; }
       
        public string Value { get; set; }
       
        public DateTime CreatedOn { get; set; }
       
        public string CreatedBy { get; set; }
       
        public DateTime? ModifiedOn { get; set; }
       
        public string ModifiedBy { get; set; }
       
        public byte[] RowVersion { get; set; }
       
        //[NotMapped]
        public object ObjectValue { get; set; }
       
        public string SourceId { get; set; }
       
        public string ValueType { get; set; }
       
        public string ValueTypeEnum { get; set; }
       
        public bool IsCompositeValue { get; set; }
       
        public bool IsDeleted { get; set; }
       
        public bool ItemsInited { get; set; }
        public ConfigItem()
        {
            CreatedOn = DateTime.Now;
            ChildItems = new List<ConfigItem>();
            IsDeleted = false;
            ItemsInited = false;
        }
        public override bool Equals(object obj)
        {
            ConfigItem configItem = (ConfigItem)obj;
            if (configItem.Id == this.Id)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
