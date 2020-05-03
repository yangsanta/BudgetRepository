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
            var budgets = this._repo.GetALL();
            var budgetsPerDay = this.GetBudgetsPerDay(budgets);
            return budgetsPerDay.Where(x => startDateTime.Date <= x.Date && x.Date <= endDateTime.Date).Sum(x => x.Amount);
        }

        private List<BudgetEntity> GetBudgetsPerDay(List<Budget> budgets)
        {
            var budgetsPerDay = new List<BudgetEntity>();
            foreach (var budget in budgets)
            {
                var year = int.Parse(budget.YearMonth.Substring(0, 4));
                var month = int.Parse(budget.YearMonth.Substring(4, 2));
                var day = 1;
                var daysInMonth = DateTime.DaysInMonth(year, month);
                var amountPerDay = Convert.ToDecimal(budget.Amount) / daysInMonth;
                while (day <= daysInMonth)
                {
                    budgetsPerDay.Add(new BudgetEntity() { Date = new DateTime(year, month, day++), Amount = amountPerDay });
                }
            }

            return budgetsPerDay;
        }
    }
}
