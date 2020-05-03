using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetApp
{
    /// <summary>
    /// 預算
    /// </summary>
    public class Budget
    {
        /// <summary>
        /// 時間
        /// </summary>f
        public string YearMonth { get; set; }


        /// <summary>
        /// 金額
        /// </summary>
        public int Amount { get; set; }

        public List<BudgetEntity> BudgetsPerDay
        {
            get
            {
                var budgetsPerDay = new List<BudgetEntity>();
                var year = int.Parse(this.YearMonth.Substring(0, 4));
                var month = int.Parse(this.YearMonth.Substring(4, 2));
                var day = 1;
                var daysInMonth = DateTime.DaysInMonth(year, month);
                var amountPerDay = Convert.ToDecimal(this.Amount) / daysInMonth;
                while (day <= daysInMonth)
                {
                    budgetsPerDay.Add(new BudgetEntity()
                    {
                        Date = new DateTime(year, month, day++),
                        Amount = amountPerDay
                    });
                }

                return budgetsPerDay;
            }
        }
    }
}
