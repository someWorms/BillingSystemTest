using BillingSystemTest.Common.DTO;
using BillingSystemTest.Common.Enums;
using BillingSystemTest.Common.Models;
using BillingSystemTest.Services.Interfaces;
using DatabaseCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;



namespace BillingSystemTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [HttpPost("balance")]
        public async Task<BalanceInfoDTO> Post([FromBody] BalanceRequestModel model)
        {
            return await _accountService.GetBalance(model);
        }

        [HttpPost("history")]
        public async Task<List<TransactionDTO>> Post([FromBody] HistoryRequestModel model)
        {
            return await _accountService.GetHistory(model);
        }

        [HttpPost("statistic")]
        public async Task<List<UserStatisticDTO>> Post([FromBody] StatisticRequestModel model)
        {
            return await _accountService.GetStatistics(model);
        }

        [HttpPut("add")]
        public async Task<ResultType> Put([FromBody] AddTransactionRequestModel model)
        {
            return await _accountService.AddTransaction(model);
        }

    }
}
