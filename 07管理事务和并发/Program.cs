using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace _07管理事务和并发
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new Initializer());

            //EF的默认并发();

            //处理字段级别的并发应用();

            //为并发实现RowVersion();

            //EF默认的事务处理();

            //使用TransactionScope处理事务();

            //使用EF6管理事务();

            使用已存在的事务();

            FluentConsole.Green.Line("操作完成！");
            Console.ReadKey();
        }
        #region 私有方法，即准备工作
        /*******EF默认并发************/
        public static Donator GetDonator(int id)
        {
            using (var db = new Context())
            {
                return db.Donators.Find(id);
            }
        }

        public static void UpdateDonator(Donator donator)
        {
            using (var db = new Context())
            {
                db.Entry(donator).State = EntityState.Modified;
                db.SaveChanges();
            }
        }




        /*********设计处理字段级别的并发********/
        public static Donator GetUpdatedDonator(int id,string name,decimal amount,DateTime donateDate)
        {
            return new _07管理事务和并发.Donator
            {
                Id=id,
                Name=name,
                Amount=amount,
                DonateDate=donateDate
            };
        }

        public static void UpdateDonatorEnhanced(Donator originalDonator,Donator newDonator)
        {
            using(var db=new Context())
            {
                //从数据库中检索最新的模型
                var donator = db.Donators.Find(originalDonator.Id);
                //接下来检查用户修改的每个属性
                if(originalDonator.Name!=newDonator.Name)
                {
                    //新值和原始值不同，说明有修改。
                    //然后就把新值更新到数据库
                    donator.Name = newDonator.Name;
                }
                if (originalDonator.Amount != newDonator.Amount)
                {
                    donator.Amount = newDonator.Amount;
                }
                //省略了其他属性的检查
                db.SaveChanges();
            };
        }

        /*************使用已存在的事务************/
        public static bool DebitOutputAccount(SqlConnection conn,SqlTransaction trans,int accountId,decimal amountToDebit)
        {
            int affectedRows = 0;
            var command = conn.CreateCommand();
            command.Transaction = trans;
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "Update OutputAccounts set Balance = Balance - @amountToDebit where id=@accountId";
            command.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter ("@amountToDebit",amountToDebit),
                new SqlParameter("@accountId",accountId)
            });

            try
            {
                affectedRows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return affectedRows == 1;
        }
        #endregion

        public static void EF的默认并发()
        {
            
                //1.用户甲获取id=1的打赏者
                var 用户甲获取的数据 = GetDonator(1);
                //2.用户乙也获取id=1的打赏者
                var 用户乙获取的数据 = GetDonator(1);

                //3.用户甲只更新这个实体的Name字段
                用户甲获取的数据.Name = "用户甲修改的";
                UpdateDonator(用户甲获取的数据);
                //4.用户乙只更新这个实体的Amount字段
                用户乙获取的数据.Amount = 111m;
                UpdateDonator(用户乙获取的数据);
          
        }


        public static void 处理字段级别的并发应用()
        {
            //用户甲读取ID=1的打赏者
            var donator1 = GetDonator(1);
            //用户乙同样读取id=1的打赏者
            var donator2 = GetDonator(1);
            //用户甲通过创建一个新的对象来更新打赏金额为100m
            var newDonator1 = GetUpdatedDonator(donator2.Id, donator1.Name, 100m, donator1.DonateDate);
            //执行用户甲的更新
            UpdateDonatorEnhanced(donator1, newDonator1);
            //用户乙通过创建一个新的对象来更新打赏者姓名为:"并发测试"
            var newDonator2 = GetUpdatedDonator(donator2.Id, "并发测试", donator2.Amount, donator2.DonateDate);
            //执行乙的更新
            UpdateDonatorEnhanced(donator1, newDonator2);
        }


        public static void 为并发实现RowVersion()
        {
            //1.用户甲获取id=1的打赏者
            var donator1 = GetDonator(1);
            //2.用户乙也获取id=1的打赏者
            var donator2 = GetDonator(1);
            //3.用户甲只更新这个实体的Name字段
            donator1.Name = "RowVersion下，用户甲修改Name值";

            UpdateDonator(donator1);


            try
            {
                //4.用户乙只更新这个实体的Amount字段
                //DbEntityEntry<Donator>
                donator2.Amount = 2m;
                UpdateDonator(donator2);
                Console.WriteLine("应该放生并发异常！");
            }
            catch (DbUpdateConcurrencyException ex)
            {

                FluentConsole.Green.Line("异常如愿捕获！");
            }
        }


        public static void EF默认的事务处理()
        {
            int outputId = 1, intputId = 1;
            decimal transferAmount = 1000m;
            using(var db=new Context())
            {
                //1. 检索事务中涉及的账户
                var outputAccount = db.OutputAccounts.Find(outputId);
                var inputAccount = db.InputAccounts.Find(intputId);
                //2. 从输出账户上扣除1000
                outputAccount.Balance -= transferAmount;
                //3.从输入账户上增加1000
                inputAccount.Balance += transferAmount;
                //4.提交事务
                db.SaveChanges();
            }

            Console.WriteLine("EF默认的事务处理");
        }


        public static void 使用TransactionScope处理事务()
        {
            int outputId = 1, inputId = 1;
            decimal transferAmount = 1000m;
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                var db1 = new Context();
                var db2 = new Context();
                //1 检索事务中涉及的账户
                var outputAccount = db1.OutputAccounts.Find(outputId);
                var inputAccount = db2.InputAccounts.Find(inputId);
                //2 从输出账户上扣除1000
                outputAccount.Balance -= transferAmount;
                //3 从输入账户上增加1000
                inputAccount.Balance += transferAmount;

                db1.SaveChanges();
                db2.SaveChanges();
                ts.Complete();
            };
            Console.WriteLine("使用TransactionScope处理事务");
        }


        public static void 使用EF6管理事务()
        {
            int outputId = 1, inputId = 1;
            decimal transferAmount = 1000m;
            using(var db=new Context())
            {
                using(var trans = db.Database.BeginTransaction())
                {
                    try
                    {
                        var sql = "Update OutputAccounts set  Balance=Balance-@amountToDebit where id=@outputId";
                        db.Database.ExecuteSqlCommand(sql, new SqlParameter("@amountToDebit", transferAmount), new SqlParameter("@outputId", outputId));


                        var inputAccount = db.InputAccounts.Find(inputId);
                        inputAccount.Balance += transferAmount;
                        db.SaveChanges();

                        trans.Commit();
                    }
                    catch (Exception)
                    {

                        trans.Rollback();
                    }
                }
            }
            Console.WriteLine("使用EF6管理事务");
        }

        public static void 使用已存在的事务()
        {
            int outputid = 1, inputid = 1;
            decimal transferAmount = 1000m;
            var connectionString = ConfigurationManager.ConnectionStrings["FirstCodeFirstApp"].ConnectionString;

            using(var conn=new SqlConnection(connectionString))
            {
                conn.Open();
                using(var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var result = DebitOutputAccount(conn, trans, outputid, transferAmount);
                        if (!result)
                        {
                            throw new Exception("不能正常扣款！");
                        }

                        using(var db=new Context(conn, ContextOwnsConnection: false))
                        {
                            db.Database.UseTransaction(trans);
                            var inputAccount = db.InputAccounts.Find(inputid);
                            inputAccount.Balance += transferAmount;
                            db.SaveChanges();
                        }

                        trans.Commit();
                    }
                    catch (Exception ex)
                    {

                        trans.Rollback();
                        Console.WriteLine("事务回滚");
                    }
                }
            }

            Console.WriteLine("使用已存在的事务");
        }


    }
}
