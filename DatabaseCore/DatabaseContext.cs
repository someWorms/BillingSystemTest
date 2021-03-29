using DatabaseCore.Interfaces;
using DatabaseCore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseCore
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> opt) : base(opt)
        {
            Database.EnsureCreated();


            #region create test data

            UserModel user = Users.FirstOrDefault(a => a.Name == "test");

            if (user == null)
            {
                Users.Add(new UserModel()
                {
                    Name = "test",
                    LastName = "test",
                    Account = new AccountModel { Balance = 400 },
                    Transactions = new List<TransactionModel>()
                    {
                        new TransactionModel { Amount = 100, Time = DateTime.Parse("11.11.2011"), Notes = "Первый перевод" },
                        new TransactionModel { Amount = 100, Time = DateTime.Parse("18.12.2011"), Notes = "Второй перевод" },
                        new TransactionModel { Amount = 200, Time = DateTime.Parse("02.04.2012"), Notes = "Третий перевод" }
                    }
                });

                Users.Add(new UserModel()
                {
                    Name = "test2",
                    LastName = "test2",
                    Account = new AccountModel { Balance = 400 },
                    Transactions = new List<TransactionModel>()
                    {
                        new TransactionModel { Amount = 100, Time = DateTime.Parse("11.11.2011"), Notes = "Первый перевод" },
                        new TransactionModel { Amount = 100, Time = DateTime.Parse("18.12.2011"), Notes = "Второй перевод" },
                        new TransactionModel { Amount = 500, Time = DateTime.Parse("03.04.2012"), Notes = "Третий перевод" }
                    }
                });

                SaveChanges();
            }

            #endregion
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasOne(a => a.Account)
                .WithOne(b => b.User)
                .HasForeignKey<AccountModel>(b => b.UserID);

            modelBuilder.Entity<UserModel>()
                .HasMany(a => a.Transactions)
                .WithOne(b => b.User);
        }

    }
}
