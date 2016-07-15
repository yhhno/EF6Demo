using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _05EF中ViewsAndStoreProcedure
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new Initializer());

            //视图查询();

            //另一种视图查询();

            //从存储过程中查询();

            执行存储过程无返回值();

            FluentConsole.Green.Line("操作完成！");
            Console.ReadKey();
        }

        public static void 视图查询()
        {
            using (var db = new DonatorContext())
            {
                var donators = db.DonatorViews;
                foreach (var donatorViewInfo in donators)
                {
                    Console.WriteLine(donatorViewInfo.ProvinceName + "\t" + donatorViewInfo.DonatorId + "\t" + donatorViewInfo.DonatorName + "\t" + donatorViewInfo.Amount + "\t" + donatorViewInfo.DonateDate);
                }
            }
            Console.WriteLine("视图查询");
        }

        public static void 另一种视图查询()
        {
            using (var db = new DonatorContext())
            {
                var sql = @"select donatorid,donatorname,amount,donatedate,provincename from dbo.donatorviews where provincename={0}";
                var donatorsViaCommand = db.Database.SqlQuery<DonatorViewInfo>(sql, "河北省");
                foreach (var donator in donatorsViaCommand)
                {
                    Console.WriteLine(donator.ProvinceName + "\t" + donator.DonatorId + "\t" + donator.DonatorName + "\t" + donator.Amount + "\t" + donator.DonateDate);
                }
            }
        }

        public static void 从存储过程中查询()
        {
            using (var db = new DonatorContext())
            {
                var sql = "SelectDonators {0}";
                var donators = db.Database.SqlQuery<DonatorFromStoreProcedure>(sql, "山东省");
                foreach (var donator in donators)
                {
                    FluentConsole.Green.Line(donator.ProvinceName + "\t" + donator.Name + "\t" + donator.Amount + "\t" +
                                             donator.DonateDate);
                }
            }
            Console.WriteLine("从存储过程中获取数据");
        }

        public static void 执行存储过程无返回值()
        {
            using (var db = new DonatorContext())
            {
                var sql = "UpdateHeBeiDonator {0},{1}";
                FluentConsole.Green.Line("执行存储过程前的数据为：");
                PrintDonators();
                var rowsAffected = db.Database.ExecuteSqlCommand(sql, "Update", 10m);
                FluentConsole.Green.Line("影响的行数为{0}条，", rowsAffected);
                FluentConsole.Green.Line("执行存储过程之后的数据为：");
                PrintDonators();
            }
           
            

        }

        public static void PrintDonators()
        {
            using (var db = new DonatorContext())
            {
                var donators = db.Donators.Where(p => p.ProvinceId == 2);//找出河北省的打赏者
                foreach (var donator in donators)
                {
                    Console.WriteLine(donator.Name + "\t" + donator.Amount + "\t" + donator.DonateDate);
                }
            }
        }

        public static async Task<IEnumerable<Donator>>  异步查询对象列表()
        {
            using (var db = new DonatorContext())
            {
                return await db.Donators.ToListAsync();
            }
        }

        public static async Task 异步创建对象(Donator donator)
        {
            using (var db = new DonatorContext())
            {
                db.Donators.Add(donator);
                await db.SaveChangesAsync();
            }
        }

        public static async Task<Donator> 异步定位一条记录(int donatorId)
        {
            using (var db = new DonatorContext())
            {
                return await db.Donators.FindAsync(donatorId);
                //return await db.Donators.FirstAsync(donatorId);
            }
        }

        public static async Task<int> 异步聚合函数()
        {
            using (var db = new DonatorContext())
            {
                return await db.Donators.CountAsync();
            }
        }


        public static async Task 异步遍历查询结果()
        {
            using (var db = new DonatorContext())
            {
                await db.Donators.ForEachAsync(d =>
                {
                    d.DonateDate = DateTime.Today;
                });
            }
        }

    }
}
