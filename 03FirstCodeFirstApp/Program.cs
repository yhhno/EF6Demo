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

            //RetrievePayWays();

            //OneToManyDefault();

            //OneToManyCustom();

            //OnToManySelect();

            //OneToOne();

            ManyToMany();
            //Console.WriteLine("操作完成!");
            FluentConsole.Green.Line("操作完成!");
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
                    Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", donator.Id, donator.Name, donator.Amount,
                        donator.DonateDate.ToShortDateString());

                    //Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", donator.DonitorId, donator.Name, donator.Amount,
                    //   donator.DonateDate.ToShortDateString());
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
                //Console.WriteLine("Id\t\t支付方式");
                FluentConsole.Green.Line("Id\t\t支付方式");
                foreach (var payway in payways)
                {
                    //Console.WriteLine("{0}\t\t{1}",payway.Id,payway.Name);
                    FluentConsole.Red.Line("{0}\t\t{1}", payway.Id, payway.Name);
                }
            }
        }

        //一对多关系 EF默认关系
        public static void OneToManyDefault()
        {
            using (var context = new Context())
            {
                var donator = new Donator
                {
                    Amount = 6,
                    Name = "一对多时，添加的捐赠者",
                    DonateDate = DateTime.Parse("2016-7-6")
                };

                donator.PayWays.Add(new PayWay {Name="支付宝"});
                donator.PayWays.Add(new PayWay {Name="微信"});
                context.Donators.Add(donator);
                context.SaveChanges();
            }
        }



        //一对多关系， 自定义配置链接
        public static void OneToManyCustom()
        {
            using (var context = new Context())
            {
                var donator = new Donator
                {
                    Amount = 6,
                    Name = "一对多时，添加的捐赠者",
                    DonateDate = DateTime.Parse("2016-7-6")
                };

                donator.PayWays.Add(new PayWay { Name = "支付宝" });
                donator.PayWays.Add(new PayWay { Name = "微信" });
                context.Donators.Add(donator);
                context.SaveChanges();
            }
        }

        //一对多关系， 主实体有查询属性
        public static void OnToManySelect()
        {
            using (var context = new Context())
            {
                var donatorType = new DonatorType()
                {
                    Name = "博客园园友",
                    Donators = new List<Donator>
                    {
                        new Donator
                        {
                            Amount=6,
                            Name="一对多关系，主实体有查询属性",
                            DonateDate=DateTime.Parse("2016-7-6"),
                            PayWays=new List<PayWay>
                            {
                                new PayWay{Name="支付宝" },
                                new PayWay{ Name="微信"}
                            }
                        }
                    }
                };



                var donatorType2 = new DonatorType
                {
                    Name = "非博客园园友",
                    Donators = new List<Donator>
                    {

                        new Donator
                        {
                            Amount = 10,
                            Name = "一对多关系",
                            DonateDate = DateTime.Parse("2016-4-27"),
                            PayWays = new List<PayWay> {new PayWay {Name = "支付宝"}, new PayWay {Name = "微信"}}
                        }

                    }
                };



                context.DonatorTypes.Add(donatorType);
                context.DonatorTypes.Add(donatorType2);
                context.SaveChanges();
            }
        }

        //一对一关系
        public static void OneToOne()
        {
            using (var context = new Context())
            {
                var student = new Student
                {
                    CollegeName = "XX大学",
                    EnrollmentDateTime = DateTime.Parse("2016-7-6"),
                    Person = new Person
                    {
                        Name = "学生名",
                    }
                };

                context.Students.Add(student);
                context.SaveChanges();
            }

        }

        //多对多关系
        public static void ManyToMany()
        {
            using (var context = new Context())
            {
                var person = new Person
                {
                    Name = "比尔盖茨",
                };
                var person2 = new Person
                {
                    Name = "乔布斯",
                };
                context.Persons.Add(person);
                context.Persons.Add(person2);

                var company = new Company
                {
                    CompanyName = "微软",
                };
                company.Persons.Add(person);
                company.Persons.Add(person2);

                context.Companies.Add(company);
                context.SaveChanges();
            }
        }

    }
}
