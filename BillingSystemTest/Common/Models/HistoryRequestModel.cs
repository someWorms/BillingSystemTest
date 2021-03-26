using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingSystemTest.Common.Models
{
    public class HistoryRequestModel
    {
        public int UserID { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
