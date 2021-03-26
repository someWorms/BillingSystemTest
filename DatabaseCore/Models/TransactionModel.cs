using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCore.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public int UserID { get; set; }

        public DateTime Time { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; }

        public UserModel User { get; set; }
    }
}
