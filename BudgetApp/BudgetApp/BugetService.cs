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

        public decimal Query(DateTime startDateTime, DateTime endDateTime)
        {

            var all = this._repo.GetALL()
                                                .Select(x => new BudgetEntity
                                                {
                                                    YeatMonthDateTime = int.Parse(x.YearMonth),
                                                    Amount = x.Amount
                                                });
            BudgetEntity NowMonth = null;

            if (startDateTime.Month == endDateTime.Month && startDateTime.Day == 1 && endDateTime.Day == DateTime.DaysInMonth(endDateTime.Year, endDateTime.Month))
            {
                var now = int.Parse(startDateTime.ToString("yyyyMM"));
                NowMonth = all.Where(o => startDateTime.Month == endDateTime.Month && o.YeatMonthDateTime == now).FirstOrDefault();
            }
            else if (startDateTime.Date == endDateTime.Date)
            {
                var now = int.Parse(startDateTime.ToString("yyyyMM"));
                NowMonth = all.Where(o => startDateTime.Month == endDateTime.Month && o.YeatMonthDateTime == now).FirstOrDefault();

                return NowMonth.Amount / DateTime.DaysInMonth(startDateTime.Year, startDateTime.Month);
            }
            else if (startDateTime.Month < endDateTime.Month || startDateTime.Year != endDateTime.Year)
            {
                int firstDay = DateTime.DaysInMonth(startDateTime.Year, startDateTime.Month) - startDateTime.Day + 1;
                var now = int.Parse(startDateTime.ToString("yyyyMM"));
                NowMonth = all.Where(o => o.YeatMonthDateTime == now).FirstOrDefault();
                decimal firstAmount = NowMonth.Amount * (firstDay / DateTime.DaysInMonth(startDateTime.Year, startDateTime.Month));

                if (endDateTime.Month - startDateTime.Month == 1)
                {
                    var nowNext = int.Parse(endDateTime.ToString("yyyyMM"));
                    var NextMonth = all.Where(o => o.YeatMonthDateTime == nowNext).FirstOrDefault();

                    decimal lastAmount = (NextMonth.Amount / DateTime.DaysInMonth(endDateTime.Year, endDateTime.Month)) * endDateTime.Day;
                    return firstAmount + lastAmount;
                }
                else if (endDateTime.Month - startDateTime.Month > 1)
                {

                }
            }

            return NowMonth.Amount;
        }


    }
}
