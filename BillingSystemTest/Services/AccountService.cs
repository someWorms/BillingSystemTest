using BillingSystemTest.Common.DTO;
using BillingSystemTest.Common.Enums;
using BillingSystemTest.Common.Models;
using BillingSystemTest.Services.Interfaces;
using DatabaseCore.Enums;
using DatabaseCore.Interfaces;
using DatabaseCore.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


namespace BillingSystemTest.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDatabaseContext _db;
        public AccountService(IDatabaseContext db)
        {
            _db = db;
        }


        public async Task<BalanceInfoDTO> GetBalance(BalanceRequestModel model)
        {
            var balance = await _db.Accounts.FirstOrDefaultAsync(u => u.Id == model.UserID);

            if (balance == null) return null;
            

            BalanceInfoDTO result = new BalanceInfoDTO
            {
                Balance = balance.Balance
            };

            return result;
        }


        public async Task<List<TransactionDTO>> GetHistory(HistoryRequestModel model)
        {

            var transaction = await _db.Transactions
                .Where(t => t.Time.Date >= model.From.Date && t.Time.Date <= model.To.Date && t.UserID == model.UserID)
                .ToListAsync();


            return transaction.Adapt<List<TransactionDTO>>();

        }

        public async Task<ResultType> AddTransaction(AddTransactionRequestModel model)
        {
            var user = await _db.Users.Include(x => x.Transactions).Include(c => c.Account).FirstOrDefaultAsync(u => u.Id == model.UserID);

            if (user == null)
            {
                return ResultType.Fail;
            }

            if (model.Amount < 0 && user.Account.Balance + model.Amount < 0)
            {
                return ResultType.Fail;
            }

            TransactionType tt = model.Amount < 0 ? tt = TransactionType.Debt : TransactionType.Income;

            TransactionModel transaction = new TransactionModel()
            {
                UserID = model.UserID,
                Time = model.Time,
                Amount = model.Amount,
                Notes = model.Notes,
                Type = tt
            };

            user.Transactions.Add(transaction);

            
            if (tt == TransactionType.Debt)
            {
                user.Account.Credit += (model.Amount * -1);
            }
            else
            {
                user.Account.Debet += model.Amount;
            }
            

            user.Account.Balance += model.Amount;

            await _db.SaveChangesAsync();

            return ResultType.Success;
        }

        public async Task<List<UserStatisticDTO>> GetStatistics(StatisticRequestModel model)
        {
            var tInfo = await _db.Transactions.Where(x => x.Time.Date == model.onDay.Date).ToListAsync();

            List<UserStatisticDTO> userStatistics = new List<UserStatisticDTO>();

            

            foreach (var item in tInfo)
            {
                decimal debet = 0;
                decimal credit = 0;
                if (item.Type == TransactionType.Income)
                {
                    debet += item.Amount;
                }
                else
                {
                    credit += item.Amount;
                }

                var user = userStatistics.FirstOrDefault(u => u.UserID == item.UserID);

                if (user != null)
                {
                    user.Debet += debet;
                    user.Credit += credit;
                }
                else
                {
                    UserStatisticDTO statistic = new UserStatisticDTO
                    {
                        UserID = item.UserID,
                        Debet = debet,
                        Credit = credit,
                        Day = model.onDay
                    };
                    userStatistics.Add(statistic);
                }
            }
            return userStatistics;
        }
    }
}
