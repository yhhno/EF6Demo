using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05EF中ViewsAndStoreProcedure
{
    public class Initializer : DropCreateDatabaseIfModelChanges<DonatorContext>
    {
        protected override void Seed(DonatorContext context)
        {
            var drop = @"Drop Table DonatorViews";
            context.Database.ExecuteSqlCommand(drop);
            var createView = @"CREATE VIEW [dbo].[DonatorViews]
                            AS SELECT 
                            dbo.Donators.Id AS DonatorId,
                            dbo.Donators.Name AS DonatorName,
                            dbo.Donators.Amount AS Amount,
                            dbo.Donators.DonateDate AS DonateDate,
                            dbo.Provinces.ProvinceName AS ProvinceName
                            FROM dbo.Donators
                            INNER JOIN dbo.Provinces ON dbo.Provinces.Id = dbo.Donators.ProvinceId";
            context.Database.ExecuteSqlCommand(createView);


            var loadData = @"INSERT dbo.Provinces VALUES( N'山东省')
INSERT dbo.Provinces VALUES( N'河北省')

INSERT dbo.Donators VALUES  ( N'陈志康', 50, '2016-04-07',1)
INSERT dbo.Donators VALUES  ( N'海风', 5, '2016-04-08',1)
INSERT dbo.Donators VALUES  ( N'醉、千秋', 12, '2016-04-13',1)
INSERT dbo.Donators VALUES  ( N'雪茄', 18.8, '2016-04-15',2)
INSERT dbo.Donators VALUES  ( N'王小乙', 10, '2016-04-09',2)";

            context.Database.ExecuteSqlCommand(loadData);


            var UpdateProc = @"CREATE PROCEDURE UpdateHeBeiDonator
@namePrefix AS NVARCHAR(10),
@addedAmount AS DECIMAL
AS 

BEGIN
UPDATE dbo.Donators SET Name=@namePrefix+Name,Amount=Amount+@addedAmount
WHERE ProvinceId=2/*给河北省的打赏者名字前加个前缀,并将金额加上指定的数量*/
END";


            context.Database.ExecuteSqlCommand(UpdateProc);


            base.Seed(context);
        }
    }
}
