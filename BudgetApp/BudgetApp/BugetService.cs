using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    public class BudgetService
    {
        private IBudgetRepo _repo;

        public BudgetService(IBudgetRepo repo)
        {
            this._repo = repo;
        }

        public decimal Query(DateTime now1, DateTime now2)
        {

            var all = this._repo.GetALL()
                                                .Select(x=> new BudgetEntity
                                                {
                                                    YeatMonthDateTime = int.Parse(x.YearMonth),
                                                    Amount = x.Amount
                                                });
            var now = int.Parse(now1.ToString("yyyyMM"));
            BudgetEntity NowMonth = all.Where(o => now1.Month == now2.Month && o.YeatMonthDateTime == now).FirstOrDefault();

            return NowMonth.Amount;
        }



    }
}
