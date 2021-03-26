using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingSystemTest.Common.DTO
{
    public class TransactionDTO
    {
        public DateTime Time { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public decimal Debet { get; set; }
        public decimal Credit { get; set; }

    }
}
