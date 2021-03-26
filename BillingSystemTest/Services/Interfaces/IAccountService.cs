using BillingSystemTest.Common.DTO;
using BillingSystemTest.Common.Enums;
using BillingSystemTest.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BillingSystemTest.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BalanceInfoDTO> GetBalance(int id);
        Task<List<TransactionDTO>> GetHistory(HistoryRequestModel model);
        Task<ResultType> AddTransaction(AddTransactionRequestModel model);
    }
}
