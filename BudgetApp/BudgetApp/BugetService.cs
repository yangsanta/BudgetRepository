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
            var budgetsPerDay = this._repo.GetALL().SelectMany(x =>x.BudgetsPerDay);
            return budgetsPerDay.Where(x => startDateTime.Date <= x.Date && x.Date <= endDateTime.Date).Sum(x => x.Amount);
        }
    }
}
