using BillingSystemTest.Common.DTO;
using BillingSystemTest.Common.Enums;
using BillingSystemTest.Common.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace BillingSystemTest.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BalanceInfoDTO> GetBalance(BalanceRequestModel model);
        Task<List<TransactionDTO>> GetHistory(HistoryRequestModel model);
        Task<ResultType> AddTransaction(AddTransactionRequestModel model);
        Task<List<UserStatisticDTO>> GetStatistics(StatisticRequestModel model);
    }
}
