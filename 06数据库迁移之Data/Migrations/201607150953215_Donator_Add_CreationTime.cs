namespace _06数据库迁移之Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Donator_Add_CreationTime : DbMigration
    {
        public override void Up()
        {

            AddColumn("dbo.Donators", "CreationTime", c => c.DateTime(nullable: false,defaultValueSql:"GetDate()"));//我们使用了SQL Server中的GetDate函数使用当前日期填充新加入的列
        }
        
        public override void Down()
        {
            DropColumn("dbo.Donators", "CreationTime");
        }
    }
}
