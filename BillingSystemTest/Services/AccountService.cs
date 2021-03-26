using BillingSystemTest.Common.DTO;
using BillingSystemTest.Common.Enums;
using BillingSystemTest.Common.Models;
using BillingSystemTest.Services.Interfaces;
using DatabaseCore.Interfaces;
using DatabaseCore.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public async Task<BalanceInfoDTO> GetBalance(int id)
        {
            var user = await _db.Users.Include(x => x.Account).FirstOrDefaultAsync(u => u.Id == id);

            BalanceInfoDTO result = new BalanceInfoDTO
            {
                Name = user.Name,
                Balance = user.Account.Balance
            };

            return result;
        }


        public async Task<List<TransactionDTO>> GetHistory(HistoryRequestModel model)
        {
            var user = await _db.Users.Include(x => x.Transactions.Where(t => t.Time >= model.From && t.Time <= model.To)).FirstOrDefaultAsync(u => u.Id == model.UserID);

            return user.Transactions.Adapt<List<TransactionDTO>>();

        }

        public async Task<ResultType> AddTransaction(AddTransactionRequestModel model)
        {
            var user = await _db.Users.Include(x => x.Transactions).Include(c => c.Account).FirstOrDefaultAsync(u => u.Id == model.UserID);

            TransactionModel transaction = new TransactionModel()
            {
                UserID = model.UserID,
                Time = model.Time,
                Amount = model.Amount,
                Notes = model.Notes,
            };

            user.Transactions.Add(transaction);

            if (model.Amount < 0 && user.Account.Balance + model.Amount < 0)
            {
                return ResultType.Fail;
            }
            //TODO переписать
            if (model.Amount < 0)
            {
                user.Account.Credet += (model.Amount * -1);
            }
            else
            {
                user.Account.Debet += model.Amount;
            }
            

            user.Account.Balance += model.Amount;

            await _db.SaveChangesAsync();

            return ResultType.Success;
        }

        public async Task<List<UserStatisticDTO>> GetStatistics(DateTime dateTime)
        {
            
        }

    }
}
