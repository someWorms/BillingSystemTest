using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingSystemTest.Common.DTO
{
    public class UserStatisticDTO
    {
        public int UserID { get; set; }
        public decimal Debet { get; set; }
        public decimal Credet { get; set; }
    }
}
