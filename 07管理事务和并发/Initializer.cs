using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07管理事务和并发
{
    public class Initializer:DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            context.Donators.Add(new Donator
            {
                Name = "捐赠者1",
                Amount = 1111m,
                DonateDate = DateTime.Now

            });
            context.Donators.Add(new Donator
            {
                Name = "捐赠者2",
                Amount = 2222m,
                DonateDate = DateTime.Now

            });

            context.OutputAccounts.Add(new _07管理事务和并发.OutputAccount
            {
                Name = "甲",
                Balance = 6000m
            });

            context.InputAccounts.Add(new InputAccount
            {
                Name = "乙",
                Balance = 0m
            });


            base.Seed(context);
        }
    }
}
