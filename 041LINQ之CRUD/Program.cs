using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _041LINQ之CRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            #region LINQ 操作

            //AddDonator();

            //查询语法Retrieve();

            //方法语法Retrieve();

            //查询语法导航属性根据某个Province得到所有的Donator();

            //方法语法导航属性根据某个province得到所有的Donator();

            //查询语法导航属性根据某个Donator得到Province();

            //方法语法导航属性根据某个Donator得到Province();

            //查询语法过滤数据();

            //方法语法过滤数据();

            //查询语法LINQ投影();

            //方法语法LINQ投影();

            //查询语法分组Group();

            //方法语法分组Group();

            //查询语法升序排序();

            //方法语法升序排序();

            //查询语法降序排序();

            //方法语法降序排序();

            //聚合操作求和();

            //其他聚合操作();

            //分页PagingSkip();

            //分页PagingTake();

            //数据分页实现();

            //查询语法多表连接();

            //方法语法多表连接();

            #endregion

            //InsertProvince();

            //InsertProvince2();

            //桌面应用UpdateDonator();

            //Web应用UpdateProvince();

            //EF追踪与不追踪性能比较();

            //消除EF追踪();

            //Web应用另一种更新操作();

            //常规删除数据();

            //设置实体状态删除();

            使用内存数据();

            FluentConsole.Green.Line("操作完成！");
            Console.ReadKey();
        }

        #region LINQ 操作

        //添加捐赠者
        public static void AddDonator()
        {
            using (var context = new DonatorsContext())
            {
                var donator = new Donator
                {
                    Name = "小黄",
                    Amount = 12m,
                    DonateDate = DateTime.MaxValue,
                    Province = new Province
                    {
                        ProvinceName = "新疆"
                    }
                };
                context.Donators.Add(donator);
                context.SaveChanges();
            }
        }

        //查询语法检索捐赠者
        public static void 查询语法Retrieve()
        {
            using (var db = new DonatorsContext())
            {
                var donators = from donator in db.Donators
                    where donator.Amount == 50m
                    select donator;
                FluentConsole.Green.Line("Id\t姓名\t金额\t打赏时间");
                foreach (var donator in donators)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", donator.Id, donator.Name, donator.Amount,
                        donator.DonateDate.ToShortDateString());
                }
            }

            Console.WriteLine("查询语法检索完成！");
        }

        //方法语法检索捐赠者
        public static void 方法语法Retrieve()
        {
            using (var db = new DonatorsContext())
            {
                var donators = db.Donators.Where(d => d.Amount == 50m);
                FluentConsole.Green.Line("Id\t姓名\t金额\t打赏时间");
                foreach (var donator in donators)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", donator.Id, donator.Name, donator.Amount,
                        donator.DonateDate.ToShortDateString());
                }
            }

            Console.WriteLine("查询语法检索完成！");
        }


        //查询语法， 利用导航属性，得到匹配某个Province的所有Donators
        public static void 查询语法导航属性根据某个Province得到所有的Donator()
        {
            using (var db = new DonatorsContext())
            {
                var donators = from province in db.Provinces
                    where province.ProvinceName == "山东省"
                    from donator in province.Donators
                    select donator;

                FluentConsole.Green.Line("Id\t姓名\t金额\t打赏时间");
                foreach (var donator in donators)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", donator.Id, donator.Name, donator.Amount,
                        donator.DonateDate.ToShortDateString());
                }
            }

            Console.WriteLine("查询语法导航属性,利用Province中的导航属性，找到符合某个Province的所有打赏者！");
        }

        //方法语法，利用导航属性，得到匹配某个Province的所有Donators
        public static void 方法语法导航属性根据某个province得到所有的Donator()
        {
            using (var db = new DonatorsContext())
            {
                var donators = db.Provinces.Where(p => p.ProvinceName == "山东省").SelectMany(p => p.Donators);
                FluentConsole.Green.Line("Id\t姓名\t金额\t打赏时间");
                foreach (var donator in donators)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", donator.Id, donator.Name, donator.Amount,
                        donator.DonateDate.ToShortDateString());
                }
            }
            Console.WriteLine("方法语法导航属性,利用Province中的导航属性，找到符合某个Province的所有打赏者！");
        }

        //查询语法， 利用导航属性，得到匹配某个Donator的所有Province
        public static void 查询语法导航属性根据某个Donator得到Province()
        {
            using (var db = new DonatorsContext())
            {
                var provinces = from donator in db.Donators
                    where donator.Name == "雪茄"
                    select donator.Province;

                FluentConsole.Green.Line("Id\t省份");
                foreach (var province in provinces)
                {
                    Console.WriteLine("{0}\t{1}", province.Id, province.ProvinceName);
                }
            }
            Console.WriteLine("查询语法导航属性,利用Donator中的导航属性，找到符合某个Donator的省份！");
        }

        //方法语法， 利用导航属性，得到匹配某个Donator的所有Province
        public static void 方法语法导航属性根据某个Donator得到Province()
        {
            using (var db = new DonatorsContext())
            {
                var provinces = db.Donators.Where(d => d.Name == "雪茄").Select(d => d.Province);
                FluentConsole.Green.Line("Id\t省份");
                foreach (var province in provinces)
                {
                    Console.WriteLine("{0}\t{1}", province.Id, province.ProvinceName);
                }
            }
            Console.WriteLine("方法语法导航属性,利用Donator中的导航属性，找到符合某个Donator的省份！");
        }

        //查询语法，过滤数据
        public static void 查询语法过滤数据()
        {
            using (var db = new DonatorsContext())
            {
                var donators = from donator in db.Donators
                    where donator.Amount > 10 && donator.Amount < 20
                    select donator;
                FluentConsole.Green.Line("Id\t姓名\t金额\t打赏时间");
                foreach (var donator in donators)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", donator.Id, donator.Name, donator.Amount,
                        donator.DonateDate.ToShortDateString());
                }
            }

            Console.WriteLine("查询语法过滤数据！");
        }

        //方法语法，过滤数据
        public static void 方法语法过滤数据()
        {
            using (var db = new DonatorsContext())
            {
                var donators = db.Donators.Where(d => d.Amount > 10 && d.Amount < 20);

                FluentConsole.Green.Line("Id\t姓名\t金额\t打赏时间");
                foreach (var donator in donators)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", donator.Id, donator.Name, donator.Amount,
                        donator.DonateDate.ToShortDateString());
                }
            }

            Console.WriteLine("方法语法过滤数据！");
        }

        //查询语法,LINQ投影
        public static void 查询语法LINQ投影()
        {
            using (var db = new DonatorsContext())
            {
                var donators = from province in db.Provinces
                    select new
                    {
                        Province = province.ProvinceName,
                        DonatorList = province.Donators
                    };
                FluentConsole.Green.Line("省份\t打赏者");
                foreach (var donator in donators)
                {
                    foreach (var subdonator in donator.DonatorList)
                    {
                        Console.WriteLine("{0}\t{1}", donator.Province, subdonator.Name);
                    }
                }


            }

            Console.WriteLine("查询语法：LINQ投影");
        }

        //方法语法，LINQ投影
        public static void 方法语法LINQ投影()
        {
            using (var db = new DonatorsContext())
            {
                var donators = db.Provinces.Select(p => new DonatorsWithProvinceViewModel
                {
                    Province = p.ProvinceName,
                    DonatorList = p.Donators
                });
                FluentConsole.Green.Line("省份\t打赏者");
                foreach (var donator in donators)
                {
                    foreach (var subdonator in donator.DonatorList)
                    {
                        Console.WriteLine("{0}\t{1}", donator.Province, subdonator.Name);
                    }
                }

            }
            Console.WriteLine("方法语法：LINQ投影！");

        }

        //查询语法，分组Group
        public static void 查询语法分组Group()
        {
            using (var db = new DonatorsContext())
            {
                //代码会根据省份名称进行分组，最终以匿名对象的投影返回，结果中的ProvinceName就是分组时用到的字段，Donators属性包含了通过ProvinceName找到的Donators集合
                var donatorsWithProvince = from donator in db.Donators
                    group donator by donator.Province.ProvinceName
                    into donatorGroup
                    select new
                    {
                        ProvinceName = donatorGroup.Key,
                        Donators = donatorGroup
                    };
                foreach (var dwp in donatorsWithProvince)
                {
                    FluentConsole.Green.Line("{0}的打赏者如下：", dwp.ProvinceName);
                    foreach (var d in dwp.Donators)
                    {
                        Console.WriteLine("{0}\t{1}", d.Name, d.Amount);
                    }
                }
            }

            Console.WriteLine("查询语法：分组Group！");
        }

        //方法语法， 分组Group
        public static void 方法语法分组Group()
        {
            using (var db = new DonatorsContext())
            {
                //代码会根据省份名称进行分组，最终以匿名对象的投影返回，结果中的ProvinceName就是分组时用到的字段，Donators属性包含了通过ProvinceName找到的Donators集合
                var donatorsWithProvince = db.Donators.GroupBy(d => d.Province.ProvinceName)
                    .Select(donatorGroup => new
                    {
                        ProvinceName = donatorGroup.Key,
                        Donators = donatorGroup
                    });
                foreach (var dwp in donatorsWithProvince)
                {
                    FluentConsole.Green.Line("{0}的打赏者如下：", dwp.ProvinceName);
                    foreach (var d in dwp.Donators)
                    {
                        Console.WriteLine("{0}\t{1}", d.Name, d.Amount);
                    }
                }
            }

            Console.WriteLine("方法语法：分组Group！");
        }

        //查询语法，升序排序
        public static void 查询语法升序排序()
        {
            using (var db = new DonatorsContext())
            {
                var donators = from donator in db.Donators
                    orderby donator.Amount ascending
                    //ascending可省略
                    select donator;

                FluentConsole.Green.Line("Id\t姓名\t金额\t打赏时间");
                foreach (var donator in donators)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", donator.Id, donator.Name, donator.Amount,
                        donator.DonateDate.ToShortDateString());
                }
            }

            Console.WriteLine("查询语法升序排序！");
        }

        //方法语法，升序排序
        public static void 方法语法升序排序()
        {
            using (var db = new DonatorsContext())
            {
                var donators = db.Donators.OrderBy(d => d.Amount);

                FluentConsole.Green.Line("Id\t姓名\t金额\t打赏时间");
                foreach (var donator in donators)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", donator.Id, donator.Name, donator.Amount,
                        donator.DonateDate.ToShortDateString());
                }
            }

            Console.WriteLine("方法语法升序排序！");
        }


        //查询语法降序排序
        public static void 查询语法降序排序()
        {
            using (var db = new DonatorsContext())
            {
                var donators = from donator in db.Donators
                    orderby donator.Amount descending
                    select donator;

                FluentConsole.Green.Line("Id\t姓名\t金额\t打赏时间");
                foreach (var donator in donators)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", donator.Id, donator.Name, donator.Amount,
                        donator.DonateDate.ToShortDateString());
                }
            }

            Console.WriteLine("查询语法降序排序！");
        }

        //方法语法，降序排序
        public static void 方法语法降序排序()
        {
            using (var db = new DonatorsContext())
            {
                var donators = db.Donators.OrderByDescending(d => d.Amount);

                FluentConsole.Green.Line("Id\t姓名\t金额\t打赏时间");
                foreach (var donator in donators)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}", donator.Id, donator.Name, donator.Amount,
                        donator.DonateDate.ToShortDateString());
                }
            }

            Console.WriteLine("方法语法降序排序！");
        }

        //聚合操作，求和
        public static void 聚合操作求和()
        {
            using (var db = new DonatorsContext())
            {
                //方法语法更简洁，而且查询语法还要讲前面的LINQ SQL用括号括起来才能进行聚合（其实这是混合语法），没有方法语法简洁
                var count = (from donator in db.Donators
                    where donator.Province.ProvinceName == "山东省"
                    select donator).Count();

                var count1 = db.Donators.Count(d => d.Province.ProvinceName == "山东省");

                FluentConsole.Green.Line("查询语法Count={0},方法语法Count={1}", count, count1);
            }
        }

        //其他聚合操作
        public static void 其他聚合操作()
        {
            using (var db = new DonatorsContext())
            {
                var sum = db.Donators.Sum(d => d.Amount); //计算所有打赏者的金额总和
                var min = db.Donators.Min(d => d.Amount); //最少的打赏金额
                var max = db.Donators.Max(d => d.Amount); //最大的打赏金额
                var average = db.Donators.Average(d => d.Amount); //打赏金额的平均值

                FluentConsole.Green.Line("Sum={0},Min={1},Average={2},Max={3}", sum, min, average, max);

            }
        }


        public static void PrintDonators(IQueryable<Donator> donators)
        {
            FluentConsole.Green.Line("Id\t\t姓名\t\t金额\t\t打赏金额");
            foreach (var donator in donators)
            {
                Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}", donator.Id, donator.Name, donator.Amount,
                    donator.DonateDate.ToShortDateString());
            }
        }

        //方法语法， 分页PagingSkip
        public static void 分页Paging之Skip()
        {
            using (var db = new DonatorsContext())
            {
                var donatorsBefore = db.Donators;
                var donatorsAfter = db.Donators.OrderBy(d => d.Id).Skip(2);
                Console.WriteLine("原始数据打印结果：");
                PrintDonators(donatorsBefore);
                Console.WriteLine("Skip（2）之后的结果");
                PrintDonators(donatorsAfter);
            }
            Console.WriteLine("分页PagingSkip，操作完毕！");
        }

        //方法语法，分页PagingTake
        public static void 分页Paging之Take()
        {
            using (var db = new DonatorsContext())
            {
                var donatorsBefore = db.Donators;
                var donatorsAfter = db.Donators.OrderBy(d => d.Amount).Take(2);
                Console.WriteLine("原始数据打印结果：");
                PrintDonators(donatorsBefore);
                Console.WriteLine("Take(3)之后的结果：");
                PrintDonators(donatorsAfter);

            }
            Console.WriteLine("方法语法，分页PagingTake！");
        }


        //方法语法，数据分页实现
        public static void 数据分页实现()
        {
            using (var db = new DonatorsContext())
            {
                while (true)
                {
                    FluentConsole.Green.Line("你要看第几页数据？");
                    string pageStr = Console.ReadLine() ?? "1";
                    int page = int.Parse(pageStr);
                    const int pageSize = 2;
                    if (page > 0 && page < 4)
                    {
                        var donators = db.Donators.OrderBy(d => d.Id).Skip((page - 1)*pageSize).Take(pageSize);
                        PrintDonators(donators);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            Console.WriteLine("数据分页实现完成！");
        }

        //查询方法， 多表连接
        public static void 查询语法多表连接()
        {
            using (var db = new DonatorsContext())
            {
                var join1 = from province in db.Provinces
                    join donator in db.Donators on province.Id equals donator.Province.Id
                        into donatorList
                    //注意，这里的donatorList是属于某个省份的所有打赏者，很多人误解为这是两张表join之后的结果集
                    select new
                    {
                        ProvinceName = province.ProvinceName,
                        DonatorList = donatorList
                    };

                FluentConsole.Green.Line("省份\t打赏者");
                foreach (var donator in join1)
                {
                    foreach (var subdonator in donator.DonatorList)
                    {
                        Console.WriteLine("{0}\t{1}", donator.ProvinceName, subdonator.Name);
                    }
                }

            }
            Console.WriteLine("查询语法多表连接！");
        }

        //方法语法，多表连接
        public static void 方法语法多表连接()
        {
            using (var db = new DonatorsContext())
            {
                var join = db.Provinces.GroupJoin(db.Donators, //Provinces集合要连接的Donator是集合
                    province => province.Id, //左表要连接的键
                    donator => donator.Province.Id, //右表要连接的键
                    (province, donatorGroup) => new //返回的结果集
                    {
                        ProvinceName = province.ProvinceName,
                        DonatorList = donatorGroup
                    });

                FluentConsole.Green.Line("省份\t打赏者");
                foreach (var donator in join)
                {
                    foreach (var subdonator in donator.DonatorList)
                    {
                        Console.WriteLine("{0}\t{1}", donator.ProvinceName, subdonator.Name);
                    }
                }

            }
            Console.WriteLine("方法语法多表连接！");
        }

        #endregion

        //Insert Province
        public static void InsertProvince()
        {
            var province = new Province {ProvinceName = "浙江省"};
            province.Donators.Add(new Donator
            {
                Name = "星空夜焰",
                Amount = 50m,
                DonateDate = DateTime.Parse("2016-7-12")
            });
            province.Donators.Add(new Donator
            {
                Name = "伟涛",
                Amount = 30m,
                DonateDate = DateTime.Parse("2016-7-12")
            });

            using (var db = new DonatorsContext())
            {
                db.Provinces.Add(province);
                db.SaveChanges();
            }

            Console.WriteLine("新建对象，然后添加到数据库上下文，这个表明了EF会追踪当时上下文中为attached或者added状态的实体");
        }

        //Insert Province ,修改实体的状态为added
        public static void InsertProvince2()
        {
            var province = new Province {ProvinceName = "广东省"};
            province.Donators.Add(new Donator
            {
                Name = "求余",
                Amount = 30,
                DonateDate = DateTime.Parse("2016-7-12")
            });

            using (var db = new DonatorsContext())
            {
                db.Entry(province).State = EntityState.Added;
                db.SaveChanges();
            }
            Console.WriteLine("InsertProvince,修改实体的状态为added");
        }

        //桌面应用,更新Dontor
        public static void 桌面应用UpdateDonator()
        {
            using (var db = new DonatorsContext())
            {
                var donator = db.Donators.Find(4);
                donator.Name = "醉千秋"; //我想把“醉、千秋”中的顿号去掉
                db.SaveChanges();
            }
            Console.WriteLine("桌面应用更新Donators， ");
        }

        //Web应用，更新Province
        public static void Web应用UpdateProvince()
        {
            var province = new Province {Id = 2, ProvinceName = "山东省更新"};
            province.Donators.Add(new Donator
            {
                Name = "醉、千秋", //再改回来
                Id = 4,
                Amount = 12m,
                DonateDate = DateTime.Parse("2015-7-12")
            });
            using (var db = new DonatorsContext())
            {
                db.Entry(province).State = EntityState.Modified;
                foreach (var donator in province.Donators)
                {
                    db.Entry(donator).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            Console.WriteLine("Web应用更新Province");
        }


        public static void EF追踪与不追踪性能比较()
        {
            using (var db = new DonatorsContext())
            {
                Console.WriteLine("循环10000次，正常查询开始");

                Stopwatch sw = Stopwatch.StartNew();

                for (int i = 0; i < 100000; i++)
                {
                    var provinceNormal = db.Provinces.Include(p => p.Donators);
                    //foreach (var province in provinceNormal)
                    //{
                    //    FluentConsole.Green.Line("省份追踪状态：{0}", db.Entry(province).State);
                    //    foreach (var donator in province.Donators)
                    //    {
                    //        Console.WriteLine("打赏者的追踪状态：{0}", db.Entry(donator).State);
                    //    }
                    //}
                }


                FluentConsole.Green.Line("循环10000次，正常查询结束，消耗时间:{0}", sw.ElapsedMilliseconds);



                Console.WriteLine("循环10000次，NoTracking查询开始：");
                sw.Restart();
                for (int i = 0; i < 100000; i++)
                {
                    var provinceNoTracking = db.Provinces.Include(p => p.Donators).AsNoTracking();
                        //使用AsNoTracking()方法设置不再追踪实体
                    //foreach (var province in provinceNoTracking)
                    //{
                    //    FluentConsole.Green.Line("省份追踪状态：{0}", db.Entry(province).State);
                    //    foreach (var donator in province.Donators)
                    //    {
                    //        Console.WriteLine("打赏者的追踪状态：{0}", db.Entry(donator).State);
                    //    }
                    //}
                }
                sw.Stop();
                FluentConsole.Green.Line("循环10000次，NoTracking查询结束，消耗时间:{0}", sw.ElapsedMilliseconds);
            }
        }

        public static void 消除EF追踪()
        {
            using (var db = new DonatorsContext())
            {
                Console.WriteLine("默认情况下，实体的状态");


                var provinceNormal = db.Provinces.Include(p => p.Donators);
                foreach (var province in provinceNormal)
                {
                    FluentConsole.Green.Line("省份追踪状态：{0}", db.Entry(province).State);
                    foreach (var donator in province.Donators)
                    {
                        Console.WriteLine("打赏者的追踪状态：{0}", db.Entry(donator).State);
                    }
                }



                Console.WriteLine("使用AsNoTracking()方法后，实体的状态");
                var provinceNoTracking = db.Provinces.Include(p => p.Donators).AsNoTracking();
                    //使用AsNoTracking()方法设置不再追踪实体
                foreach (var province in provinceNoTracking)
                {
                    FluentConsole.Green.Line("省份追踪状态：{0}", db.Entry(province).State);
                    foreach (var donator in province.Donators)
                    {
                        Console.WriteLine("打赏者的追踪状态：{0}", db.Entry(donator).State);
                    }
                }
            }

            Console.WriteLine("EF中实体的状态比较。");
        }

        public static void Web应用另一种更新操作()
        {
            var donator = new Donator
            {
                Id = 5,
                Name = "雪茄",
                Amount = 12m,
                DonateDate = DateTime.Parse("2016-7-13")
            };
            using (var db = new DonatorsContext())
            {
                db.Donators.Attach(donator);
                //db.Entry(donator).State=EntityState.Modified;//这句可以作为第二种方法替换上面一句代码
                donator.Name = "秦皇岛-雪茄";
                db.SaveChanges();
            }

            Console.WriteLine("Web应用另一种更新操作完成！");
        }

        //常规删除数据
        public static void 常规删除数据()
        {
            using (var db = new DonatorsContext())
            {
                PrintAllDonators(db);
                FluentConsole.Green.Line("删除后的数据如下：");
                var toDelete = db.Provinces.Find(2);
                toDelete.Donators.ToList().ForEach(d => db.Donators.Remove(d));
                db.Provinces.Remove(toDelete);
                db.SaveChanges();

                PrintAllDonators(db);
            }
        }

        public static void PrintAllDonators(DonatorsContext db)
        {
            var provinces = db.Provinces.ToList();
            foreach (var province in provinces)
            {
                FluentConsole.Green.Line("{0}的打赏者如下：", province.ProvinceName);
                foreach (var donator in province.Donators)
                {
                    Console.WriteLine("{0,-10}\t{1,-10}\t{2,-10}\t{3,-10}", donator.Id, donator.Name, donator.Amount,
                        donator.DonateDate.ToShortDateString());
                }
            }
        }
        //设置实体状态删除
        public static void 设置实体状态删除()
        {
            //通过设置实体状态删除
            var toDeleteProvince = new Province {Id = 1};
            toDeleteProvince.Donators.Add(new Donator
            {
                Id = 2
            });
            toDeleteProvince.Donators.Add(new Donator
            {
                Id = 3
            });
            toDeleteProvince.Donators.Add(new Donator
            {
                Id = 4
            });

            using (var db = new DonatorsContext())
            {
                PrintAllDonators(db); //删除前先输出现有的数据,不能写在下面的using语句中，否则Attach方法会报错，原因我相信你已经可以思考出来了
            }
            using (var db = new DonatorsContext())
            {
               
                db.Provinces.Attach(toDeleteProvince);
                foreach (var donator in toDeleteProvince.Donators.ToList())
                {
                    db.Entry(donator).State = EntityState.Deleted;
                }
                db.Entry(toDeleteProvince).State = EntityState.Deleted; //删除子实体再删除父实体。
                db.SaveChanges();
                Console.WriteLine("删除之后的数据如下：\r\n");
                PrintAllDonators(db);
            }


        }


        public static void 使用内存数据()
        {
            using (var db = new DonatorsContext())
            {
                var provinces = db.Provinces.ToList();
                //var query = db.Provinces.Find(3);//Find方法首先会去查询内存中的数据。

                //ChangeTracker的使用
                foreach (var dbEntityEntry in db.ChangeTracker.Entries<Province>())
                {
                    Console.WriteLine(dbEntityEntry.State);
                    Console.WriteLine(dbEntityEntry.Entity.ProvinceName);
                }
            }
        }


    }

}
