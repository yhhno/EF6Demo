using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02FirstCodeFirstApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var context = new Context())
            //{
            //    //context.Database.Create();//如果数据库存在报错，Db已存在错误
            //    context.Database.CreateIfNotExists();//如果数据库不存在时则创建,,如果存在同名，不做操作，重复操作时不会报错，
            //}

            //应用初始化器
            Database.SetInitializer(new Initializer());


            //CreateSingleDonator();

            //CreateMultiDonatorsAsync();

            //RetrieveDonators();

            //UpdateDonators();

            //DeleteDonators();

            RetrievePayWays();


            Console.WriteLine("操作完成!");
            Console.Read();
        }


        //添加单个Donator
        public static void CreateSingleDonator()
        {
            using (var context = new Context())
                //Context的实例用了using语句包装起来，这是因为DbContext实现了IDisposable接口。Dbcontext还包含了DbConnection的实例，该实例指向了具有特定连接字符串的数据库。在EF中合适地释放数据库连接和ADO.NET中同等重要
            {
                context.Database.CreateIfNotExists(); //如果数据库不存在则创建

                Donator donators = new Donator() {Name = "单个", Amount = 50, DonateDate = new DateTime(2016, 4, 7)};

                context.Donators.Add(donators);
                context.SaveChanges();

            }
        }

        //添加多个Donator
        public static async void CreateMultiDonatorsAsync()
        {
            using (var context = new Context())
            {
                context.Database.CreateIfNotExists();
                var donators = new List<Donator>
                {
                    new Donator
                    {
                        Name = "多个11",
                        Amount = 50,
                        DonateDate = new DateTime(2016, 4, 7)
                    },
                    new Donator
                    {
                        Name = "多个22",
                        Amount = 5,
                        DonateDate = new DateTime(2016, 4, 8)
                    },
                    new Donator
                    {
                        Name = "多个33",
                        Amount = 18.8m,
                        DonateDate = new DateTime(2016, 4, 15)
                    }
                };

                context.Donators.AddRange(donators);
               int i=await context.SaveChangesAsync();
            }
        }

        //检索Donator
        public static void RetrieveDonators()
        {
            using (var context = new Context())
            {
                var donators = context.Donators;
                Console.WriteLine("Id\t\t姓名\t\t金额\t\t赞助日期");
                foreach (var donator in donators)
                {
                    Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", donator.DonitorId, donator.Name, donator.Amount,
                        donator.DonateDate.ToShortDateString());
                }
            }
        }

        //更新Donator
        public static void UpdateDonators()
        {
            using (var context = new Context())
            {
                var donators = context.Donators;
                if (donators.Any())//any扩展方法来判断序列中是否由元素，
                {
                    var toBeUpdatedDonator = donators.First(d => d.Name =="多个11");//然后使用First扩展方法来找到元素
                    toBeUpdatedDonator.Name = "多个11已修改";//然后给目标对象的Name属性赋予新值，
                    context.SaveChanges();//最后调用SaveChanges方法
                }
            }
        }
        //删除Donators
        public static void DeleteDonators()
        {
            using (var context = new Context())
            {
                var toBeDeletedDonator = context.Donators.Single(d => d.Name == "多个11已修改");//根据Name找到要删除的目标测试数据
                context.SaveChanges();//最后持久化到数据库
            }
        }

        //检索PayWays
        public static void RetrievePayWays()
        {
            using (var context = new Context())
            {
                var payways = context.PayWays;
                Console.WriteLine("Id\t\t支付方式");
                foreach (var payway in payways)
                {
                    Console.WriteLine("{0}\t\t{1}",payway.Id,payway.Name);
                }
            }
        }

    }
}
