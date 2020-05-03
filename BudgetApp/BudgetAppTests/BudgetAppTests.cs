using System;
using System.Collections.Generic;
using BudgetApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace BudgetAppTests
{
    [TestClass]
    public class BudgetAppTests
    {
        private Budget budget = new Budget();

        [TestMethod]
        public void OneDay()
        {
            var repo = NSubstitute.Substitute.For<IBudgetRepo>();
            repo.GetALL().Returns(new List<Budget>() {
                new Budget() { YearMonth = "202004", Amount = 3000 },
                new Budget() { YearMonth = "202005", Amount = 3100 },
                new Budget() { YearMonth = "202006", Amount = 3000 }
            });
            var budgetService = new BudgetService(repo);
            var dateTime = DateTime.Now;
            var queryResult = budgetService.Query(dateTime, dateTime);

            Assert.AreEqual(100, queryResult);
        }

        [TestMethod]
        public void OneMonth()
        {
            var repo = NSubstitute.Substitute.For<IBudgetRepo>();
            repo.GetALL().Returns(new List<Budget>() {
                new Budget() { YearMonth = "202004", Amount = 3000 },
                new Budget() { YearMonth = "202005", Amount = 3100 },
                new Budget() { YearMonth = "202006", Amount = 3000 }
            });
            var budgetService = new BudgetService(repo);
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(2020, 05, 1);
            var lastDayOfMonth = new DateTime(2020, 05, 31);

            var queryResult = budgetService.Query(firstDayOfMonth, lastDayOfMonth);

            Assert.AreEqual(3100, queryResult);
        }

        [TestMethod]
        public void MultipleDay()
        {
            var repo = NSubstitute.Substitute.For<IBudgetRepo>();
            repo.GetALL().Returns(new List<Budget>() {
                new Budget() { YearMonth = "202004", Amount = 300 },
                new Budget() { YearMonth = "202005", Amount = 3100 },
                new Budget() { YearMonth = "202006", Amount = 30000 }
            });
            var budgetService = new BudgetService(repo);

            var firstDayOfMonth = new DateTime(2020, 05, 21);
            var lastDayOfMonth = new DateTime(2020, 06, 2);

            var queryResult = budgetService.Query(firstDayOfMonth, lastDayOfMonth);

            Assert.AreEqual(3100, queryResult);
        }

        [TestMethod]
        public void MultipleMonth()
        {
            var repo = NSubstitute.Substitute.For<IBudgetRepo>();
            repo.GetALL().Returns(new List<Budget>() {
                new Budget() { YearMonth = "202004", Amount = 300 },
                new Budget() { YearMonth = "202005", Amount = 3100 },
                new Budget() { YearMonth = "202006", Amount = 30000 }
            });
            var budgetService = new BudgetService(repo);
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(2020, 04, 29);
            var lastDayOfMonth = new DateTime(2020, 06, 1);

            var queryResult = budgetService.Query(firstDayOfMonth, lastDayOfMonth);

            Assert.AreEqual(4120,queryResult);
        }
    }
}
