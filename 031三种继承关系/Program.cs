using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _031三种继承关系
{
    class Program
    {
        static void Main(string[] args)
        {

            //应用初始化器，，数据库模式改变时
            Database.SetInitializer(new Initializer());
            //TPT继承();

            //TPH继承();

            TPC继承();

            FluentConsole.Green.Line("操作完成！");
            Console.ReadKey();
        }

        ////TPT继承
        //public static void TPT继承()
        //{
        //    using (var context = new Context())
        //    {
        //        var employee = new Employee
        //        {
        //            Name = "TPT继承 Employee类的对象",
        //            Email = "11@qq.com",
        //            PhoneNumber = "11111111",
        //            Salary = 123

        //        };

        //        var vendor = new Vendor()
        //        {
        //            Name = "TPH继承 Vendor类的对象",
        //            Email = "11@qq.com",
        //            PhoneNumber = "123123",
        //            HourlyRate = DateTime.Parse("2016-7-7")
        //        };

        //        context.Person.Add(employee);
        //        context.Person.Add(vendor);

        //        context.SaveChanges();
        //    }
        //}

        
       public void test()
        {
        }

        //TPH继承
        //public static void TPH继承()
        //{
        //    using (var context = new Context())
        //    {
        //        var employee = new Employee
        //        {
        //            Name = "TPH继承 Employee类的对象",
        //            Email = "farbguo@qq.com",
        //            PhoneNumber = "12345678",
        //            Salary = 1234
        //        };

        //        var vendor = new Vendor
        //        {
        //            Name = "TPH继承 Vendor类的对象",
        //            Email = "farbguo@outlook.com",
        //            PhoneNumber = "78956131",
        //            HourlyRate = DateTime.MaxValue
        //        };

        //        context.TPHPerson.Add(employee);
        //        context.TPHPerson.Add(vendor);
        //        context.SaveChanges();
        //    }
        //}



        //TPC继承



        public static void TPC继承()
        {
            using (var context = new Context())
            {
                var employee = new Employee
                {
                    Name = "TPC继承 Employee类的对象",
                    Email = "farbguo@qq.com",
                    PhoneNumber = "12345678",
                    Salary = 1234
                };

                var vendor = new Vendor
                {
                    Name = "TPC继承 Vendor类的对象",
                    Email = "farbguo@outlook.com",
                    PhoneNumber = "78956131",
                    HourlyRate = DateTime.MaxValue
                };

                context.People.Add(employee);
                context.People.Add(vendor);
                context.SaveChanges();
            }
        }
    }
}
