using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04数据库创建和填充种子数据及LINQ操作
{
    class Program
    {
        static void Main(string[] args)
        {
            RetrieveSeedingData();

            FluentConsole.Green.Line("操作完成！");
            Console.ReadKey();
        }

        //查询种子数据
        public static void RetrieveSeedingData()
        {
            using (var context = new SeedingDataContext())
            {
                var employers = context.Employers;

                foreach (var employer in employers)
                {
                    FluentConsole.Green.Line("Id={0}\tName={1}", employer.Id, employer.EmployerName);
                }

            }
            Console.WriteLine("查询种子数据");
        }
     
    }
}
