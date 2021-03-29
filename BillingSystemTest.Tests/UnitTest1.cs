
using System;
using BillingSystemTest.Services;
using DatabaseCore;
using Microsoft.EntityFrameworkCore;
using DatabaseCore.Models;
using BillingSystemTest.Common.Models;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using BillingSystemTest.Common.Enums;
using BillingSystemTest.Common.DTO;

namespace BillingSystemTest.Tests
{
    
    public class TestBalance
    {
        [Fact]
        public void IsGetBalance()
        {
            var opt = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(databaseName: "TestBalance").Options;
            var context = new DatabaseContext(opt);
            AddDb(context);


            var service = new AccountService(context);

            BalanceRequestModel model = new BalanceRequestModel { UserID = 1};


            var res = service.GetBalance(model);

            Assert.True(400 == res.Result.Balance);

        }
        [Fact]
        public void IsGetHistory()
        {
            var opt = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(databaseName: "TestHistory").Options;
            var context = new DatabaseContext(opt);
            AddDb(context);


            var service = new AccountService(context);

            HistoryRequestModel model = new HistoryRequestModel
            {
                UserID = 1,
                From = DateTime.Parse("20.02.2020"),
                To = DateTime.Parse("24.04.2020")
            };

            //res.Result, a => a.Time >= DateTime.Parse("20.02.2020") && a.Time <= DateTime.Parse("24.04.2020")
            var res = service.GetHistory(model);

            
            foreach(var item in res.Result)
            {
                Assert.True(model.From <= item.Time && model.To >= item.Time);
            }
            
        }

        [Fact]
        public void IsAddTr()
        {
            var opt = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(databaseName: "AddTr").Options;
            var context = new DatabaseContext(opt);

            var serivce = new AccountService(context);

            AddTransactionRequestModel model = new AddTransactionRequestModel
            {
                UserID = 1,
                Time = DateTime.Parse("20.02.2020"),
                Amount = 2000,
                Notes = "String"
            };

            var res = serivce.AddTransaction(model);

            Assert.Equal(ResultType.Success, res.Result);

        }
        [Fact]
        public void IsGetStat()
        {
            var opt = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(databaseName: "GetStat").Options;
            var context = new DatabaseContext(opt);

            var serivce = new AccountService(context);
            AddDb(context);

            StatisticRequestModel model = new StatisticRequestModel
            { onDay = DateTime.Parse("20.02.2020") };

            List<UserStatisticDTO> expected = new List<UserStatisticDTO>()
            {
                new UserStatisticDTO
                {
                    UserID = 4,
                    Debet = 0,
                    Credit = 250

                },
                new UserStatisticDTO
                {
                     UserID = 3,
                     Debet = 0,
                     Credit = 200
                }

            };
            var res = serivce.GetStatistics(model);

            foreach (var item in res.Result)
            {
                Assert.Equal(model.onDay, item.Day);
            }

        }

        private void AddDb(DatabaseContext database)
        {
            var usersNew = new[] {

                new UserModel
                {
                    Account = new AccountModel { Balance = 400, Credit = 2000, Debet = 1000 },
                    Transactions = new List<TransactionModel>()
                    {
                        new TransactionModel { Amount = 200, Time = DateTime.Parse("20.02.2020"), Notes = "Первый перевод" },
                        new TransactionModel { Amount = 200, Time = DateTime.Parse("20.03.2020"), Notes = "Второй перевод" },
                        new TransactionModel { Amount = 100, Time = DateTime.Parse("20.03.2012"), Notes = "Второй перевод" }
                    }
                },
                new UserModel
                {
                    Account = new AccountModel { Balance = 500, Debet = 4000, Credit = 2000 },
                    Transactions = new List<TransactionModel>()
                    {
                        new TransactionModel { Amount = 200, Time = DateTime.Parse("20.02.2020"), Notes = "Первый перевод" },
                        new TransactionModel { Amount = 200, Time = DateTime.Parse("20.03.2020"), Notes = "Второй перевод" },
                        new TransactionModel { Amount = 100, Time = DateTime.Parse("20.03.2012"), Notes = "Второй перевод" }
                    }
                }
            };

            database.Users.AddRange(usersNew);
            database.SaveChanges();
        }

    }
}
