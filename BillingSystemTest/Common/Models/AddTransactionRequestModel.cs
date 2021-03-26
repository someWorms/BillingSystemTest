using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingSystemTest.Common.Models
{
    public class AddTransactionRequestModel
    {
        public int UserID { get; set; }
        public DateTime Time { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }
    }
}
